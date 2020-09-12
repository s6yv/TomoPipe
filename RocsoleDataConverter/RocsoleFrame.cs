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
            catch (JsonException)
            {
                Console.WriteLine("Error parsing JSON message");
            }
            return false;
        }

        //filter out only the opposite electrodes measurements from field lastRocsoleFrame.Filtered
        //and store them to lastFilteredFrame
        internal void FilterFrame(string json, bool considerNormalized)
        {
            if (!ParseFromJSON(json))
            {
                return;
            }

            Filtered = new Data();
            Filtered.data = new List<double>();
            if (considerNormalized)
                FilterFrameNormalized();
            else
                FilterFrameRAW();
            lastFilteredAverage = Filtered.data.Average();
        }
        internal void FilterFrameNormalized()
        {

        }
        internal void FilterFrameRAW()
        {

        }
    }
}
