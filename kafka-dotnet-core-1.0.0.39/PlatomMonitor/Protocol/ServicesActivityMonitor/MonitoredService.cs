using System;

namespace PlatomMonitor.PROTO
{
    internal class MonitoredService
    {
        /// <summary> Nazwa monitorowanej usługi </summary>
        public string ServiceName { get; }

        /// <summary> Czas ostatniej aktualizacji (otrzymania komunikatu kanałem status) </summary>
        public DateTime LastUpdateTimestamp { get; set; }

        /// <summary> Czas następnej aktualizacji </summary>
        public DateTime NextUpdateTimestamp { get; set; }

        /// <summary> Czas po jakim nieaktualizowaną usługę należy uznać za martwą. </summary>
        public DateTime TimeoutTimestamp { get; set; }


        public string[] PublicationChannelNames { get; set; }
        public string[] SubscribedChannelNames { get; set; }

        public MonitoredService(string serviceName)
        {
            this.ServiceName = serviceName;
        }
    }
}