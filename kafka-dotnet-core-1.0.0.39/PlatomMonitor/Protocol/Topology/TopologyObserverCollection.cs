using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PlatomMonitor.Topology
{
    public class TopologyObserverCollection : ITopologyObserver
    {
        private List<ITopologyObserver> observers;

        public TopologyObserverCollection()
        {
            this.observers = new List<ITopologyObserver>();
        }

        public bool AddObserver(ITopologyObserver observer)
        {
            lock (this.observers)
            {
                if (this.observers.Contains(observer))
                    return false;
                this.observers.Add(observer);
                return true;
            }
        }

        public bool RemoveObserver(ITopologyObserver observer)
        {
            lock (this.observers)
            {
                if (!this.observers.Contains(observer))
                    return false;
                this.observers.Remove(observer);
                return true;
            }
        }


        #region ITopologyObserver

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            string name = sf.GetMethod().Name;
            name = name.Substring(name.LastIndexOf('.') + 1);
            return "### ITopologyObserver." + name;
        }

        void ITopologyObserver.OnServiceDiscovery(string serviceName)
        {
            Debug.WriteLine($"{GetCurrentMethod()}: serviceName={serviceName}");
            lock (this.observers)
                foreach (var observer in this.observers)
                    observer.OnServiceDiscovery(serviceName);
        }

        void ITopologyObserver.OnServiceActivated(string serviceName)
        {
            Debug.WriteLine($"{GetCurrentMethod()}: serviceName={serviceName}");
            lock (this.observers)
                foreach (var observer in this.observers)
                    observer.OnServiceActivated(serviceName);
        }

        void ITopologyObserver.OnServiceTimeout(string serviceName)
        {
            Debug.WriteLine($"{GetCurrentMethod()}: serviceName={serviceName}");
            lock (this.observers)
                foreach (var observer in this.observers)
                    observer.OnServiceTimeout(serviceName);
        }

        void ITopologyObserver.OnPublicationChannelAdded(string serviceName, string channelName)
        {
            Debug.WriteLine($"{GetCurrentMethod()}: serviceName={serviceName}, channelName={channelName}");
            lock (this.observers)
                foreach (var observer in this.observers)
                    observer.OnPublicationChannelAdded(serviceName, channelName);
        }

        void ITopologyObserver.OnPublicationChannelRemoved(string serviceName, string channelName)
        {
            Debug.WriteLine($"{GetCurrentMethod()}: serviceName={serviceName}, channelName={channelName}");
            lock (this.observers)
                foreach (var observer in this.observers)
                    observer.OnPublicationChannelRemoved(serviceName, channelName);
        }

        void ITopologyObserver.OnSubscriptionChannelAdded(string serviceName, string channelName)
        {
            Debug.WriteLine($"{GetCurrentMethod()}: serviceName={serviceName}, channelName={channelName}");
            lock (this.observers)
                foreach (var observer in this.observers)
                    observer.OnSubscriptionChannelAdded(serviceName, channelName);
        }

        void ITopologyObserver.OnSubscriptionChannelRemoved(string serviceName, string channelName)
        {
            Debug.WriteLine($"{GetCurrentMethod()}: serviceName={serviceName}, channelName={channelName}");
            lock (this.observers)
                foreach (var observer in this.observers)
                    observer.OnSubscriptionChannelRemoved(serviceName, channelName);
        }

        #endregion
    }
}
