
using System.Text;

namespace PlatomMonitor.PROTO
{
    public class ServiceUpdateEventArgs : ServiceEventArgs
    {
        public string[] PublicationChannels { get; private set; }
        public string[] SubscribedChannels { get; private set; }


        public ServiceUpdateEventArgs(string serviceName, string[] publicationChannels, string[] subscribedChannels)
        :base(serviceName)
        {
            this.PublicationChannels = publicationChannels.Clone() as string[];
            this.SubscribedChannels = subscribedChannels.Clone() as string[];
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE {ServiceName}: ");
            sb.Append("PUB: " + string.Join(",", this.PublicationChannels));
            sb.Append(", SUB: " + string.Join(",", this.SubscribedChannels));
            return sb.ToString();
        }
    }
}