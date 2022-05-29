using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Runtime.InteropServices;

namespace TranceBox
{
    public partial class Form4 : Form
    {
        SpeechSynthesizer ss;
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        public Form4()
        {
            InitializeComponent();
            // By the default set the volume to 0
            uint CurrVol = 0;
            // At this point, CurrVol gets assigned the volume
            waveOutGetVolume(IntPtr.Zero, out CurrVol);
            // Calculate the volume
            ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
            // Get the volume on a scale of 1 to 10 (to fit the trackbar)
            trackBar1.Value = CalcVol / (ushort.MaxValue / 10);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ss = new SpeechSynthesizer();
        }
         
      
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "textfile | *.txt";
            openFileDialog1.ShowDialog();
            txtfilename.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "wavefiles | *.wav";
            saveFileDialog1.ShowDialog();
            txttarget.Text = saveFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtfilename.Text == "")
            {
                MessageBox.Show("Browse a valid text file location");
            }
            else
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(txtfilename.Text);
                txtmessage.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtmessage.Text == "")
                MessageBox.Show("Please Enter/Import text");
            else
            {
                ss = new SpeechSynthesizer();
                ss.SpeakAsync(txtmessage.Text);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtmessage.Text == "")
            {
                MessageBox.Show("Please enter/import text first.");
            }
            else
            {
                try
                {
                    ss = new SpeechSynthesizer();
                    ss.SetOutputToWaveFile(txttarget.Text);
                    ss.Speak(txtmessage.Text);
                    ss.SetOutputToDefaultAudioDevice();
                    MessageBox.Show("VOICE SUCCESSFULLY RECORDED...", this.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Calculate the volume that's being set
            int NewVolume = ((ushort.MaxValue / 10) * trackBar1.Value);
            // Set the same volume for both the left and the right channels
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            // Set the volume
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        private void sourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "textfile | *.txt";
            openFileDialog1.ShowDialog();
            txtfilename.Text = openFileDialog1.FileName;
        }

        private void targetFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "wavefiles | *.wav";
            saveFileDialog1.ShowDialog();
            txttarget.Text = saveFileDialog1.FileName;
        }

        private void exitToTranceboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtfilename.Text == "")
            {
                MessageBox.Show("Browse a valid text file location");
            }
            else 
           
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(txtfilename.Text);
                txtmessage.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void speakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtmessage.Text == "")
                MessageBox.Show("Please Enter/Import text");
            else
            {
                ss = new SpeechSynthesizer();
                ss.SpeakAsync(txtmessage.Text);
            }
        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtmessage.Text == "")
            {
                MessageBox.Show("Please enter/import text first.");
            }
            else
            {
                try
                {

                    ss.SetOutputToWaveFile(txttarget.Text);
                    ss.Speak(txtmessage.Text);
                    ss.SetOutputToDefaultAudioDevice();
                    MessageBox.Show("VOICE SUCCESSFULLY RECORDED...", this.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help4 h4 = new Help4();
            h4.Show();
        }




 

 

        

  


    }
}
