using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TranceBox;


namespace TranceBox
{
    public partial class Form2 : Form
    {
        TranceBox.MP3Player mplayer;
        private WaveInRecorder _recorder;
        private byte[] _recorderBuffer;
        private WaveOutPlayer _player;
        private byte[] _playerBuffer;
        private FifoStream _stream;
        private WaveFormat _waveFormat;
        private AudioFrame _audioFrame;
        private int _audioSamplesPerSecond = 44100;
        private int _audioFrameSize = 16384;
        private byte _audioBitsPerSample = 16;
        private byte _audioChannels = 2;
        private bool _isPlayer = false;
        private bool _isTest = false;
        public Form2()
        {
            InitializeComponent();
            mplayer = new MP3Player();
            mplayer.OpenFile += new MP3Player.OpenFileEventHandler(mplayer_OpenFile);
            mplayer.PlayFile += new MP3Player.PlayFileEventHandler(mplayer_PlayFile);
            mplayer.StopFile += new MP3Player.StopFileEventHandler(mplayer_StopFile);
            mplayer.PauseFile += new MP3Player.PauseFileEventHandler(mplayer_PauseFile);
      if (WaveNative.waveInGetNumDevs() == 0)
            {
                textBox1.AppendText(DateTime.Now.ToString() + " : There are no audio devices available\r\n");
            }
            else
            {
                if (_isPlayer == true)
                    _stream = new FifoStream();
                _audioFrame = new AudioFrame(_isTest);
                Start();
            }
        }
        private void Start()
        {
            Stop();
            try
            {
                _waveFormat = new WaveFormat(_audioFrameSize, _audioBitsPerSample, _audioChannels);
                _recorder = new WaveInRecorder(0, _waveFormat, _audioFrameSize * 2, 3, new BufferDoneEventHandler(DataArrived));
                if (_isPlayer == true)
                    _player = new WaveOutPlayer(-1, _waveFormat, _audioFrameSize * 2, 3, new BufferFillEventHandler(Filler));
                textBox1.AppendText(DateTime.Now.ToString() + " : Audio device initialized\r\n");
                textBox1.AppendText(DateTime.Now.ToString() + " : Audio device polling started\r\n");
                textBox1.AppendText(DateTime.Now + " : Samples per second = " + _audioSamplesPerSecond.ToString() + "\r\n");
                textBox1.AppendText(DateTime.Now + " : Frame size = " + _audioFrameSize.ToString() + "\r\n");
                textBox1.AppendText(DateTime.Now + " : Bits per sample = " + _audioBitsPerSample.ToString() + "\r\n");
                textBox1.AppendText(DateTime.Now + " : Channels = " + _audioChannels.ToString() + "\r\n");
            }
            catch (Exception ex)
            {
                textBox1.AppendText(DateTime.Now + " : Audio exception\r\n" + ex.ToString() + "\r\n");
            }
        }

        private void Stop()
        {
            if (_recorder != null)
                try
                {
                    _recorder.Dispose();
                }
                finally
                {
                    _recorder = null;
                }
            if (_isPlayer == true)
            {
                if (_player != null)
                    try
                    {
                        _player.Dispose();
                    }
                    finally
                    {
                        _player = null;
                    }
                _stream.Flush(); // clear all pending data
            }
        }

        private void Filler(IntPtr data, int size)
        {
            if (_isPlayer == true)
            {
                if (_playerBuffer == null || _playerBuffer.Length < size)
                    _playerBuffer = new byte[size];
                if (_stream.Length >= size)
                    _stream.Read(_playerBuffer, 0, size);
                else
                    for (int i = 0; i < _playerBuffer.Length; i++)
                        _playerBuffer[i] = 0;
                System.Runtime.InteropServices.Marshal.Copy(_playerBuffer, 0, data, size);
            }
        }

        private void DataArrived(IntPtr data, int size)
        {
            if (_recorderBuffer == null || _recorderBuffer.Length < size)
                _recorderBuffer = new byte[size];
            if (_recorderBuffer != null)
            {
                System.Runtime.InteropServices.Marshal.Copy(data, _recorderBuffer, 0, size);
                if (_isPlayer == true)
                    _stream.Write(_recorderBuffer, 0, _recorderBuffer.Length);
                _audioFrame.Process(ref _recorderBuffer);
                //_audioFrame.RenderTimeDomain(ref pictureMp3);
                _audioFrame.RenderFrequencyDomain(ref pictureBox2);
            }
        }
        void mplayer_OpenFile(Object sender, OpenFileEventArgs e)
        {
            trackBar1.Maximum = (int)(mplayer.AudioLength / 1000);
            this.Text = e.FileName;
            trackBar1.Value = 0;
            timer1.Enabled = false;
        }

        void mplayer_StopFile(Object sender, StopFileEventArgs e)
        {
            this.Text = "Stopped";
            timer1.Enabled = false;
            trackBar1.Value = 0;
        }

