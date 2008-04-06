namespace ArchiveMe
{
    partial class UploadForm
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
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( UploadForm ) );
            this.groupUpload = new System.Windows.Forms.GroupBox();
            this.checkSaveSettings = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textPass = new System.Windows.Forms.TextBox();
            this.textUser = new System.Windows.Forms.TextBox();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupUpload
            // 
            this.groupUpload.Controls.Add( this.checkSaveSettings );
            this.groupUpload.Controls.Add( this.label2 );
            this.groupUpload.Controls.Add( this.label1 );
            this.groupUpload.Controls.Add( this.textPass );
            this.groupUpload.Controls.Add( this.textUser );
            this.groupUpload.Location = new System.Drawing.Point( 13, 13 );
            this.groupUpload.Name = "groupUpload";
            this.groupUpload.Size = new System.Drawing.Size( 258, 110 );
            this.groupUpload.TabIndex = 0;
            this.groupUpload.TabStop = false;
            this.groupUpload.Text = "Upload messages";
            // 
            // checkSaveSettings
            // 
            this.checkSaveSettings.AutoSize = true;
            this.checkSaveSettings.Location = new System.Drawing.Point( 9, 75 );
            this.checkSaveSettings.Name = "checkSaveSettings";
            this.checkSaveSettings.Size = new System.Drawing.Size( 173, 17 );
            this.checkSaveSettings.TabIndex = 4;
            this.checkSaveSettings.Text = "Save authorization information";
            this.checkSaveSettings.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 6, 50 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 57, 13 );
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 6, 23 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 62, 13 );
            this.label1.TabIndex = 2;
            this.label1.Text = "User name:";
            // 
            // textPass
            // 
            this.textPass.Location = new System.Drawing.Point( 94, 47 );
            this.textPass.Name = "textPass";
            this.textPass.PasswordChar = '*';
            this.textPass.Size = new System.Drawing.Size( 158, 21 );
            this.textPass.TabIndex = 1;
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point( 94, 20 );
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size( 158, 21 );
            this.textUser.TabIndex = 0;
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point( 112, 153 );
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size( 159, 23 );
            this.buttonUpload.TabIndex = 4;
            this.buttonUpload.Text = "&Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler( this.buttonUpload_Click );
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point( 12, 153 );
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size( 94, 23 );
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler( this.buttonCancel_Click );
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject( "pictureBox1.Image" )));
            this.pictureBox1.Location = new System.Drawing.Point( 252, 157 );
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size( 18, 19 );
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler( this.pictureBox1_Click );
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point( 10, 126 );
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size( 69, 13 );
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status: none";
            // 
            // UploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 282, 192 );
            this.Controls.Add( this.labelStatus );
            this.Controls.Add( this.buttonUpload );
            this.Controls.Add( this.pictureBox1 );
            this.Controls.Add( this.buttonCancel );
            this.Controls.Add( this.groupUpload );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UploadForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler( this.UploadForm_FormClosed );
            this.groupUpload.ResumeLayout( false );
            this.groupUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupUpload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPass;
        private System.Windows.Forms.TextBox textUser;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkSaveSettings;
        private System.Windows.Forms.Label labelStatus;
    }
}