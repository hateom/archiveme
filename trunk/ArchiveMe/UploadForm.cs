using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArchiveMe
{
    public partial class UploadForm : Form
    {
        public UploadForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        public void setIPhone( iPhoneManager manager )
        {
            iphone = manager;
        }

        private iPhoneManager iphone = null;

        public delegate void onUploadDelegate();

        public void onProgress( string table, int recnum )
        {
            if(InvokeRequired)
            {
                BeginInvoke( new iPhoneManager.onProgressDelegate(onProgress), new object[]{table,recnum} );
                return;
            }

            labelStatus.Text = "Uploading " + table + "("+recnum.ToString()+")";
        }

        private bool upload_result = false;
        private string upload_response = "";

        public void OnUpload()
        {
            if(InvokeRequired)
            {
                string user = textUser.Text;
                string pass = DBManager.MD5( textPass.Text );

                upload_result = iphone.UploadData( user, pass, out upload_response, new iPhoneManager.onProgressDelegate( onProgress ) );

                BeginInvoke( new onUploadDelegate( OnUpload ) );
                return;
            }

            if( upload_result )
            {
                MessageBox.Show( "Upload successful!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information );
                Close();
            }
            else
            {
                MessageBox.Show( upload_response, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            groupUpload.Enabled = true;
            buttonCancel.Enabled = true;
            buttonUpload.Enabled = true;

            buttonUpload.SetBounds( buttonUpload.Location.X, buttonUpload.Location.Y, 159, buttonUpload.Size.Height );
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            groupUpload.Enabled = false;
            buttonCancel.Enabled = false;
            buttonUpload.Enabled = false;
            buttonUpload.SetBounds( buttonUpload.Location.X, buttonUpload.Location.Y, 128, buttonUpload.Size.Height );
            
            upload_result = false;
            onUploadDelegate onUpload = new onUploadDelegate( OnUpload );
            onUpload.BeginInvoke( null, null );
        }

        private void UploadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            iphone = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
