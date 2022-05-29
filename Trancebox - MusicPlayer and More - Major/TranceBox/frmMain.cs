using System;
using System.Threading;
using System.Windows.Forms;

namespace JDP {
	public partial class frmMain : Form
    {
		Thread _statusThread;

		public frmMain()
        {
			InitializeComponent();
			Program.SetFontAndScaling(this);
		}

		private void LoadSettings() 
        {
			SettingsReader sr = new SettingsReader("FLV Extract", "settings.txt");
			string val;

			if ((val = sr.Load("ExtractVideo")) != null) 
            {
				chkVideo.Checked = (val != "0");
			}
			if ((val = sr.Load("ExtractTimeCodes")) != null)
            {
				chkTimeCodes.Checked = (val != "0");
			}
			if ((val = sr.Load("ExtractAudio")) != null) 
            {
				chkAudio.Checked = (val != "0");
			}
		}

		private void SaveSettings() 
        {
			SettingsWriter sw = new SettingsWriter("FLV Extract", "settings.txt");

			sw.Save("ExtractVideo", chkVideo.Checked ? "1" : "0");
			sw.Save("ExtractTimeCodes", chkTimeCodes.Checked ? "1" : "0");
			sw.Save("ExtractAudio", chkAudio.Checked ? "1" : "0");

			sw.Close();
		}

        //private void frmMain_DragEnter(object sender, DragEventArgs e) {
        //    if ((_statusThread != null) && _statusThread.IsAlive) return;

        //    if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
        //        e.Effect = DragDropEffects.Copy;
        //    }
        //}

		private void frmMain_callstatus(string s)
        {
			if ((_statusThread != null) && _statusThread.IsAlive) return;

            //if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
           
                    string[] paths = {s};
                    //string[] paths = new string[s.Length];
                    //for(int i = 0; i < s.Length; i++) {
                    //paths[i] = s[i].ToString();
           

				frmStatus statusForm = new frmStatus(paths,chkVideo.Checked, chkAudio.Checked, chkTimeCodes.Checked);
				_statusThread = new Thread((ThreadStart)delegate() 
               {
					Invoke((MethodInvoker)delegate() 
                    {
						bool topMost = TopMost;
						TopMost = false;
						statusForm.ShowDialog();
						TopMost = topMost;
					});
				});
				_statusThread.Start();
			}

        private void button1_Click(object sender, EventArgs e)
        {
             OpenFileDialog fd = new OpenFileDialog();
            //fd.Filter = "mp3 files|*.mp3";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBoxOpen.Text = fd.FileName;
                frmMain_callstatus(fd.FileName);

            } 
        }
		

		private void frmMain_Load(object sender, EventArgs e)
        {
			LoadSettings();
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e) 
        {
			SaveSettings();
		}
    }

	}

