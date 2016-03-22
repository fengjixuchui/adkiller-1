using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Helpers;
using System.Windows.Forms;
using AdKiller.Interfaces;
using System.Diagnostics;
using System.Threading;
namespace AdKiller
{
    public class AdDetector : BaseProcessor, IDisposable
    {

        private Sound startJingle;
        private Sound endJingle;
        private int frequency;
        private CircularBuffer soundBuffer;
        private static readonly int defaultFrequency = 48000;
        private readonly int addtionalDataBufferTime;
        private static readonly int channelCount = 2;
        private static readonly short volumeDownValue = 20;
        private bool adState;
        private int newStartJingleLength = 0;
        private short[] dataBuffer;
        private Object thisLock;
        public string StatusText { get; set; }

        public AdDetector(Sound startJingle, Sound endJingle)
        {
            this.startJingle = startJingle;
            this.endJingle = endJingle;
            this.thisLock = new Object();

            double endTime = endJingle.SoundDataBuffer.Buffer.Length / (defaultFrequency * channelCount);
            double startTime = startJingle.SoundDataBuffer.Buffer.Length / (defaultFrequency * channelCount);

            addtionalDataBufferTime = startTime > endTime ? (int)Math.Ceiling(startTime) : (int)Math.Ceiling(endTime);
        }

        protected override void HandleDataReady(object sender, SoundDataReadyEventArgs args)
        {

            lock (thisLock)
            {

                frequency = args.Frequency;
                short[] addtionalData = new short[addtionalDataBufferTime * channelCount * frequency];

                if (args.DataParcelNumber == 1)
                {
                    dataBuffer = new short[args.SoundData.Length];
                    soundBuffer = new CircularBuffer(args.SoundData.Length);
                    soundBuffer.Push(args.SoundData);
                    Array.Copy(args.SoundData, dataBuffer, args.SoundData.Length);          
                }
                else
                {
                    Array.Copy(args.SoundData, 0, addtionalData, 0, addtionalData.Length);
                    soundBuffer.Push(addtionalData);
                    DetectionResult detectionTimes = SoundProcessor.DetectJingle(soundBuffer.Buffer, startJingle.SoundDataBuffer.Buffer, endJingle.SoundDataBuffer.Buffer);

                    SoundDataReadyEventArgs dataSet = new SoundDataReadyEventArgs(dataBuffer.Length, frequency, dataBuffer, adState);

                    adKilling(ref dataSet, detectionTimes.StartJingleIndex, detectionTimes.EndJingleIndex);
#if DEBUG
                    Debug.WriteLine(detectionTimes.StartJingleIndex / (double)(frequency * sizeof(short)));
                    Debug.WriteLine(detectionTimes.EndJingleIndex / (double)(frequency * sizeof(short)));
#endif

                    base.OnDataReady(dataSet);
                    soundBuffer = new CircularBuffer(args.SoundData.Length);
                    soundBuffer.Push(args.SoundData);
                    adState = dataSet.AdState;

                    dataBuffer = new short[args.SoundData.Length];
                    Array.Copy(args.SoundData, dataBuffer, args.SoundData.Length);

                    if (args.LastDataParcel)
                    {
                        detectionTimes = SoundProcessor.DetectJingle(soundBuffer.Buffer, startJingle.SoundDataBuffer.Buffer, endJingle.SoundDataBuffer.Buffer);
                        adKilling(ref dataSet, detectionTimes.StartJingleIndex, detectionTimes.EndJingleIndex);
#if DEBUG
                        Debug.WriteLine(detectionTimes.StartJingleIndex / (double)(frequency * sizeof(short)));
                        Debug.WriteLine(detectionTimes.EndJingleIndex / (double)(frequency * sizeof(short)));
#endif
                        base.OnDataReady(dataSet);
                    }

                }
            }
        }
        protected override void HandleStop(object sender, EventArgs args)
        {
            base.HandleStop(this, EventArgs.Empty);
            // base.OnStop();
        }
        public void Dispose()
        {
            if (startJingle != null)
            {
                startJingle.Dispose();
                startJingle = null;
            }
            if (endJingle != null)
            {
                endJingle.Dispose();
                endJingle = null;
            }

        }
        private void adKilling(ref SoundDataReadyEventArgs dataSet, int start, int end)
        {
            if (start > -1)
            {
                newStartJingleLength = 0;

                if (start + startJingle.SoundDataBuffer.Buffer.Length >= dataSet.SoundData.Length)
                {
                    newStartJingleLength = startJingle.SoundDataBuffer.Buffer.Length - (dataSet.SoundData.Length - start);
                    dataSet.AdState = true;
                }
                else
                {
                    if (end > -1)
                    {
                        if (end >= dataSet.SoundData.Length)
                        {
                            this.silencerLoop(dataSet, start + startJingle.SoundDataBuffer.Buffer.Length, dataSet.SoundData.Length);
                            dataSet.AdState = true;
                        }
                        else
                        {
                            this.silencerLoop(dataSet, start + startJingle.SoundDataBuffer.Buffer.Length, end);
                            dataSet.AdState = false;
                        }
                    }
                    else
                    {
                        this.silencerLoop(dataSet, start + startJingle.SoundDataBuffer.Buffer.Length, dataSet.SoundData.Length);
                        dataSet.AdState = true;
                    }
                }
            }
            else
            {
                if (dataSet.AdState)
                {
                    if (end > -1)
                    {
                        if (end >= dataSet.SoundData.Length)
                        {
                            this.silencerLoop(dataSet, newStartJingleLength, dataSet.SoundData.Length);
                        }
                        else
                        {
                            this.silencerLoop(dataSet, newStartJingleLength, end);
                            dataSet.AdState = false;
                        }
                    }
                    else
                    {
                        this.silencerLoop(dataSet, newStartJingleLength, dataSet.SoundData.Length);
                    }
                    newStartJingleLength = 0;
                }
            }
        }

        private void silencerLoop(SoundDataReadyEventArgs dataSet, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                dataSet.SoundData[i] /= volumeDownValue;
            }
        }
    }
}
