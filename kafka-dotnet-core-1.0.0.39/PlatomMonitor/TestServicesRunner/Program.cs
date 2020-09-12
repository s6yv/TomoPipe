using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Serialization;

namespace TestServicesRunner
{
    class Program
    {
        public static object Synchronized = new Object();
        static void Main(string[] args)
        {
            //
            // uruchom usługi testowe
            List<IDummyService> services = new List<IDummyService>();
            services.Add(new CzyToPiateczekService());
            services.Add(new CzyToCzwarteczekService());
            services.Add(new ZegarynkaService());
            services.Add(new RandomizerKanalowService());
            services.Add(new KangarooService());


            // Przygotuj cały system
            foreach (var service in services)
                service.Setup();

            // Uruchom....
            foreach (var service in services)
                service.Run();


            lock (Program.Synchronized)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Naciśnij dowolny klawisz, aby przerwać...");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.ReadKey();

            
            foreach (var service in services)
                service.Stop();
            foreach (var service in services)
                service.WaiteForTermination();

        }
    }
}
