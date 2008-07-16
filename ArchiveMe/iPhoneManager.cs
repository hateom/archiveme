using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using Tamir.SharpSsh;
using Manzana;
using System.Net;
using System.Web;

namespace ArchiveMe
{
    public class iPhoneManager
    {
        #region Connections
        
        public bool Connect()
        {
            if(mobile != null) return true;

            mobile = new iPhone();
            mobile.Connect += new ConnectEventHandler(mobile_Connect);
            mobile.Disconnect += new ConnectEventHandler(mobile_Disconnect);

            return true;
        }

        private delegate bool onSSHConnect();
        private bool sshConnect()
        {
            try
            {
                scp.Connect();
                if(scp.Connected)
                {
                    isConnected = true;
                    ctype = ConnType.ctSSH;
                    return true;
                }
            }
            catch(System.Net.Sockets.SocketException)
            {
                return false;
            }
            catch(Exception)
            {
                return false;
            }

            return false;
        }

        public bool Connect( string ip, string password)
        {
            if(ip == "") return false;
            if(IsConnected && ctype == ConnType.ctSSH) scp.Close();

            scp = new Scp( ip, "root", password );
            onSSHConnect sshc = new onSSHConnect( sshConnect );
            sshc.BeginInvoke( null, null );

            return true;
        }
        public void Disconnect()
        {
            if(!IsConnected) return;
            
            if( ctype == ConnType.ctSSH )
            {
                scp.Close();
            }
        }

        #endregion
        #region USB Events
        
        void mobile_Disconnect(object sender, ConnectEventArgs args)
        {
            isConnected = false;
        }
        void mobile_Connect(object sender, ConnectEventArgs args)
        {
            ctype = ConnType.ctUSB;
            isConnected = true;
        }

        #endregion
        #region Reading SMS
        
        SmsDataSet ReadSmsDataSet(string fileName)
        {
            SmsDataSet ds;
            using(var conn = new SQLiteConnection(@"data source=" + fileName))
            {
                var mta = new SmsDataSetTableAdapters.messageTableAdapter();
                ds = new SmsDataSet();
                mta.Connection = conn;
                mta.Fill(ds.message);
            }
            return ds;
        }
        AddrDataSet ReadAddrDataSet(string fileName)
        {
            AddrDataSet ds;
            using(var conn = new SQLiteConnection(@"data source=" + fileName))
            {
                var mta = new AddrDataSetTableAdapters.ABPersonTableAdapter();
                var mtn = new AddrDataSetTableAdapters.ABMultiValueTableAdapter();
                ds = new AddrDataSet();
                mta.Connection = conn;
                mta.Fill(ds.ABPerson);
                mtn.Fill(ds.ABMultiValue);
            }
            return ds;
        }
        
        public bool Synchronize( ref DBManager.synchroResult s_res )
        {
            bool result = false;

            switch( ctype )
            {
                case ConnType.ctUSB: 
                    result = ReadUSBFile(smsFileName, smsDst) && 
                             ReadUSBFile(addrFileName, addrDst); 
                    break;
                case ConnType.ctSSH:
                    result = ReadSSHFile(smsFileName, smsDst) && 
                             ReadSSHFile(addrFileName, addrDst); 
                    break;
            }

            if(!result) return false;

            string dir = Directory.GetCurrentDirectory();

            string in1 = dir + @"\" + smsDst;
            string in2 = dir + @"\" + addrDst;
            sms_ds  = ReadSmsDataSet( in1 );
            addr_ds = ReadAddrDataSet( in2 );
            File.Delete(in1);
            File.Delete(in2);

            return DBManager.Synchronize( sms_ds, addr_ds, ref s_res );
        }
        
        private bool ReadSSHFile( string f_from, string f_to )
        {
            if(scp == null || !scp.Connected) return false;
            scp.From(f_from, f_to);
            if(File.Exists(f_to))
                return true;

            return false;
        }
        private bool ReadUSBFile( string f_from, string f_to )
        {
            if(!IsConnected) return false;
            if(mobile.Exists(smsFileName))
            {
                iPhoneFile file = iPhoneFile.OpenRead(mobile, f_from);
                
                if(!file.CanRead) return false;
                FileStream fs = File.Create(f_to);
                if(!fs.CanWrite) return false;

                int chunk_size = 1024 * 1024;
                byte [] buffer = new byte[chunk_size];
                int read;
                while(true)
                {
                    read = file.Read( buffer, 0, chunk_size ); 
                    fs.Write( buffer, 0, read );
                    if(read < chunk_size) break;
                }
                    
                fs.Close();
                file.Close();
                
                if(File.Exists(smsDst)) 
                    return true;
            }

            return false;
        }

        #endregion
        #region Fields

        public enum ConnType { ctNone, ctUSB, ctSSH }
        private ConnType ctype = ConnType.ctNone;
        public ConnType connectionType { get { return ctype; } }
        private Scp scp = null;
        private iPhone mobile = null;
        public OnConnected usbEvent = null;

