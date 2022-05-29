using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TranceBox
{
    public partial class Form3 : Form
    {
        Thread _statusThread;

		public Form3()
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

       
		private void frmMain_Load(object sender, EventArgs e)
        {
			LoadSettings();
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e) 
        {
			SaveSettings();
		}

        private void button1_Click_1(object sender, EventArgs e)
        {

            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "flv files|*.flv";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBoxOpen.Text = fd.FileName;
                frmMain_callstatus(fd.FileName);

            } 
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "flv files|*.flv";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBoxOpen.Text = fd.FileName;
                frmMain_callstatus(fd.FileName);

            } 
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help3 h3 = new Help3();
            h3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
