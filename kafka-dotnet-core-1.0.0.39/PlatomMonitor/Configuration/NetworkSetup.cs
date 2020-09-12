using System;
using System.IO;
using System.Net;

namespace Platom

{
    public static class Configuration
    {

        public static  IPAddress KafkaServerIP { get; }
        public static int KafkaServerPort { get; }

        public static string KafkaServerPortForm => $"{KafkaServerIP}:{KafkaServerPort}";
        public static Uri KafkaUriForm => new Uri($"http://{KafkaServerIP}:{KafkaServerPort}/");

        static Configuration()
        {
            //
            // Konfiguracja brokera Kafki
            KafkaServerIP = IPAddress.Parse("212.191.89.18");
            KafkaServerPort = 9092;


            //
            // Lokalizacja katalogu danych
            string base_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Platom\\NetworkToolbox");
            Directory.CreateDirectory(base_path);
            ApplicationDataPath = base_path;

            //
            // Lokalizacja katalogu z repozytorium schematów walidacyjnych
            string repo_path = Path.Combine(base_path, "validation_schemas");
            Directory.CreateDirectory(repo_path);
            ValidationSchemasRepositoryPath = repo_path;
        }

        public static string ValidationSchemasRepositoryPath { get; }

        public static string ApplicationDataPath { get; }
    }
}