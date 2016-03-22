using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Helpers;
using System.Windows.Forms;
using System.Threading;
using AdKiller.Interfaces;

namespace AdKiller
{
    public class Mp3StreamSimulator : ISoundSource
    {

        private Sound sample;
        private int frequency = 48000;
        private int bufferTime = 3;
        private int dataCounter = 0;
        private short[] buffer;
        private bool endOfFile;
        private int bufferCount;
        private SoundDataReadyEventArgs dataSet;
        public event SoundDataReadyEventHandler DataReady;
        public event EventHandler Stop;
        public void Process(FMOD.System newsys, string filename)
        {
            var system = newsys as FMOD.System;
            if (newsys == null)
                throw new ArgumentException("data has to be FMOD.System!");
            endOfFile = false;

            //  Create and initialize Sound object containing sample.mp3 
            sample = Sound.CreateJingle(system,filename);
            buffer = new short[frequency * bufferTime * sample.ChannelCount];

            if (sample.SoundDataBuffer.Buffer.Length % buffer.Length == 0)
            {
                bufferCount = sample.SoundDataBuffer.Buffer.Length / buffer.Length;
            }
            else
            {
                bufferCount = (sample.SoundDataBuffer.Buffer.Length / buffer.Length) + 1;
            }

            for (int i = 0; i < bufferCount; i++)
            {
                if (i == bufferCount - 1)
                {
                    endOfFile = true;
                    for (int k = 0; k < sample.SoundDataBuffer.Buffer.Length - (i * buffer.Length); k++)
                    {
                        buffer[k] = sample.SoundDataBuffer.Buffer[k + (i * buffer.Length)];
                    }
                }
                else
                {
                    for (int k = 0; k < buffer.Length; k++)
                    {
                        buffer[k] = sample.SoundDataBuffer.Buffer[k + (i * buffer.Length)];
                    }
                }

                this.OnDataReady();
                dataCounter++;
                Array.Clear(buffer, 0, buffer.Length);
            }
            dataCounter = 0;
        }
        private void OnDataReady()
        {
            dataSet = new SoundDataReadyEventArgs(buffer.Length, frequency, buffer, false);
            dataSet.LastDataParcel = endOfFile;
            dataSet.DataParcelNumber = dataCounter;

            if (DataReady != null)
            {
                DataReady(this, dataSet);
            }
        }
        private void OnStop()
        {
            if (Stop != null)
            {
                Stop(this, EventArgs.Empty);
            }
        }
    }
}
