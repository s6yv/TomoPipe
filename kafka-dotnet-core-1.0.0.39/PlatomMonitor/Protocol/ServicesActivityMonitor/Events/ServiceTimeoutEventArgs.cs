namespace PlatomMonitor.PROTO
{
    public class ServiceTimeoutEventArgs : ServiceEventArgs
    {
        public ServiceTimeoutEventArgs(string serviceName)
        :base(serviceName)
        {
        }

        public override string ToString() => $"Timeout {this.ServiceName}";

    }
}