namespace ArchiveMe
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
            this.groupConnection = new System.Windows.Forms.GroupBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textStatus = new System.Windows.Forms.TextBox();
            this.groupMgr = new System.Windows.Forms.GroupBox();
            this.buttonReadSMS = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.smsDataSet = new ArchiveMe.SmsDataSet();
            this.timerUSB = new System.Windows.Forms.Timer( this.components );
            this.timerSSH = new System.Windows.Forms.Timer( this.components );
            this.imgLoading = new System.Windows.Forms.PictureBox();
            this.buttonSynchroHttp = new System.Windows.Forms.Button();
            this.groupConnection.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupMgr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // groupConnection
            // 
            this.groupConnection.Controls.Add( this.imgLoading );
            this.groupConnection.Controls.Add( this.buttonConnect );
            this.groupConnection.Controls.Add( this.textPwd );
            this.groupConnection.Controls.Add( this.label3 );
            this.groupConnection.Controls.Add( this.textIP );
            this.groupConnection.Controls.Add( this.label2 );
            this.groupConnection.Location = new System.Drawing.Point( 13, 13 );
            this.groupConnection.Name = "groupConnection";
            this.groupConnection.Size = new System.Drawing.Size( 226, 106 );
            this.groupConnection.TabIndex = 0;
            this.groupConnection.TabStop = false;
            this.groupConnection.Text = "SSH Connection";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point( 75, 73 );
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size( 140, 23 );
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "&SSH Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler( this.buttonConnect_Click );
            // 
            // textPwd
            // 
            this.textPwd.Location = new System.Drawing.Point( 75, 46 );
            this.textPwd.Name = "textPwd";
            this.textPwd.PasswordChar = '*';
            this.textPwd.Size = new System.Drawing.Size( 140, 21 );
            this.textPwd.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 11, 49 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 57, 13 );
            this.label3.TabIndex = 3;
            this.label3.Text = "password:";
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point( 75, 19 );
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size( 140, 21 );
            this.textIP.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 11, 22 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 60, 13 );
            this.label2.TabIndex = 2;
            this.label2.Text = "ip address:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add( this.textStatus );
            this.groupBox2.Location = new System.Drawing.Point( 14, 125 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 226, 56 );
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection Status";
            // 
            // textStatus
            // 
            this.textStatus.BackColor = System.Drawing.Color.MistyRose;
            this.textStatus.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)) );
            this.textStatus.Location = new System.Drawing.Point( 6, 20 );
            this.textStatus.Name = "textStatus";
            this.textStatus.ReadOnly = true;
            this.textStatus.Size = new System.Drawing.Size( 209, 21 );
            this.textStatus.TabIndex = 0;
            this.textStatus.TabStop = false;
            this.textStatus.Text = "Not connected";
            this.textStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupMgr
            // 
            this.groupMgr.Controls.Add( this.buttonSynchroHttp );
            this.groupMgr.Controls.Add( this.buttonReadSMS );
            this.groupMgr.Location = new System.Drawing.Point( 246, 13 );
            this.groupMgr.Name = "groupMgr";
            this.groupMgr.Size = new System.Drawing.Size( 166, 139 );
            this.groupMgr.TabIndex = 3;
            this.groupMgr.TabStop = false;
            this.groupMgr.Text = "SMS manager";
            // 
            // buttonReadSMS
            // 
            this.buttonReadSMS.Location = new System.Drawing.Point( 6, 20 );
            this.buttonReadSMS.Name = "buttonReadSMS";
            this.buttonReadSMS.Size = new System.Drawing.Size( 150, 23 );
            this.buttonReadSMS.TabIndex = 0;
            this.buttonReadSMS.Text = "&Synchronize";
            this.buttonReadSMS.UseVisualStyleBackColor = true;
            this.buttonReadSMS.Click += new System.EventHandler( this.buttonReadSMS_Click );
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point( 246, 158 );
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size( 166, 23 );
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "&Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler( this.buttonExit_Click );
            // 
            // smsDataSet
            // 
            this.smsDataSet.DataSetName = "SmsDataSet";
            this.smsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // timerUSB
            // 
            this.timerUSB.Interval = 1000;
            this.timerUSB.Tick += new System.EventHandler( this.timerUSB_Tick );
            // 
            // timerSSH
            // 
            this.timerSSH.Interval = 10000;
            this.timerSSH.Tick += new System.EventHandler( this.timerSSH_Tick );
            // 
            // imgLoading
            // 
            this.imgLoading.Image = ((System.Drawing.Image)(resources.GetObject( "imgLoading.Image" )));
            this.imgLoading.Location = new System.Drawing.Point( 27, 76 );
            this.imgLoading.Name = "imgLoading";
            this.imgLoading.Size = new System.Drawing.Size( 41, 20 );
            this.imgLoading.TabIndex = 7;
            this.imgLoading.TabStop = false;
            this.imgLoading.Visible = false;
            // 
            // buttonSynchroHttp
            // 
            this.buttonSynchroHttp.Location = new System.Drawing.Point( 6, 49 );
            this.buttonSynchroHttp.Name = "buttonSynchroHttp";
            this.buttonSynchroHttp.Size = new System.Drawing.Size( 150, 23 );
            this.buttonSynchroHttp.TabIndex = 1;
            this.buttonSynchroHttp.Text = "&Upload to server";
            this.buttonSynchroHttp.UseVisualStyleBackColor = true;
            this.buttonSynchroHttp.Click += new System.EventHandler( this.buttonSynchroHttp_Click );
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 423, 190 );
            this.Controls.Add( this.buttonExit );
            this.Controls.Add( this.groupMgr );
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.groupConnection );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "ArchiveMe iPhone SMS archiver";
            this.Load += new System.EventHandler( this.MainForm_Load );
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler( this.MainForm_FormClosed );
            this.groupConnection.ResumeLayout( false );
            this.groupConnection.PerformLayout();
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout();
            this.groupMgr.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize)(this.smsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoading)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.GroupBox groupConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textPwd;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textStatus;
        private System.Windows.Forms.GroupBox groupMgr;
        private System.Windows.Forms.Button buttonReadSMS;
        private System.Windows.Forms.Button buttonExit;
        private SmsDataSet smsDataSet;
        private AddrDataSet addrDataSet;
        private iSmsDataSet iSmsDataSet;
        private System.Windows.Forms.Timer timerUSB;
        private System.Windows.Forms.Timer timerSSH;
        private System.Windows.Forms.PictureBox imgLoading;
        private System.Windows.Forms.Button buttonSynchroHttp;
    }
}

