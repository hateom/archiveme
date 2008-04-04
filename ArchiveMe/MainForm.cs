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
    public partial class MainForm : Form
    {
        #region MainFrom methods

        public MainForm()
        {
            InitializeComponent();
            iphone = new iPhoneManager();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            groupMgr.Enabled = false;
            iphone.Connect();
            timerUSB.Enabled = true;
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerUSB.Enabled = false;
            iphone.Disconnect();
        }
        
        #endregion
        #region Control events

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            imgLoading.Visible = true;
            timerSSH.Enabled = true;
            setConnectControls(false);
            textStatus.Text = "Waiting for response...";
            if(!iphone.Connect( textIP.Text, textPwd.Text ))
            {
                setConnectControls( true );
                setStatus("Could not connect thru SSH",false);
                imgLoading.Visible = false;
            }
            groupMgr.Enabled = false;
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            iphone.Disconnect();
            Close();
        }
        private void buttonReadSMS_Click(object sender, EventArgs e)
        {
            DBManager.synchroResult s_result = new DBManager.synchroResult();
            if(!iphone.Synchronize(ref s_result))
            {
                MessageBox.Show("Could not read SMS database!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Synchronization done!\n\n"+
                                "Added "+ s_result.smsAdded +" messages,\n" + s_result.numAdded + " entries in addressbook", 
                                "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
        #region Connection callbacks
        /* all the callbacks for connect and disconnect events */

        private void onSSHConnect()
        {
            imgLoading.Visible = false;
            setConnectControls( false );
            setStatus("Connected thru SSH", true );
        }
        private void onUSBConnect()
        {
            setConnectControls( false );
            setStatus("Connected thru USB", true);
        }
        private void onSSHDisconnect()
        {
            timerUSB.Enabled = false;
            MessageBox.Show("SSH connection lost!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            timerUSB.Enabled = true;
            setConnectControls( false );
            setStatus("Not connected", false);
        }
        private void onUSBDisconnect()
        {
            timerUSB.Enabled = false;
            MessageBox.Show("USB connection lost!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            timerUSB.Enabled = true;
            setConnectControls(true);
            setStatus( "Not connected", false );
        }

        #endregion
        #region GUI Helpers

        private void setStatus(string msg, bool positive)
        {
            textStatus.Text = msg;
            if(positive) textStatus.BackColor = Color.LightGreen;
            else textStatus.BackColor = Color.MistyRose;
        }
        private void setConnectControls(bool enabled)
        {
            textIP.Enabled = enabled;
            textPwd.Enabled = enabled;
            buttonConnect.Enabled = enabled;
            groupMgr.Enabled = !enabled;
        }

        #endregion
        #region Timers
        
        private void timerUSB_Tick(object sender, EventArgs e)
        {
            if(guiConnection != iPhoneManager.ConnType.ctNone && !iphone.IsConnected)
            {
                if(guiConnection == iPhoneManager.ConnType.ctUSB)
                {
                    onUSBDisconnect();
                }
                else
                {
                    onSSHDisconnect();
                }
                guiConnection = iPhoneManager.ConnType.ctNone;
            }

            if(!iphone.IsConnected) return;
            if(iphone.connectionType == iPhoneManager.ConnType.ctUSB)
            {
                onUSBConnect();
            }
            else
            {
                onSSHConnect();
            }
            guiConnection = iphone.connectionType;
        }
        private void timerSSH_Tick(object sender, EventArgs e)
        {
            timerSSH.Enabled = false;
            imgLoading.Visible = false;
            if(guiConnection != iPhoneManager.ConnType.ctNone) return;

            setConnectControls( true );
            setStatus( "Could not connect thru SSH!", false );
        }

        #endregion
        #region Fields
        
        private iPhoneManager.ConnType guiConnection = iPhoneManager.ConnType.ctNone;
        private iPhoneManager iphone;

        #endregion

        private void buttonSynchroHttp_Click(object sender, EventArgs e)
        {
            UploadForm uForm = new UploadForm();
            uForm.setIPhone( iphone );
            uForm.ShowDialog();
        }
    }
}
