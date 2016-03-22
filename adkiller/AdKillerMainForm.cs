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
using AdKiller.Helpers;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Reflection;
namespace AdKiller
{
    public partial class AdKillerMainForm : Form
    {
        private static readonly int maxFileLenght = 10000000;
        private ModuleIniti modules;
        private RadioConfiguration RadioConf;
        private RadioStation radioStation;
        private Thread moduleInitThread;

        public AdKillerMainForm()
        {
            InitializeComponent();
            InitializeActions();
            InitializeRadio();
            modules = new ModuleIniti(this);
            string path = Assembly.GetExecutingAssembly().CodeBase;
            string path2 = Path.GetDirectoryName(path);
        }
        private void OpenFile()
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Mp3 files (*.mp3)|*.mp3|Wave files (*.wav)|*.wav|Ogg Vorbis files (*.ogg)|*.ogg|All sound files (*.mp3;*.wav)|*mp3;*.wav";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (new FileInfo(openFileDialog1.FileName).Length < maxFileLenght)
                {
                    FileTextBox.Text = Path.GetFileName(openFileDialog1.FileName);
                    isFileSelectedCondition.Value = true;
                }
                else
                {
                    MessageBox.Show("File size is bigger than 10 mb's! ");
                }
            }
        }
        private void DoPlay()
        {
            toolStripRadioStatusLabel.Text = "Connecting";
            isPlayingCondition.Value = true;
            moduleInitThread = new Thread( ()=> modules.Process(radioStation.StartJinglePath,radioStation.EndJinglePath,FileTextBox.Text));
            moduleInitThread.Start();
        }
        private void DoStop()
        {
            isPlayingCondition.Value = false;
            isRadioSelectedCondition.Value = false;
            listBoxRadio.ClearSelected();
            moduleInitThread.Abort();
            modules.Dispose();
        }
        private void SelectRadio()
        {
             radioStation = listBoxRadio.SelectedItem as RadioStation;
            if (radioStation != null)
            {
                if (!File.Exists(radioStation.StartJinglePath) || !File.Exists(radioStation.EndJinglePath))
                {
                    MessageBox.Show("Can't find jingle's! Path is wrong or file is missing. Check the radiostation.xml");
                    isRadioSelectedCondition.Value = false;
                }
                else
                {
                    FileTextBox.Text = radioStation.ConnectionString;
                    isRadioSelectedCondition.Value = true;
                }
            }
        }
        private void InitializeRadio()
        {
            RadioConf = new RadioConfiguration();
            //RadioConf.FillWithSampleData();
            //RadioConf.SaveToXml();
            RadioConf = RadioConfiguration.LoadStationsFromXml();
            RadioConf.CheckNull("Stations");
            foreach (var station in RadioConf.Stations)
            {
                listBoxRadio.Items.Add(station);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RadioStationForm radioStationForm = new RadioStationForm();
            radioStationForm.Show();
        }
        public void refreshListBox()
        {
            listBoxRadio = new ListBox();
            InitializeRadio();
        }
        public void UpdateStatusLabel(string text)
        {
            toolStripRadioStatusLabel.Text = text;
        }
        public delegate void UpdateStatusLabelCallback(string text);
    }

}
