using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller
{
    public delegate void SoundDataReadyEventHandler(object sender, SoundDataReadyEventArgs args);
    public class SoundDataReadyEventArgs : EventArgs
    {
        public int DataParcelNumber { get; set; }
        public short[] SoundData { get; private set; }
        public bool LastDataParcel { get; set; }
        public int Frequency 
        { 
            get
            {
                return 48000;
            }
       }
        public int SoundDataTime { get; private set; }
        public bool AdState { get; set; }


        public SoundDataReadyEventArgs(int size, int frequency)
        {
            DataParcelNumber = 0;
            SoundData = new short[size];
            this.SoundDataTime = SoundData.Length / (this.Frequency * sizeof(short));

        }
        public SoundDataReadyEventArgs(int size, int frequency, short[] data , bool adState)
        {
            DataParcelNumber = 0;
            SoundData = new short[size];
            this.SoundDataTime = SoundData.Length / (this.Frequency * sizeof(short));
            Array.Copy(data, 0, SoundData, 0, data.Length);
            this.AdState = adState;
        }

        public void ResetEventCounter()
        {
            DataParcelNumber = 0;
        }
        public void RaiseEventCounter()
        {
            DataParcelNumber++;
        }

    }
}
