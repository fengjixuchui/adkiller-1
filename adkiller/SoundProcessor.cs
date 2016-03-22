using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Helpers;

namespace AdKiller
{
    public static class SoundProcessor
    {
        private static double maxShortValue = 32768.0;
        private static double criterionMinValue = 0.7; //  0.7 value is a criterion of matching, can be lowered to find more noisy pattern match
        private static double criterionMaxValue = 1.8;
        public static int FindPattern(short[] data, short[] pattern)
        {
            data.CheckNull("data");
            pattern.CheckNull("pattern");

            if (data.Length > pattern.Length && pattern.Length != 0 && data.Length != 0)
            {
                //double maxValue = 0,
                //    dataAverge = 0,
                //    patternAverge = 0,
                //    sumData = 0,
                //    sumPattern = 0;

                double[] dataTemp = new double[data.Length],
                    patternTemp = new double[pattern.Length],
                    corrResult = new double[dataTemp.Length + patternTemp.Length - 2]; // result returned from corrr1d method below, returns an array of this length

                for (int i = 0; i < dataTemp.Length; i++)
                    dataTemp[i] = data[i] / maxShortValue;
                for (int i = 0; i < patternTemp.Length; i++)
                    patternTemp[i] = pattern[i] / maxShortValue;

                alglib.corrr1d(dataTemp, dataTemp.Length, patternTemp, patternTemp.Length, out corrResult);

                EvaluateStdDev(dataTemp, patternTemp, ref corrResult);
                return FindMaxAndIndex(corrResult, patternTemp);

                // Evaluate standart deviation 
                //foreach (var val in dataTemp)
                //{
                //    dataAverge += val;
                //}
                //dataAverge = dataAverge / dataTemp.Length;
                //foreach (var val in patternTemp)
                //{
                //    patternAverge += val;
                //}
                //patternAverge = patternAverge / patternTemp.Length;


                //foreach (var val in dataTemp)
                //{
                //    sumData += ((val - dataAverge) * (val - dataAverge));
                //}
                //double dataStdDev = Math.Sqrt(sumData / dataTemp.Length);
                //foreach (var val in patternTemp)
                //{
                //    sumPattern += ((val - patternAverge) * (val - patternAverge));
                //}
                //double patternStdDev = Math.Sqrt(sumPattern / patternTemp.Length);

                //for (int i = 0; i < corrResult.Length; i++)
                //{
                //    corrResult[i] = corrResult[i] / (patternTemp.Length * patternStdDev * dataStdDev);
                //}


                // Find the index of max value in array
                //int index = -1;
                //for (int i = 0; i < corrResult.Length - patternTemp.Length; i++)
                //{
                //    if (corrResult[i] > maxValue)
                //    {
                //        index = i;
                //        maxValue = corrResult[i];
                //    }
                //}

                ////  0.7 value is a criterion of matching, can be lowered to find more noisy pattern match
                //if (maxValue > criterionMinValue && maxValue < criterionMaxValue)
                //{
                //    return index;
                //}
                //else
                //{
                //    return -1; // returns -1 when it didnt find any match
                //}
            }
            else
            {
                throw new ArgumentException("Bad argument passed");
            }

        }
        public static DetectionResult DetectJingle(short[] data, short[] startJingle, short[] endJingle)
        {
            DetectionResult detectionTimes = new DetectionResult(SoundProcessor.FindPattern(data, startJingle), SoundProcessor.FindPattern(data, endJingle));
            return detectionTimes;
        }
        private static void EvaluateStdDev(double[] dataTemp, double[] patternTemp, ref double[] corrResult)
        {
            double dataAverge = 0,
                  patternAverge = 0,
                  sumData = 0,
                  sumPattern = 0;

            foreach (var val in dataTemp)
            {
                dataAverge += val;
            }
            dataAverge = dataAverge / dataTemp.Length;
            foreach (var val in patternTemp)
            {
                patternAverge += val;
            }
            patternAverge = patternAverge / patternTemp.Length;


            foreach (var val in dataTemp)
            {
                sumData += ((val - dataAverge) * (val - dataAverge));
            }
            double dataStdDev = Math.Sqrt(sumData / dataTemp.Length);
            foreach (var val in patternTemp)
            {
                sumPattern += ((val - patternAverge) * (val - patternAverge));
            }
            double patternStdDev = Math.Sqrt(sumPattern / patternTemp.Length);

            for (int i = 0; i < corrResult.Length; i++)
            {
                corrResult[i] = corrResult[i] / (patternTemp.Length * patternStdDev * dataStdDev);
            }

        }
        private static int FindMaxAndIndex(double[] corrResult, double[] patternTemp)
        {
            double maxValue = 0;
            int index = -1;
            for (int i = 0; i < corrResult.Length - patternTemp.Length; i++)
            {
                if (corrResult[i] > maxValue)
                {
                    index = i;
                    maxValue = corrResult[i];
                }
            }


            if (maxValue > criterionMinValue && maxValue < criterionMaxValue)
            {
                return index;
            }
            else
            {
                return -1; // returns -1 when it didnt find any match
            }

        }
      
    }
}