        void mplayer_PauseFile(Object sender, PauseFileEventArgs e)
        {
            this.Text = "Paused";
            timer1.Enabled = false;
        }

        void mplayer_PlayFile(Object sender, PlayFileEventArgs e)
        {
            int second;
            int minute;
            trackBar1.Maximum = (int)(mplayer.AudioLength / 1000);
            second = trackBar1.Maximum;
            minute = second / 60;
            second = second % 60;
            lblTime.Text = minute.ToString() + ":" + second.ToString();
            this.Text = "Playing " + mplayer.FileName;
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            //fd.Filter = "mp3 files|*.mp3";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBoxOpen.Text = fd.FileName;
                mplayer.Open(fd.FileName);

            }
            
            pictureMan.Visible = true;
            pictureMan2.Visible = false;
            textBox2.Visible = true;
            textBox1.Visible = false;
            string path = textBoxOpen.Text;
            ID3Tag id = new ID3Tag();
            id.ReadTAG(path);
            songTitle.Text = id.SongTitle;
            artist.Text = id.Artist;
            year.Text = id.Year.ToString();
            album.Text = id.Album;
            comment.Text = id.Comment;
            genre.Text = id.Genre;
            snumber.Text = id.TitleNumber.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxOpen.Text == "")
                MessageBox.Show("Please Selesct a file first");
            else
            {
                pictureMan.Visible = false;
                pictureMan2.Visible = true;
                textBox2.Visible = false;
                textBox1.Visible = true;
                mplayer.Play();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxOpen.Text == "")
                MessageBox.Show("Please Selesct a file first");
            else
            {
                mplayer.Pause();
                pictureMan.Visible = true;
                pictureMan2.Visible = false;
                textBox2.Visible = true;
                textBox1.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxOpen.Text == "")
                MessageBox.Show("Please Selesct a file first");
            else
            {
                mplayer.Stop();
                lblCurr.Text = "00:00";
                pictureMan.Visible = true;
                pictureMan2.Visible = false;
                textBox2.Visible = true;
                textBox1.Visible = false;
            }
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            mplayer.Seek((ulong)(trackBar1.Value * 1000));
        }

        private void trackBar2_Scroll_1(object sender, EventArgs e)
        {
            mplayer.Balance = trackBar2.Value;
        }

        private void trackBar3_Scroll_1(object sender, EventArgs e)
        {
            mplayer.VolumeAll = trackBar3.Value;
        }

        private void trackBar4_Scroll_1(object sender, EventArgs e)
        {
            mplayer.VolumeBass = trackBar4.Value;
        }

        private void trackBar5_Scroll_1(object sender, EventArgs e)
        {
            mplayer.VolumeTreble = trackBar5.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mplayer.MuteAll = checkBox1.Checked;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int seconds, minutes;
            trackBar1.Value = (int)(mplayer.CurrentPosition / 1000);
            seconds = trackBar1.Value;
            minutes = seconds / 60;
            seconds = seconds % 60;
            lblCurr.Text = minutes.ToString() + ":" + seconds.ToString();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            //fd.Filter = "mp3 files|*.mp3";
            //fd.Filter = "Media Files (*.wmv;*.png;*.jpg)|*.bmp;*.png;*.jpg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBoxOpen.Text = fd.FileName;
                mplayer.Open(fd.FileName);

            }
            string path = textBoxOpen.Text;
            ID3Tag id = new ID3Tag();
            id.ReadTAG(path);
            songTitle.Text = id.SongTitle;
            artist.Text = id.Artist;
            year.Text = id.Year.ToString();
            album.Text = id.Album;
            comment.Text = id.Comment;
            genre.Text = id.Genre;
            snumber.Text = id.TitleNumber.ToString();
            pictureMan.Visible = true;
            pictureMan2.Visible = false;
            textBox2.Visible = true;
            textBox1.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mplayer.Stop();
            Application.Exit();
        }

        private void exitToTranceBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mplayer.Stop();
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxOpen.Text == "")
                MessageBox.Show("Please Selesct a file first");
            else
            {
                mplayer.Play();
                pictureMan.Visible = false;
                pictureMan2.Visible = true;
                textBox2.Visible = false;
                textBox1.Visible = true;
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxOpen.Text == "")
                MessageBox.Show("Please Selesct a file first");
            else
            {
                mplayer.Pause();
                pictureMan.Visible = true;
                pictureMan2.Visible = false;
                textBox2.Visible = true;
                textBox1.Visible = false;
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxOpen.Text == "")
                MessageBox.Show("Please Selesct a file first");
            else
            {
                mplayer.Stop();
                lblCurr.Text = "00:00";
                pictureMan.Visible = true;
                pictureMan2.Visible = false;
                textBox2.Visible = true;
                textBox1.Visible = false;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help2 h2 = new Help2();
            h2.Show();
        }

    }
}
