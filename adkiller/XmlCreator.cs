using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerializeRadioStation;
using System.Xml.Serialization;
using System.IO;
using adkiller.Helpers;

namespace adkiller
{
    public partial class XmlCreator : Form
    {

        private ListOfStations radioList;
        public XmlCreator()
        {
            InitializeComponent();
        }
        public XmlCreator(ListOfStations actualList)
        {
            InitializeComponent();
            actualList.CheckNull("Radio staiton list");
            // radioList = new ListOfStations();
            radioList = actualList;
        }

        private void RadioFileDialogButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            //openDialog.Title = "Open mp3 / wav file";
            //openDialog.Filter = "mp3 files|*.mp3|wav files|*.wav";
            //openDialog.InitialDirectory = "@C:\\";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                RadioStartJingleTextBox.Text = openDialog.FileName.ToString();
            }
            else
            {
               MessageBox.Show("Bad file format");
            }
        }

        private void RadioFileDialogButton2_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openDialog = new OpenFileDialog();
            //openDialog.Title = "Open mp3 / wav file";
            //openDialog.Filter = "mp3 files|*.mp3|wav files|*.wav";
            //openDialog.InitialDirectory = "@C:\\";
            //if (openDialog.ShowDialog() == DialogResult.OK)
            //{
            //    RadioEndJingleTextBox.Text = openDialog.FileName.ToString();
            //}
            //else
            //{
            //    throw new FormatException("Bad file format");
            //}
        }

        private void RadioCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RadioStationSaveButton_Click(object sender, EventArgs e)
        {
            RadioStation newStation = new RadioStation(RadioNameTextBox.Text,
                RadioConnectionTextBox.Text,
                RadioStartJingleTextBox.Text,
                RadioEndJingleTextBox.Text,
                float.Parse(RadioAdvertTimeTextBox.Text));

            radioList.listName = "Default name";
            radioList.stations.Add(newStation);
			try{
            XmlSerializer serializer = new XmlSerializer(typeof(ListOfStations));
            FileStream fs = new FileStream("../../Data/radiostations.xml", FileMode.Create, FileAccess.ReadWrite);
            serializer.Serialize(fs, radioList);
			}
			finally
			{
            fs.Close();
            this.Close();
			}
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XmlCreator
            // 
            this.ClientSize = new System.Drawing.Size(347, 306);
            this.Name = "XmlCreator";
            this.ResumeLayout(false);

        }

    }
}
