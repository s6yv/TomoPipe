using System.Text;

namespace PlatomMonitor.PROTO
{
    public enum ServiceDiscoveryType
    {
        Normal,
        Catchup
    }

    public class ServiceDiscoveryEventArgs : ServiceEventArgs
    {

        public string[] PublicationChannels { get; private set; }
        public string[] SubscribedChannels { get; private set; }

        private ServiceDiscoveryType discovery_type;

        public bool IsNormalDiscovery => this.discovery_type == ServiceDiscoveryType.Normal;
        public bool IsCatchUpDiscovery => this.discovery_type == ServiceDiscoveryType.Catchup;

        public ServiceDiscoveryEventArgs(string serviceName, ServiceDiscoveryType discoveryType, string[] publicationChannels,
            string[] subscribedChannels)
            : base(serviceName)
        {
            this.discovery_type = discoveryType;
            this.PublicationChannels = publicationChannels.Clone() as string[];
            this.SubscribedChannels = subscribedChannels.Clone() as string[];
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DISCOVERY ");
            sb.Append(this.discovery_type == ServiceDiscoveryType.Normal ? "NEW" : "CATCHUP");
            sb.Append($" {ServiceName} ");
            sb.Append("PUB: " + string.Join(",", this.PublicationChannels));
            sb.Append(", SUB: " + string.Join(",", this.SubscribedChannels));
            return sb.ToString();
        }
    }
}