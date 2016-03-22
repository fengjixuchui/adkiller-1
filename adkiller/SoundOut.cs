using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Interfaces;
using System.Windows.Forms;
using AdKiller.Helpers;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
namespace AdKiller
{
   public class SoundOut : BaseProcessor
    {
        private static readonly int frequency = 48000;
        private static readonly int soundDataBufferTime =12;
        private static readonly int channelCount = 2;
        private readonly FMOD.System system;
        private SoundProvider readySound;
        private Object thisLock;
        private CircularBuffer soundDataBuffer;
        private bool playSoundOnce;

        public SoundOut(FMOD.System newsys)
        {
            system = newsys;
            thisLock = new Object();
            readySound = new SoundProvider(this.system);
            soundDataBuffer = new CircularBuffer(frequency * soundDataBufferTime * channelCount);
        }

        protected override void HandleDataReady(object sender, SoundDataReadyEventArgs args)
        {
            lock (thisLock)
            {
                readySound.Sounds.addSoundData(args.SoundData);     

                if (!playSoundOnce)
                {
                    readySound.Sounds.PlayCheckedSound();
                    playSoundOnce = true;
                }
            }
            Debug.WriteLine(String.Format("{0}{1}", "Something", Thread.CurrentThread.ManagedThreadId));
        }
        protected override void HandleStop(object source, EventArgs args)
        {
            readySound.Sounds.Dispose();
        }


    }
}