        private bool isConnected = false;
        public bool IsConnected { get { return isConnected; } }
        public delegate void OnConnected(ConnType ct, bool result);
        public const string smsFileName  = @"/User/Library/SMS/sms.db";
        public const string addrFileName = @"/User/Library/AddressBook/AddressBook.sqlitedb";
        public const string smsDst       = @"sms.db";
        public const string addrDst      = @"addr.db";

        private SmsDataSet  sms_ds  = null;
        private AddrDataSet addr_ds = null;

        #endregion

        private bool uploadData( string url, string data, out string resp )
        {
            resp = "";
            byte[] postBytes = Encoding.ASCII.GetBytes( data.ToString() );
            bool result = true;

            System.Net.ServicePointManager.CertificatePolicy = new ArchiveMePolicy();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create( url );
            Stream requestStream = null;

            try
            {
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded"; // "text";
                request.ContentLength = postBytes.Length;
                requestStream = request.GetRequestStream();
                requestStream.Write( postBytes, 0, postBytes.Length );
            }
            catch(WebException)
            {
            }
            finally
            {
                if(requestStream != null) requestStream.Close();
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string response_data = new StreamReader( response.GetResponseStream() ).ReadToEnd();
                resp = response_data;
            }
            catch(WebException)
            {
                result = false;
            }

            return result;
        }

        public delegate void onProgressDelegate( string table, int recnum, int max );

        internal bool UploadData( string user, string pass, out string resp, onProgressDelegate op )
        {
            
            //string uri = @"http://student.uci.agh.edu.pl/~huczek/isms/test.php";
            string uri = @"http://127.0.0.1:8080/isms/upload.php";
            //string uri = @"https://upload.archiveme.net/upload.php";
            bool result = true;
            string rpmsg;
            resp = "";
            int recnum = 0;

            StringBuilder istr = new StringBuilder( "" );
            istr.Append( "&username=" + HttpUtility.UrlEncode( user ) );
            istr.Append( "&password=" + HttpUtility.UrlEncode( pass ) );
            result = uploadData( uri, istr.ToString(), out rpmsg );
            if(!result || !rpmsg.StartsWith( "ok" ))
            {
                resp = "Could not log in!";
                return false;
            }

            foreach(var i in DBManager.dataSet.numbers)
            {
                StringBuilder str = new StringBuilder( "" );
                str.Append( "user_id[]=" + HttpUtility.UrlEncode( i.user_id ) );
                str.Append( "&number[]=" + HttpUtility.UrlEncode( i.number ) );
                str.Append( "&username=" + HttpUtility.UrlEncode( user ) );
                str.Append( "&password=" + HttpUtility.UrlEncode( pass ) );
                op.BeginInvoke( "Phone numbers", recnum++, DBManager.dataSet.numbers.Count, null, null );
                result = uploadData( uri, str.ToString(), out rpmsg );
                if(!result || !rpmsg.StartsWith( "ok" )) return false;
            }

            recnum = 0;
            foreach(var i in DBManager.dataSet.users)
            {
                StringBuilder str = new StringBuilder( "" );
                str.Append( "id[]=" + HttpUtility.UrlEncode( i.id ) );
                str.Append( "&first[]=" + HttpUtility.UrlEncode( i.first ) );
                str.Append( "&last[]=" + HttpUtility.UrlEncode( i.last ) );
                str.Append( "&username=" + HttpUtility.UrlEncode( user ) );
                str.Append( "&password=" + HttpUtility.UrlEncode( pass ) );
                op.BeginInvoke( "Address book entries", recnum++, DBManager.dataSet.users.Count, null, null );
                result = uploadData( uri, str.ToString(), out rpmsg );
                if(!result || !rpmsg.StartsWith( "ok" )) return false;
            }

            recnum = 0;
            foreach(var i in DBManager.dataSet.messages)
            {
                StringBuilder str = new StringBuilder( "" );
                str.Append( "md5[]=" + HttpUtility.UrlEncode( i.md5 ) );
                str.Append( "&date[]=" + HttpUtility.UrlEncode( i.date.ToString() ) );
                str.Append( "&number[]=" + HttpUtility.UrlEncode( i.number ) );
                str.Append( "&message[]=" + HttpUtility.UrlEncode( i.message ) );
                str.Append( "&sent[]=" + HttpUtility.UrlEncode( i.sent.ToString() ) );
                str.Append( "&username=" + HttpUtility.UrlEncode( user ) );
                str.Append( "&password=" + HttpUtility.UrlEncode( pass ) );
                op.BeginInvoke( "Messages", recnum++, DBManager.dataSet.messages.Count, null, null );
                result = uploadData( uri, str.ToString(), out rpmsg );
                if(!result || !rpmsg.StartsWith( "ok" )) return false;
            }

            return true;
        }
    }
}
