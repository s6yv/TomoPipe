using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PlatomMonitor.PROTO;

namespace PlatomMonitor.Topology.SimpleMonitor
{
    public class SimpleTopologyMonitor : IServiceObserver, IDisposable
    {
        private TopologyObserverCollection topology_observers;
        private List<Service> services;

        public SimpleTopologyMonitor()
        {
            this.topology_observers = new TopologyObserverCollection();
            this.services = new List<Service>();

        }

        public void RegisterServiceActivityObserver(ITopologyObserver observer)
        {
            if (!this.topology_observers.AddObserver(observer))
                throw new ServicesActivityMonitorException("Taki obserwator już istnieje");
        }

        public void UnregisterServiceActivityObserver(ITopologyObserver observer)
        {
            if (!this.topology_observers.RemoveObserver(observer))
                throw new ServicesActivityMonitorException("Taki obserwator nie został wcześniej zarejestrowany");
        }


        public void OnServiceDiscovery(ServiceDiscoveryEventArgs args)
        {
            Service svc = this.GetServiceOrDefault(args.ServiceName, null);
            ITopologyObserver observers = topology_observers;

            // czy jest taka usługa?
            if (svc == null)
            {
                // nie, dodaj i powiadom
                svc = new Service(args.ServiceName);

                this.services.Add(svc);
                observers.OnServiceDiscovery(svc.Name);

                //this.SetPublicationChannelNames(svc, args.PublicationChannels);
                //this.SetServiceSourceChannels(svc, args.SubscribedChannels);

                foreach (string channel_name in args.PublicationChannels)
                {
                    Channel channel = svc.AddPublicationChannel(channel_name);
                    observers.OnPublicationChannelAdded(svc.Name, channel.Name);
                }

                foreach (string channel_name in args.SubscribedChannels)
                {
                    Channel channel = svc.AddSubscribedChannel(channel_name);
                    observers.OnSubscriptionChannelAdded(svc.Name, channel.Name);
                }
            }
            else
            {
                // ussługa była widoczna - aktywuj ją
                svc.Activate();
                observers.OnServiceActivated(svc.Name);

                //
                // uporządkuj kanały - PUBLICATION
                //

                string[] pub_to_be_removed = svc.PublicationChannels.Except(args.PublicationChannels).ToArray();
                string[] pub_to_be_added = (args.PublicationChannels).Except(svc.PublicationChannels).ToArray();

                foreach (string channel_name in pub_to_be_removed)
                {
                    svc.RemovePublicationChannel(channel_name);
                    observers.OnPublicationChannelRemoved(svc.Name, channel_name);
                }

                foreach (string channel_name in pub_to_be_added)
                {
                    svc.AddPublicationChannel(channel_name);
                    observers.OnPublicationChannelAdded(svc.Name, channel_name);
                }

                //
                // uporządkuj kanały - SUBSCRIPTION
                //

                string[] sub_to_be_removed = svc.SubscribedChannels.Except(args.SubscribedChannels).ToArray();
                string[] sub_to_be_added = (args.SubscribedChannels).Except(svc.SubscribedChannels).ToArray();

                foreach (string channel_name in sub_to_be_removed)
                {
                    svc.RemoveSubscribedChannel(channel_name);
                    observers.OnSubscriptionChannelRemoved(svc.Name, channel_name);
                }

                foreach (string channel_name in sub_to_be_added)
                {
                    svc.AddSubscribedChannel(channel_name);
                    observers.OnSubscriptionChannelAdded(svc.Name, channel_name);
                }

            }


        }


        public void OnServiceUpdate(ServiceUpdateEventArgs args)
        {
            Service svc = this.GetServiceOrDefault(args.ServiceName, null);
            ITopologyObserver observers = topology_observers;

            Debug.Assert(svc != null);


            //
            // uporządkuj kanały - PUBLICATION
            //
            string[] pub_to_be_removed = svc.PublicationChannels.Except(args.PublicationChannels).ToArray();
            string[] pub_to_be_added = (args.PublicationChannels).Except(svc.PublicationChannels).ToArray();

            foreach (string channel_name in pub_to_be_removed)
            {
                svc.RemovePublicationChannel(channel_name);
                observers.OnPublicationChannelRemoved(svc.Name, channel_name);
            }

            foreach (string channel_name in pub_to_be_added)
            {
                svc.AddPublicationChannel(channel_name);
                observers.OnPublicationChannelAdded(svc.Name, channel_name);
            }

            //
            // uporządkuj kanały - SUBSCRIPTION
            //

            string[] sub_to_be_removed = svc.SubscribedChannels.Except(args.SubscribedChannels).ToArray();
            string[] sub_to_be_added = (args.SubscribedChannels).Except(svc.SubscribedChannels).ToArray();

            foreach (string channel_name in sub_to_be_removed)
            {
                svc.RemoveSubscribedChannel(channel_name);
                observers.OnSubscriptionChannelRemoved(svc.Name, channel_name);
            }

            foreach (string channel_name in sub_to_be_added)
            {
                svc.AddSubscribedChannel(channel_name);
                observers.OnSubscriptionChannelAdded(svc.Name, channel_name);
            }

        }

        public void OnServiceTimeout(ServiceTimeoutEventArgs args)
        {
            // deaktywuj usługę i kanały, na których ona nadaje
            Service service = this.GetServiceOrDefault(args.ServiceName, default(Service));
            //if (!this.TryGetService(args.ServiceName, out Service service))
            //return;

            if (service == null)
                return; // ??

            service.Deactivate();
            (topology_observers as ITopologyObserver).OnServiceTimeout(service.Name);
        }

        private Service GetServiceOrDefault(string serviceName, Service defaultService)
        {
            foreach (Service entry in services)
                if (entry.Name == serviceName)
                    return entry;
            return defaultService;
        }



        public void Dispose()
        {
        }
    }
}