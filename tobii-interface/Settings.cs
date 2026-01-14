using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tobii.Research;

namespace tobii_interface
{
    public class Settings
    {
        public Rectangle LastPosition { get; set; } = Rectangle.Empty;

        public Settings() { }

        public void Save()
        {
            KLib.FileIO.XmlSerialize<Settings>(this, FilePath);
        }

        public static Settings Restore()
        {
            Settings settings = new Settings();
            if (File.Exists(FilePath))
            {
                settings = KLib.FileIO.XmlDeserialize<Settings>(FilePath);
            }
            return settings;
        }

        private static string FilePath
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "EPL",
                    "TobiiInterfaceSettings.xml");
            }
        }
    }
}
