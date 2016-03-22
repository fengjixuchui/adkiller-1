using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Helpers;
namespace AdKiller
{
    public class SoundProvider : BaseProcessor, IDisposable
    {
        private FMOD.SOUND_PCMREADCALLBACK pcmReadCallback;
        private FMOD.SOUND_PCMSETPOSCALLBACK pcmSetPosCallback;
        private FMOD.SOUND_PCMREADCALLBACK pcmReadCallbackStream;
        private Object thisLock, readLock;
        private FMOD.System system;
        private Sound sound;
        private static readonly int frequency = 48000;
        private static readonly int soundBufferTime = 3;
        private static readonly int channelCount = 2;
        private static readonly int defaultSize = frequency * channelCount * soundBufferTime;
        public CircularBuffer SoundDataBuffer { get; private set; }
        public Sound Sounds
        {
            get
            {
                return sound;
            }
            set
            {
                sound = value;
            }

        }
        public SoundProvider(FMOD.System newsys, string connectionUrl) // reading chunks from stream
        {
            newsys.CheckNull("newsys");
            system = newsys;
            SoundDataBuffer = new CircularBuffer(defaultSize);
            readLock = new Object();

            FMOD.RESULT result = system.setStreamBufferSize(64 * 1024, FMOD.TIMEUNIT.RAWBYTES);
            Sound.ErrorCheck(result);
            pcmReadCallbackStream = new FMOD.SOUND_PCMREADCALLBACK(this.PcmReadCallbackStream);
            pcmSetPosCallback = new FMOD.SOUND_PCMSETPOSCALLBACK(this.PcmSetPosCallback);
            sound = Sound.CreateStream(this.system, connectionUrl, pcmReadCallbackStream, pcmSetPosCallback);         
        }
        public SoundProvider(FMOD.System newsys)
        {
            newsys.CheckNull("newsys");
            system = newsys;
            SoundDataBuffer = new CircularBuffer(defaultSize);
            thisLock = new Object();

            pcmReadCallback = new FMOD.SOUND_PCMREADCALLBACK(this.PcmReadCallback);
            pcmSetPosCallback = new FMOD.SOUND_PCMSETPOSCALLBACK(this.PcmSetPosCallback);
            Sounds = Sound.CreateSound(this.system);
            Sounds.SoundInit(pcmReadCallback, pcmSetPosCallback);
        }

        public void Dispose()
        {
            sound.Dispose();
            base.HandleStop(this, EventArgs.Empty);
        }
        protected override void HandleStop(object source, EventArgs args)
        {      
            base.HandleStop(this,EventArgs.Empty);
        }
        protected override void HandleDataReady(object source, SoundDataReadyEventArgs args)
        {
            throw new NotImplementedException();
        }
        private FMOD.RESULT PcmReadCallback(IntPtr soundraw, IntPtr data, uint datalen)
        {
            lock (thisLock)
            {
                if (sound.SoundDataBuffer.Size >= (int)datalen)
                {
                    var playData = sound.SoundDataBuffer.Pop((int)datalen / sizeof(short));
                    Marshal.Copy(playData, 0, data, playData.Length);
                }
            }
            return FMOD.RESULT.OK;
        }
        private FMOD.RESULT PcmReadCallbackStream(IntPtr soundraw, IntPtr data, uint datalen)
        {
            lock (readLock)
            {
                SoundDataReadyEventArgs playData = new SoundDataReadyEventArgs((int)datalen / sizeof(short), frequency);
                Marshal.Copy(data, playData.SoundData, 0, (int)datalen / sizeof(short));
                base.OnDataReady(playData);

            }

            return FMOD.RESULT.OK;
        }


        private FMOD.RESULT PcmSetPosCallback(IntPtr soundraw, int subsound, uint pcmoffset, FMOD.TIMEUNIT postype)
        {
            // not needed
            return FMOD.RESULT.OK;
        }

    }
}
