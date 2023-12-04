using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocsoleDataConverter
{
    class Data
    {
        internal int size;
        internal List<double> data = new List<double>();
    }
    class RocsoleFrame
    {
        internal int CurrentMeasurementNo = -1;
        internal string TimeStamp;
        internal Data normalized;
        internal Data ROCSOLE_raw;

        internal Data FilteredN;
        internal Data FilteredR;
        internal double lastFilteredAverage;
        internal double lastFilteredStdDev;

        internal double gasCoreDistanceFromCenter;
        internal double gasCoreOffsetAngleDeg;

        private bool ParseFromJSON(string json)
        {
            try
            {
                JObject root = JObject.Parse(json);
                CurrentMeasurementNo = (int)root["CurrentMeasurementNo"];
                TimeStamp = (string)root["TimeStamp"];

                normalized = new Data();
                normalized.size = (int)root["normalized"]["size"];
                JArray json_normalized = (JArray)root["normalized"]["data"];
                normalized.data = json_normalized.Select(d => (double)d).ToList();

                ROCSOLE_raw = new Data();
                ROCSOLE_raw.size = (int)root["ROCSOLE_raw"]["size"];
                JArray json_ROCSOLE_raw = (JArray)root["ROCSOLE_raw"]["data"];
                ROCSOLE_raw.data = json_ROCSOLE_raw.Select(d => (double)d).ToList();

                return true;
            }
            catch (JsonException e)
            {
                Console.WriteLine("Error parsing JSON message");
                Console.WriteLine(e.Message);
            }
            return false;
        }

        private void GasCorePosition(){
            var normalizedFrame = new ComputedNormalizedFrame(ROCSOLE_raw.data);
            gasCoreDistanceFromCenter = normalizedFrame.DistanceOfGasCoreFromCentre();
            gasCoreOffsetAngleDeg = normalizedFrame.GasCoreOffsetAngleDeg();
            Console.WriteLine($"Distance from centre: {gasCoreDistanceFromCenter}");
            Console.WriteLine($"Offset angle: {gasCoreOffsetAngleDeg}");
        }

        //filter out only the opposite electrodes measurements from field lastRocsoleFrame.Filtered
        //and store them to lastFilteredFrame
        internal void FilterFrame(string json, bool considerNormalized, int elec)
        {
            if (!ParseFromJSON(json)) return;
            FilterFrameRAW(elec);

            if (considerNormalized) FilterFrameNormalized(elec);
            if (FilteredR.data.Count() == 0) return;

            GasCorePosition();
            
            lastFilteredAverage = FilteredR.data.Average();
            lastFilteredStdDev = StandardDeviation(Variance(FilteredR.data.ToArray(), lastFilteredAverage));
            if (considerNormalized) lastFilteredAverage = FilteredN.data.Average();
        }
        internal void FilterFrameNormalized(int elec)
        {
            FilteredN = new Data();
            int meas = (int)(elec * (elec - 1) / 2);
            if (normalized.size != meas)
            {
                Console.WriteLine("Error while filtering opposite electrodes pairs");
                Console.WriteLine("Input electrodes number " + elec + " not compatible with frame size " + normalized.size);
                return;
            }
            FilteredN.data.Add(normalized.data.ElementAt((int)(elec / 2 - 1)));
            int lastValue = (int)(elec / 2 - 1);
            //string indexes = ""+lastValue+";";
            for (int i = 2; i <= (int)(elec / 2); i++)
            {
                int index = lastValue + elec - (i - 1);
                lastValue = index;
                FilteredN.data.Add(normalized.data.ElementAt(index));
                //indexes += " " + index + ";";
            }
            //Console.WriteLine(indexes);
        }
        internal void FilterFrameRAW(int elec)
        {

            FilteredR = new Data();
            //Filtered.data = new List<double>(ROCSOLE_raw.data);
            int meas = elec * elec;
            if (ROCSOLE_raw.size != meas)
            {
                Console.WriteLine("Error while filtering opposite electrodes pairs");
                Console.WriteLine("Input electrodes number " + elec + " not compatible with frame size " + ROCSOLE_raw.size);
                return;
            }
            FilteredR.data.Add(ROCSOLE_raw.data.ElementAt((int)(elec / 2)));
            int lastValue = (int)(elec / 2);
            //string indexes = ""+lastValue+";";
            for (int i = 2; i <= (int)(elec / 2); i++)
            {
                int index = lastValue + elec + 1;
                lastValue = index;
                FilteredR.data.Add(ROCSOLE_raw.data.ElementAt(index));
                //indexes += " " + index + ";";
            }
            //Console.WriteLine(indexes);
        }

        private double Variance(double[] nums, double avg)
        {
            if (nums.Length > 1)
            {
                // Now figure out how far each point is from the mean
                // So we subtract from the number the average
                // Then raise it to the power of 2
                double sumOfSquares = 0.0;
                foreach (int num in nums)
                {
                    sumOfSquares += Math.Pow((num - avg), 2.0);
                }
                // Finally divide it by n - 1 (for standard deviation variance)
                // Or use length without subtracting one ( for population standard deviation variance)
                return sumOfSquares / (double)(nums.Length - 1);
            }
            else { return 0.0; }
        }

        // Square root the variance to get the standard deviation
        private double StandardDeviation(double variance)
        {
            return Math.Sqrt(variance);
        }

    }
}
