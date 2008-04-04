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

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            string user = textUser.Text;
            string pass = textPass.Text;

            if( iphone.UploadData( user, pass ) ) /* sent */
            {
                MessageBox.Show( "Upload done!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information );
                Close();
            }
            else
            {
                MessageBox.Show( "Could not upload data!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void UploadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            iphone = null;
        }
    }
}
