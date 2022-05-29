using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace TranceBox
{
    static class SettingsShared
    {
        public static string GetMyAppDataDir(string appName)
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string myAppDataDir = Path.Combine(appDataDir, appName);

            if (Directory.Exists(myAppDataDir) == false)
            {
                Directory.CreateDirectory(myAppDataDir);
            }

            return myAppDataDir;
        }
    }

    class SettingsReader
    {
        Dictionary<string, string> _settings;

        public SettingsReader(string appName, string fileName)
        {
            _settings = new Dictionary<string, string>();

            string path = Path.Combine(SettingsShared.GetMyAppDataDir(appName), fileName);
            if (!File.Exists(path))
            {
                return;
            }

            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                string line, name, val;
                int pos;

                while ((line = sr.ReadLine()) != null)
                {
                    pos = line.IndexOf('=');
                    if (pos != -1)
                    {
                        name = line.Substring(0, pos);
                        val = line.Substring(pos + 1);

                        if (!_settings.ContainsKey(name))
                        {
                            _settings.Add(name, val);
                        }
                    }
                }
            }
        }

        public string Load(string name)
        {
            return _settings.ContainsKey(name) ? _settings[name] : null;
        }
    }

    class SettingsWriter
    {
        StreamWriter _sw;

        public SettingsWriter(string appName, string fileName)
        {
            string path = Path.Combine(SettingsShared.GetMyAppDataDir(appName), fileName);

            _sw = new StreamWriter(path, false, Encoding.UTF8);
        }

        public void Save(string name, string value)
        {
            _sw.WriteLine(name + "=" + value);
        }

        public void Close()
        {
            _sw.Close();
        }
    }
}
