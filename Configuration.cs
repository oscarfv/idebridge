using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace IdeBridge
{
    [Serializable]
    public class Configuration // : ConfigurationCustomTypeDescriptor
    {
        // The configurations properties:
        [CategoryAttribute("Network"), DescriptionAttribute("The port on which the server will listen incomming connections.")]
        [DefaultValueAttribute(8989)]
        public int PortNumber { get; set; }

        [System.ComponentModel.Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("Solution"),
         DescriptionAttribute("The solution file can be set from emacs with the function ide-bridge-set-solution.")]
        public string Solution { get; set; }


        [CategoryAttribute("Adjustements"), DescriptionAttribute("The X offset to add to the location where the popup is shown.")]
        [DefaultValueAttribute(0)]
        public int XOffset { get; set; }

        [CategoryAttribute("Adjustements"), DescriptionAttribute("The Y offset to add to the location where the popup is shown. It's difficult to calculate the exact Y position of point in emacs (du to menu-bar and toolbar) so you have to adjust the Y offset here.")]
        [DefaultValueAttribute(60)]
        public int YOffset { get; set; }

        public Configuration()
        {
            _instance = this;

            _ins

            // Default values:
            XOffset = 0;
            YOffset = 60;
            PortNumber = 8989;
        }

        // Path/Load/Save stuff :
        public const string Version = "1.0.0";
        public const string Name = "IdeBridge";
        public const string Company = "raf";

        public static string ConfigPath
        {
            get
            {
                return string.Format("{0}\\{1}\\{2}\\{3}",
                                     Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                     Company, Name, Version);
            }
        }

        public static string FileName
        {
            get
            {
                return string.Format("{0}\\config.xml", ConfigPath);
            }
        }

        private static Configuration _instance = null;
        public static Configuration Get()
        {
            if (_instance == null)
            {
                _instance = Configuration.Load();
            }
            return _instance;
        }

        private static Configuration Load()
        {
            if (File.Exists(FileName))
            {
                using (TextReader reader = new StreamReader(FileName, Encoding.Default))
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                        var result = serializer.Deserialize(reader) as Configuration;
                        return result;
                    }
                    catch (InvalidOperationException e)
                    {

                        Logger.Exception( e);
                    }
                }
            }
            return new Configuration();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(ConfigPath);
                using (TextWriter writer = new StreamWriter(FileName, false, Encoding.Default))
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(writer, this);
                }
            }
            catch (InvalidOperationException e)
            {
                Logger.Exception(e);
            }
        }
    }
}