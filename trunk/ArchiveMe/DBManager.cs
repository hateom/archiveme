using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace ArchiveMe
{
    static public class DBManager
    {
        public class synchroResult
        {
            public synchroResult()
            {
                numAdded = 0;
                smsAdded = 0;
                userAdded = 0;
            }

            public int numAdded;
            public int smsAdded;
            public int userAdded;
        };

        private const string db_filename = @"db\iSms.sqlite";
        static private iSmsDataSet data_set = null;

        static public iSmsDataSet dataSet 
        { 
            get 
            { 
                if( data_set != null ) return data_set; 

                string connString = @"data source=" + getLocalDBName();
                using(var conn = new SQLiteConnection( connString ))
                {
                    var msg = new iSmsDataSetTableAdapters.messagesTableAdapter();
                    var numbers = new iSmsDataSetTableAdapters.numbersTableAdapter();
                    var users = new iSmsDataSetTableAdapters.usersTableAdapter();

                    data_set = new iSmsDataSet();

                    msg.Connection = conn;
                    msg.Fill( data_set.messages );

                    numbers.Connection = conn;
                    numbers.Fill( data_set.numbers );

                    users.Connection = conn;
                    users.Fill( data_set.users );
                }

                return data_set;
            } 
        }

        static public string getLocalDBName()
        {
            string currDir = Directory.GetCurrentDirectory();
            return currDir + @"\..\" + db_filename;
        }

        static public byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }
        private static bool messageExists(string hash, DataRowCollection rows)
        {
            foreach(DataRow j in rows)
            {
                if(j["md5"].ToString() == hash) return true;
            }
            return false;
        }
        private static bool addressExists(string hash, DataRowCollection rows)
        {
            foreach(DataRow j in rows)
            {
                if(j["id"].ToString() == hash) return true;
            }
            return false;
        }
        static public string getHash(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            string hash = System.Convert.ToBase64String(md5.ComputeHash(StrToByteArray(input)));
            return hash;
        }

        static public string MD5( string input )
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(input);
            string ret = "";
    		try {
	    		System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
		    	cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
			    byte[] hash = cryptHandler.ComputeHash (textBytes);
			    foreach (byte a in hash) {
				    if (a<16)
    					ret += "0" + a.ToString("x");
	    			else
		    			ret += a.ToString("x");
    			}
	    		return ret ;
		    }
		    catch {
			    return "";
		    }
        }

        public static bool Synchronize(SmsDataSet sms_ds, AddrDataSet addr_ds, ref synchroResult res )
        {
            string connString;
            if( !File.Exists(getLocalDBName() ) )
            {
                return false;
            }
            connString = @"data source=" + getLocalDBName();

            using(var conn = new SQLiteConnection( connString ))
            {
                var msg     = new iSmsDataSetTableAdapters.messagesTableAdapter();
                var numbers = new iSmsDataSetTableAdapters.numbersTableAdapter();
                var users   = new iSmsDataSetTableAdapters.usersTableAdapter();

                data_set = new iSmsDataSet();

                msg.Connection = conn;
                msg.Fill( data_set.messages );

                numbers.Connection = conn;
                numbers.Fill( data_set.numbers );

                users.Connection = conn;
                users.Fill( data_set.users );

                foreach( var i in addr_ds.ABPerson )
                {
                    if(i.IsFirstNull() && i.IsLastNull()) continue;
                    string strFirst = (i.IsFirstNull() ? "" : i.First);
                    string strLast = (i.IsLastNull() ? "" : i.Last);
                    string input = strFirst + @"/" + strLast;
                    string hash = getHash( input );
                    if(addressExists(hash, users.GetData().Rows)) continue;
                    users.Insert(hash, strFirst, strLast);
                    res.userAdded++;
                    foreach( var k in addr_ds.ABMultiValue )
                    {
                        if(k.IsvalueNull()) continue;
                        if(k.property == 3 && k.record_id == i.ROWID)
                        {
                            string number = removeChars(k.value);
                            numbers.Insert(number, hash);
                            res.numAdded++;
                        }
                    }
                }

                foreach( var i in sms_ds.message )
                {
                    string strText = i.IstextNull() ? "" : i.text;
                    string strAddress = i.IsaddressNull() ? "" : i.address;
                    string hash = getHash(strText);
                    long longDate = i.IsdateNull() ? 0 : (long)i.date;
                    long longSent = i.flags == 3 ? 1 : 0;

                    if(messageExists(hash, msg.GetData().Rows)) continue;
                    msg.Insert(hash, strAddress, longToDate(i.date), strText, longSent );
                    res.smsAdded++;
                }

                try
                {
                    users.Update(data_set.users);
                    numbers.Update(data_set.numbers);
                    msg.Update(data_set.messages);
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private static DateTime longToDate(long timeStamp)
        {
            DateTime date = System.DateTime.Parse("1/1/1970");
            return date.AddSeconds(timeStamp);
        }

        private static string removeChars(string p)
        {
            char[] chars = p.ToCharArray();
            StringBuilder sb = new StringBuilder("");
            for(int i = 0; i < chars.Length; ++i )
            {
                if(allowedChar(chars[i])) sb.Append(chars[i]);
            }
            return sb.ToString();
        }

        private static bool allowedChar(char p)
        {
            switch(p)
            {
                case '0': return true;
                case '1': return true;
                case '2': return true;
                case '3': return true;
                case '4': return true;
                case '5': return true;
                case '6': return true;
                case '7': return true;
                case '8': return true;
                case '9': return true;
                case '+': return true;
            }

            return false;
        }
    }
}
