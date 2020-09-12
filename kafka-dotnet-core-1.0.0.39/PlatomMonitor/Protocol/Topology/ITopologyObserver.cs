namespace PlatomMonitor.Topology
{

    public interface ITopologyObserver
    {
        void OnServiceDiscovery(string serviceName);
        void OnServiceActivated(string serviceName);

        void OnServiceTimeout(string serviceName);


        void OnPublicationChannelAdded(string serviceName, string channelName);
        void OnPublicationChannelRemoved(string serviceName, string channelName);

        void OnSubscriptionChannelAdded(string serviceName, string channelName);
        void OnSubscriptionChannelRemoved(string serviceName, string channelName);
    }

}