using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Interfaces;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using AdKiller.Helpers;
namespace AdKiller
{
    public class SoundIn : BaseProcessor
    {
        private static readonly int frequency = 48000;
        private static readonly int soundDataBufferTime = 12;
        private static readonly int channelCount = 2;
        private static readonly int bufferTime = frequency * channelCount * soundDataBufferTime;
        private static readonly int longerBufferTime = frequency * channelCount * 15;
        private static readonly int pumpedBuffer = bufferTime + 10000;
        private FMOD.System system;
        private SoundProvider streamSound;
        private int bufferCounter = 0;
        private AdKillingThread ads;
        private Thread adsKill;
        public CircularBuffer SoundDataBuffer { get; set; }
        public string StatusText { get; set; }
        public short[] SoundData { get; set; }

        public SoundIn()
        {
            SoundDataBuffer = new CircularBuffer(soundDataBufferTime * channelCount * frequency);
        }
        public SoundIn(FMOD.System newsys, string connectionUrl)
        {
            newsys.CheckNull("SoundIn newsys");
            system = newsys;
            ads = new AdKillingThread();
            ads.ThreadReady += this.HandleThreadReady;
            SoundDataBuffer = new CircularBuffer(pumpedBuffer);
            streamSound = new SoundProvider(this.system, connectionUrl);
            this.SoundSource = streamSound;
            streamSound.Sounds.PlaySound();
        }
        public void StopMe()
        {
            if (adsKill != null)
            {
                adsKill.Abort();
                adsKill = null;
            }
            streamSound.Dispose();
            streamSound = null;
            //   this.HandleStop(this, EventArgs.Empty);
        }
        protected void OnDataReady()
        {

            SoundDataReadyEventArgs dataSet = new SoundDataReadyEventArgs(SoundDataBuffer.Size, frequency, SoundDataBuffer.PopAll(), false);

            adsKill = new Thread(() => ads.Process(dataSet));
            adsKill.Start();
            dataSet.DataParcelNumber = ++bufferCounter;
        }
        //protected void OnStopMe()
        //{

        //    if (Stop != null)
        //    {
        //        Stop(this, EventArgs.Empty);
        //    }
        //}
        protected override void HandleDataReady(object sender, SoundDataReadyEventArgs args)
        {
            StatusText = "Buffering ...";
            SoundDataBuffer.Push(args.SoundData);
            if (SoundDataBuffer.Size > bufferTime)
            {
                this.OnDataReady();
            }


        }
        protected void HandleThreadReady(object sender, SoundDataReadyEventArgs args)
        {
            base.OnDataReady(args);
        }
        protected override void HandleStop(object source, EventArgs args)
        {
            base.HandleStop(this, EventArgs.Empty);
        }
    }
}















// ladne do wczytywania z dysku
//int dataShift = (args.DataParcelNumber % temp) * args.SoundData.Length;


//if (args.DataParcelNumber % temp < args.SoundDataTime)
//{
//    for (int i = 0; i < args.SoundData.Length; i++)
//    {
//        this.SoundData[i + dataShift] = args.SoundData[i];
//    }
//    if (args.LastDataParcel)
//    {
//        lastDataParcel = true;
//        this.OnDataReady();
//        Array.Clear(this.SoundData, 0, this.SoundData.Length);

//    }
//}
//else
//{
//    for (int i = 0; i < args.SoundData.Length; i++)
//    {
//        this.SoundData[i + dataShift] = args.SoundData[i];
//    }
//    this.OnDataReady();
//    Array.Clear(this.SoundData, 0, this.SoundData.Length);
//}