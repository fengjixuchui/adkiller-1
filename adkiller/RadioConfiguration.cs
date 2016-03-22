using SerializeRadioStation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdKiller
{
    [XmlRoot(ElementName = "Configuration")]
    public class RadioConfiguration
    {
        [XmlElement("ListName")]
        public string ConfigurationName { get; set; }
        [XmlArray("Stations")]
        public List<RadioStation> Stations { get; private set; }

        public RadioConfiguration()
        {
            Stations = new List<RadioStation>();

        }

        public void SaveToXml()
        {
            FileStream fs = new FileStream("..\\..\\Data\\radiostations.xml", FileMode.Create, FileAccess.ReadWrite);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RadioConfiguration));
                serializer.Serialize(fs, this);
            }
            finally
            {
                fs.Close();
            }
        }
        public void FillWithSampleData()
        {
            RadioStation trojka = new RadioStation("Trójka", "http://localhost:8080", @"C:\Users\kburdzinski\Documents\Visual Studio 2013\Projects\adkiller\adkiller\adkiller\Data\Jingle\startJingleTrojka48.mp3", @"C:\Users\kburdzinski\Documents\Visual Studio 2013\Projects\adkiller\adkiller\adkiller\Data\Jingle\endJingleTrojka48.mp3", 10);

            this.ConfigurationName = "Polskie radia";
            this.Stations.Add(trojka);
        }



        // Static method to load data
        public static RadioConfiguration LoadStationsFromXml()
        {
            string filepath = "Data\\radiostations.xml";
#if DEBUG
            filepath = "..\\..\\Data\\radiostations.xml";
#endif


            if (File.Exists(filepath))
            {

                var filestream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                RadioConfiguration stations = null;
                try
                {
                    var deserializer = new XmlSerializer(typeof(RadioConfiguration));
                    stations = (RadioConfiguration)deserializer.Deserialize(filestream);

                }
                finally
                {
                    filestream.Close();
                }
                return stations;
            }
            return null;
        }
    }
}
