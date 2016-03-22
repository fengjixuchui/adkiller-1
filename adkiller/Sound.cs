using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Helpers;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using AdKiller.Interfaces;
using System.Windows.Forms;

namespace AdKiller
{
    public class Sound : IDisposable
    {
        private FMOD.System system;
        private FMOD.Sound sounds;
        private FMOD.Channel channels;
        private Object thisLock;

        private static readonly int frequency = 48000;
        private static readonly int soundBufferTime = 3;
        private static readonly int channelCount = 2;
        private static readonly int defaultSize = frequency * channelCount * soundBufferTime;
        public CircularBuffer SoundDataBuffer { get; set; }
        private uint pcm;
        private int bits;
        public int Frequency { get; private set; }
        public int ChannelCount { get; private set; }

        private Sound(FMOD.System newsys)
        {
            newsys.CheckNull("newsys");
            system = newsys;
        }
        private void StreamInit(string connectionUrl, FMOD.SOUND_PCMREADCALLBACK pcmReadCallback, FMOD.SOUND_PCMSETPOSCALLBACK pcmSetPosCallback) // loading stream data
        {
            thisLock = new Object();
            SoundDataBuffer = new CircularBuffer(defaultSize);

            FMOD.CREATESOUNDEXINFO exInfo = new FMOD.CREATESOUNDEXINFO();
            exInfo.cbsize = Marshal.SizeOf(exInfo);
            //   exInfo.length = (uint)(SoundDataBuffer.Buffer.Length * sizeof(short));
            exInfo.numchannels = channelCount;
            exInfo.fileoffset = 0;
            exInfo.format = FMOD.SOUND_FORMAT.PCM16;
            exInfo.defaultfrequency = frequency;
            exInfo.pcmreadcallback = pcmReadCallback;
            exInfo.pcmsetposcallback = pcmSetPosCallback;
            exInfo.dlsname = null;

            FMOD.RESULT result = system.createSound(connectionUrl, (FMOD.MODE.HARDWARE | FMOD.MODE._2D | FMOD.MODE.CREATESTREAM | FMOD.MODE.NONBLOCKING), ref exInfo, ref sounds);
            ErrorCheck(result);
        }
        private void JingleInit(string filename)
        {
            FMOD.Sound tempSound = null;
            thisLock = new Object();
            // Retrieve information about sound
            FMOD.RESULT result = system.createSound(filename, (FMOD.MODE._2D | FMOD.MODE.SOFTWARE), ref tempSound);
            ErrorCheck(result);

            FMOD.SOUND_TYPE soundType = (FMOD.SOUND_TYPE)0;
            FMOD.SOUND_FORMAT soundFormat = (FMOD.SOUND_FORMAT)0;

            int tempBits = 0,
                tempChannels = 0,
                tempPriority = 0;

            uint tempPcm = 0;

            float tempFrequency = 0.0f,
                  tempPan = 0.0f,
                  tempVolume = 0.0f;

            result = tempSound.getFormat(ref soundType, ref soundFormat, ref tempChannels, ref tempBits);
            ErrorCheck(result);

            result = tempSound.getDefaults(ref tempFrequency, ref tempVolume, ref tempPan, ref tempPriority);
            ErrorCheck(result);

            result = tempSound.getLength(ref tempPcm, FMOD.TIMEUNIT.PCM);
            ErrorCheck(result);

            // Fill sound parameters
            ChannelCount = tempChannels;
            Frequency = (int)tempFrequency;
            pcm = tempPcm;
            bits = tempBits;

            // Obtain Raw PCM data from sound
            GetRawData(tempSound, tempChannels, tempBits, tempPcm);


            // Release temp sound instance
            tempSound.release();
            tempSound = null;


            FMOD.CREATESOUNDEXINFO exInfo = new FMOD.CREATESOUNDEXINFO();
            exInfo.cbsize = Marshal.SizeOf(exInfo);
            exInfo.length = (uint)(pcm * sizeof(short));
            exInfo.numchannels = ChannelCount;
            exInfo.format = FMOD.SOUND_FORMAT.PCM16;
            exInfo.defaultfrequency = (int)Frequency;
            // Create a stream from obtained data

            result = system.createStream(SoundDataBuffer.Buffer, (FMOD.MODE.OPENMEMORY | FMOD.MODE.OPENRAW), ref exInfo, ref sounds);
            ErrorCheck(result);
        } // loading jingle
        private void GetRawData(FMOD.Sound tempSound, int channels, int bits, uint pcm)
        {
            tempSound.CheckNull("tempSound");
            uint len1 = 0, len2 = 0;
            IntPtr ptr1 = IntPtr.Zero, ptr2 = IntPtr.Zero;

            switch (bits)
            {
                case 16:
                    {
                        FMOD.RESULT result = tempSound.@lock(0, (uint)(pcm * channels * sizeof(short)), ref ptr1, ref ptr2, ref len1, ref len2);
                        ErrorCheck(result);

                        SoundDataBuffer = new CircularBuffer((int)(pcm * channels));
                        Marshal.Copy(ptr1, SoundDataBuffer.Buffer, 0, (int)(len1 / sizeof(short)));
                        if (len2 > 0)
                            Marshal.Copy(ptr2, SoundDataBuffer.Buffer, (int)(len1 / sizeof(short)), (int)(len2 / sizeof(short)));

                        tempSound.unlock(ptr1, ptr2, len1, len2);
                        break;
                    }
                default:
                    throw new InvalidOperationException("Bits count unsupported");
            }

        }
        public void SoundInit(FMOD.SOUND_PCMREADCALLBACK pcmReadCallback, FMOD.SOUND_PCMSETPOSCALLBACK pcmSetPosCallback)  // providing data to stream
        {
            SoundDataBuffer = new CircularBuffer(defaultSize);
            thisLock = new Object();

            FMOD.CREATESOUNDEXINFO exInfo = new FMOD.CREATESOUNDEXINFO();
            exInfo.cbsize = Marshal.SizeOf(exInfo);
            exInfo.length = (uint)(SoundDataBuffer.Buffer.Length * sizeof(short));
            exInfo.numchannels = channelCount;
            exInfo.fileoffset = 0;
            exInfo.format = FMOD.SOUND_FORMAT.PCM16;
            exInfo.defaultfrequency = frequency;
            exInfo.pcmreadcallback = pcmReadCallback;
            exInfo.pcmsetposcallback = pcmSetPosCallback;
            exInfo.dlsname = null;

            FMOD.RESULT result = system.createSound((string)null, (FMOD.MODE.CREATESTREAM | FMOD.MODE.OPENUSER | FMOD.MODE.OPENRAW | FMOD.MODE._2D | FMOD.MODE.LOOP_NORMAL), ref exInfo, ref sounds);
            ErrorCheck(result);

        }
        public void addSoundData(short[] rawData)
        {
            rawData.CheckNull("rawData");
            this.SoundDataBuffer.Push(rawData);
        }
        public void PlaySound()
        {

            bool state = true;
            while (state)
            {
                FMOD.OPENSTATE openstate = 0;
                uint percentbuffered = 0;
                bool starving = false;
                bool busy = false;
                FMOD.RESULT result = sounds.getOpenState(ref openstate, ref percentbuffered, ref starving, ref busy);
                ErrorCheck(result);

                if (openstate == FMOD.OPENSTATE.READY && channels == null)
                {
                    result = system.playSound(FMOD.CHANNELINDEX.FREE, sounds, false, ref channels);
                    ErrorCheck(result);
                    channels.setVolume(0.0f);
                    state = false;
                }
            }
        }
        public void PlayCheckedSound()
        {
            FMOD.RESULT result = system.playSound(FMOD.CHANNELINDEX.FREE, sounds, false, ref channels);
            ErrorCheck(result);
        }
        public void Dispose()
        {
            if (sounds != null)
            {
                sounds.release();
                sounds = null;
            }
        }
        public static void ErrorCheck(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                MessageBox.Show(String.Format("{0} - {1} ", result, FMOD.Error.String(result))); //FMOD error!
                Environment.Exit(-1);
            }
        }

        // static constructors
        public static Sound CreateSound(FMOD.System newsys)
        {
            Sound sound = new Sound(newsys);
           // sound.SoundInit(pcmReadCallback, pcmSetPosCallback);
            return sound;
        }
        public static Sound CreateStream(FMOD.System newsys, string connectionUrl, FMOD.SOUND_PCMREADCALLBACK pcmReadCallback, FMOD.SOUND_PCMSETPOSCALLBACK pcmSetPosCallback)
        {
            Sound sound = new Sound(newsys);
            sound.StreamInit(connectionUrl, pcmReadCallback, pcmSetPosCallback);
            return sound;
        }
        public static Sound CreateJingle(FMOD.System newsys,string filename)
        {
            Sound sound = new Sound(newsys);
            sound.JingleInit(filename);
            return sound;
        }
    }
}
