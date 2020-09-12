using System;

namespace PlatomMonitor.PROTO
{
    public class ConsoleServiceActivityObserver : IServiceObserver
    {
        private int id;


        public void OnServiceDiscovery(ServiceDiscoveryEventArgs args)
        {
            if (args.IsNormalDiscovery)
                InternalShow($"DISCOVERY[normal; {this.id}]", ConsoleColor.Green, args.ServiceName);
            else
                InternalShow($"DISCOVERY[catchup;{this.id}]", ConsoleColor.Green, args.ServiceName);
        }

        public void OnServiceUpdate(ServiceUpdateEventArgs args) => InternalShow($"UPDATE[{this.id}]     ", ConsoleColor.Magenta, args.ServiceName);

        public void OnServiceTimeout(ServiceTimeoutEventArgs args) => InternalShow($"TIMEOUT[{this.id}]    ", ConsoleColor.Red, args.ServiceName);


        public ConsoleServiceActivityObserver() : this(0)
        {
        }

        public ConsoleServiceActivityObserver(int id)
        {
            this.id = id;
        }

        private void InternalShow(string header, ConsoleColor color, string message)
        {
            lock (this)
            {
                Console.ForegroundColor = color;
                Console.Write(header);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($": {message}");
            }
        }
    }
}

