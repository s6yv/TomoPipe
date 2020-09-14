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
        internal List<double> data;
    }
    class RocsoleFrame
    {
        internal int CurrentMeasurementNo = -1;
        internal string TimeStamp;
        internal Data normalized;
        internal Data ROCSOLE_raw;

        internal Data Filtered;
        internal double lastFilteredAverage;

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

        //filter out only the opposite electrodes measurements from field lastRocsoleFrame.Filtered
        //and store them to lastFilteredFrame
        internal void FilterFrame(string json, bool considerNormalized, int elec)
        {
            if (!ParseFromJSON(json))
            {
                return;
            }

            Filtered = new Data();
            Filtered.data = new List<double>();
            if (considerNormalized)
                FilterFrameNormalized(elec);
            else
                FilterFrameRAW(elec);




            if (Filtered.data.Count() > 0)
                lastFilteredAverage = Filtered.data.Average();
        }
        internal void FilterFrameNormalized(int elec)
        {
            //Filtered.data = new List<double>(normalized.data);
            int meas = (int)(elec * (elec - 1) / 2);
            if (normalized.size != meas) {
                Console.WriteLine("Error while filtering opposite electrodes pairs");
                Console.WriteLine("Input electrodes number " + elec + " not compatible with frame size " + normalized.size);
                return;
            }
            Filtered.data.Add(normalized.data.ElementAt((int)(elec / 2 - 1)));
            int lastValue = (int)(elec / 2 - 1);
            //string indexes = ""+lastValue+";";
            for (int i = 2; i <= (int)(elec/2); i++)
            {
                int index = lastValue + elec - (i - 1);
                lastValue = index;
                Filtered.data.Add(normalized.data.ElementAt(index));
                //indexes += " " + index + ";";
            }
            //Console.WriteLine(indexes);
        }
        internal void FilterFrameRAW(int elec)
        {
            //Filtered.data = new List<double>(ROCSOLE_raw.data);
            int meas = elec * elec;
            if (ROCSOLE_raw.size != meas)
            {
                Console.WriteLine("Error while filtering opposite electrodes pairs");
                Console.WriteLine("Input electrodes number " + elec + " not compatible with frame size " + ROCSOLE_raw.size);
                return;
            }
            Filtered.data.Add(ROCSOLE_raw.data.ElementAt((int)(elec / 2)));
            int lastValue = (int)(elec / 2);
            //string indexes = ""+lastValue+";";
            for (int i = 2; i <= (int)(elec / 2); i++)
            {
                int index = lastValue + elec + 1;
                lastValue = index;
                Filtered.data.Add(ROCSOLE_raw.data.ElementAt(index));
                //indexes += " " + index + ";";
            }
            //Console.WriteLine(indexes);
        }
    }
}
