using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdKiller.Helpers;
using SerializeRadioStation;

namespace AdKiller
{
    public partial class RadioStationForm : Form
    {
        private RadioConfiguration radioConf;
        private RadioStation radioStation;
        public RadioStationForm()
        {
            InitializeComponent();
            InitializeRadioStation();
            InitializeActions();
        }

        private void InitializeRadioStation()
        {
          radioConf = new RadioConfiguration();
          radioStation = new RadioStation();
          radioConf = RadioConfiguration.LoadStationsFromXml();
          radioConf.CheckNull("Stations");
          foreach (var station in radioConf.Stations)
          {
              radioStationListBox.Items.Add(station);
          }
        }

        private void DoDeleteRadio()
        {
            radioConf.Stations.Remove(radioStation);
            radioStationListBox.Items.Remove(radioStation);
            radioConf.SaveToXml();

        }
        private void SelectRadio()
        {
           
           radioStation = radioStationListBox.SelectedItem as RadioStation;
            if(radioStation!=null)
            {
                radioPropertyGrid.SelectedObject = radioStation;
                isRadioSelectedCondition.Value = true;
            }
        }
        private void DoEditRadio()
        {
            throw new NotImplementedException();
        }
        private void DoAddRadio()
        {
            throw new NotImplementedException();
        }
    }
}
