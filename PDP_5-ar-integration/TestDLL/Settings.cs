using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;


namespace RocsoleDataConverter
{
    class Settings
    {
        static string jsonFileName = "settings.json";

        public static bool Store(Converter obj)
        {
            try
            {
                File.WriteAllText(jsonFileName, JsonConvert.SerializeObject(obj));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing settings to the settings.JSON file");
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public static bool Read(Converter obj)
        {
            // FIXME: Naming (removed UDP, added AR port)
            try
            {
                JObject root = JObject.Parse(File.ReadAllText(jsonFileName));
                obj.TomoKISStudioIP = (string)root["TomoKISStudioIP"];
                obj.TomoKISStudioPort = (int)root["TomoKISStudioPort"];
                obj.ConsiderNormalizedData = (bool)root["ConsiderNormalizedData"];
                obj.FactorA = (double)root["FactorA"];
                obj.FactorB = (double)root["FactorB"];
                obj.FactorC = (double)root["FactorC"];
                obj.ElectrodesCount = (int)root["ElectrodesCount"];
                obj.TimeInterval = (int)root["TimeInterval"];
                Console.WriteLine("Settings read successfully from the settings.JSON file");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error parsing settings from the settings.JSON file\nSet init values");
                Console.WriteLine(e.Message);
            }
            return false;
        }

    }
}


