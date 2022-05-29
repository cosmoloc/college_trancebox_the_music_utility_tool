namespace JDP {
	partial class frmMain {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.lblInstructions = new System.Windows.Forms.Label();
            this.grpExtract = new System.Windows.Forms.GroupBox();
            this.chkAudio = new System.Windows.Forms.CheckBox();
            this.chkTimeCodes = new System.Windows.Forms.CheckBox();
            this.chkVideo = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxOpen = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.grpExtract.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.Location = new System.Drawing.Point(8, 8);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(361, 56);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Output files are written in the same folder as the FLVs.";
            this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpExtract
            // 
            this.grpExtract.Controls.Add(this.chkAudio);
            this.grpExtract.Controls.Add(this.chkTimeCodes);
            this.grpExtract.Controls.Add(this.chkVideo);
            this.grpExtract.Location = new System.Drawing.Point(12, 131);
            this.grpExtract.Name = "grpExtract";
            this.grpExtract.Size = new System.Drawing.Size(357, 114);
            this.grpExtract.TabIndex = 1;
            this.grpExtract.TabStop = false;
            this.grpExtract.Text = "Extract:";
            // 
            // chkAudio
            // 
            this.chkAudio.Checked = true;
            this.chkAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAudio.Location = new System.Drawing.Point(32, 65);
            this.chkAudio.Name = "chkAudio";
            this.chkAudio.Size = new System.Drawing.Size(80, 17);
            this.chkAudio.TabIndex = 2;
            this.chkAudio.Text = "&Audio";
            this.chkAudio.UseVisualStyleBackColor = true;
            // 
            // chkTimeCodes
            // 
            this.chkTimeCodes.Checked = true;
            this.chkTimeCodes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimeCodes.Location = new System.Drawing.Point(32, 42);
            this.chkTimeCodes.Name = "chkTimeCodes";
            this.chkTimeCodes.Size = new System.Drawing.Size(80, 17);
            this.chkTimeCodes.TabIndex = 1;
            this.chkTimeCodes.Text = "&Timecodes";
            this.chkTimeCodes.UseVisualStyleBackColor = true;
            // 
            // chkVideo
            // 
            this.chkVideo.Checked = true;
            this.chkVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVideo.Location = new System.Drawing.Point(32, 19);
            this.chkVideo.Name = "chkVideo";
            this.chkVideo.Size = new System.Drawing.Size(80, 17);
            this.chkVideo.TabIndex = 0;
            this.chkVideo.Text = "&Video";
            this.chkVideo.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxOpen
            // 
            this.textBoxOpen.Location = new System.Drawing.Point(83, 70);
            this.textBoxOpen.Name = "textBoxOpen";
            this.textBoxOpen.Size = new System.Drawing.Size(270, 20);
            this.textBoxOpen.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(443, 284);
            this.Controls.Add(this.textBoxOpen);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpExtract);
            this.Controls.Add(this.lblInstructions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLV Extract";
            this.TopMost = true;
            this.grpExtract.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label lblInstructions;
		private System.Windows.Forms.GroupBox grpExtract;
		private System.Windows.Forms.CheckBox chkAudio;
		private System.Windows.Forms.CheckBox chkTimeCodes;
		private System.Windows.Forms.CheckBox chkVideo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}