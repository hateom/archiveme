using System;
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

        public void onProgress( string table, int recnum, int max )
        {
            if(InvokeRequired)
            {
                BeginInvoke( new iPhoneManager.onProgressDelegate(onProgress), new object[]{table,recnum,max} );
                return;
            }

            labelStatus.Text = string.Format("Uploading {0} ({1}/{2})", table, recnum, max);
        }

        private bool upload_result = false;
        private string upload_response = "";

        public void OnUpload()
        {
            if(InvokeRequired)
            {
                string user = textUser.Text;
                string pass = DBManager.MD5( textPass.Text );

                upload_result = iphone.UploadData( user, pass, out upload_response, onProgress );

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
            onUploadDelegate onUpload = OnUpload;
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
