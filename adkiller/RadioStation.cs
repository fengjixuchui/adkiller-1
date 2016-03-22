using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SerializeRadioStation
{
    [XmlRoot(ElementName = "Item")]
    public class RadioStation
    {
        public RadioStation() { }
        public RadioStation(string name, string connection, string startJinglePath, string endJinglePath, float advTime)
        {
            Name = name;
            ConnectionString = connection;
            StartJinglePath = startJinglePath;
            EndJinglePath = endJinglePath;
        }
        public override string ToString()
        {
            return Name;
        }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string StartJinglePath { get; set; }
        public string EndJinglePath { get; set; }

    }

}
