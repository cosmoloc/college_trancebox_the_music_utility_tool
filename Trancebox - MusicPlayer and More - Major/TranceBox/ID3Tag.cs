using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TranceBox
{
    class ID3Tag
    {
        #region Declarations

        private string songTitle;
        private string artist;
        private string album;
        private int year;
        private string comment;
        private string genre;
        private int songnumber;

        #endregion

        #region Properties
        public string SongTitle
        {
            get
            {
                return songTitle;
            }
        }

        public string Artist
        {
            get
            {
                return artist;
            }
        }

        public string Album
        {
            get
            {
                return album;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }
        }

        public int TitleNumber
        {
            get
            {
                return songnumber;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
        }

        public string Genre
        {
            get
            {
                return genre;
            }
        }

        public ID3Tag()
        {
            songTitle = "Not Set";
            artist = "Not Set";
            album = "Not Set";
            year = -1; ;
            comment = "Not Set";
            genre = "Not Set";
            songnumber = -1;
        }

        #endregion

        #region Public Functions
        public void ReadTAG(string filePath)
        {


            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(file);

            sr.BaseStream.Seek(-128, SeekOrigin.End);
            char[] buffer = new char[128];


            sr.ReadBlock(buffer, 0, 128);
            string tag = new string(buffer);

            if (tag.Substring(0, 3).Equals("TAG"))
            {
                songTitle = tag.Substring(3, 30).Replace('\0', ' ').Trim();
                artist = tag.Substring(33, 30).Replace('\0', ' ').Trim();
                album = tag.Substring(63, 30).Replace('\0', ' ').Trim();
                year = GetNumber(tag.Substring(93, 4).Replace('\0', ' ').Trim());

                comment = tag.Substring(97, 28).Replace('\0', ' ').Trim();
                songnumber = GetNumber(tag.Substring(126, 1).Replace('\0', ' ').Trim());
                genre = GetGenre(tag.Substring(127, 1).Replace('\0', ' ').Trim());
            }

            sr.Close();
            file.Close();

        }

        #endregion

        #region Private Functions
        private string GetGenre(string genreString)
        {
            try
            {
                string[] genreList = new string[]
            {
                "Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz",
                "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno",
                "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno",
                "Ambient", "Trip-Hop", "Vocal", "Jazz&Funk", "Fusion", "Trance", "Classical", "Instrumental",
                "Acid", "House", "Game", "Sound Clip", "Gospel", "Noise", "Alternativ Rock", "Bass", "Soul",
                "Punk", "Space", "Meditative", "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic",
                "Darkwave", "Techno-Industrial", "Electronic", "Pop-Folk", "Eurodance", "Dream", "Southern Rock",
                "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", "Pop/Funk", "Jungle", "Native US",
                "Carbaret", "New Wave", "Psychedelic", "Rave", "Showtunes", "Trailer", "Lo-Fi", "Tribal", "Acid Punk",
                "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", "Folk",
                "Folk-Rock", "National Folk", "Swing", "Fast Fusion", "Bebop", "Latin", "Revival", "Celtic",
                "Bluegrass", "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychadelic Rock", "Symphonic Rock",
                "Slow Rock", "Big Band", "Chorus", "Easy Listening", "Acoustic", "Homour", "Speech", "Chanson",
                "Opera", "Chamber Music", "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove",
                "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", "Power Ballad",
                "Rythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "Acapella", "Euor-House",
                "Dance Hall", "Goa", "Drum & Bass", "Club-House", "Hardcore", "Terror", "Indie", "BritPop",
                "Negerpunk", "Polsk Punk", "Beat", "Christian Gangsta", "Heavy Metal", "Black Metal", "Crossover",
                "Contemporary", "Christian Rock", "Merengue", "Salsa", "Trash Metal", "Anime", "JPop", "SynthPop"
            };

                int ret;

                if (genreString.Length > 0)
                {
                    ret = Convert.ToInt32(genreString[0]);
                    return genreList[ret];
                }
                else
                    return "Not Set";
            }
            catch
            {
                return "Not Set";
            }


        }

        private int GetNumber(string strNumber)
        {
            if (strNumber.Length > 0)
            {

                return Convert.ToInt32(strNumber[0]);
            }
            else
                return -1;
        }

        #endregion
    }
}
