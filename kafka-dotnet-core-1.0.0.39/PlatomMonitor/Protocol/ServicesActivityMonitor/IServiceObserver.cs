namespace PlatomMonitor.PROTO
{
    public interface IServiceObserver
    {
        void OnServiceDiscovery(ServiceDiscoveryEventArgs args);

        void OnServiceUpdate(ServiceUpdateEventArgs args);

        void OnServiceTimeout(ServiceTimeoutEventArgs args);

    }

}