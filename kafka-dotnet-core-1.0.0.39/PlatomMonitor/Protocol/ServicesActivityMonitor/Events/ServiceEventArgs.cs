namespace PlatomMonitor.PROTO
{
    public class ServiceEventArgs
    {
        public static readonly ServiceEventArgs Empty = new ServiceEventArgs(null);

        /// <summary> Nazwa usługi </summary>
        public string ServiceName { get;}


        protected ServiceEventArgs(string serviceName)
        {
            this.ServiceName = serviceName;
        }

        public override string ToString()
        {
            if (this.ServiceName == null)
                return "Empty";
            else
                return ServiceName;
        }
    }
}