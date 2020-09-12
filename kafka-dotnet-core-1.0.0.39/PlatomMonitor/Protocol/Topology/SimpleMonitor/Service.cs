using System.Collections.Generic;
using System.Linq;

namespace PlatomMonitor.Topology.SimpleMonitor
{
    public class Service
    {
        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        /// <summary>
        /// Lista kanałów, na których usługa TopologyService nasłuchuje.
        /// Z punktu widzenia kanału usługa jest oznaczona jako Sink (wyjście).
        /// </summary>
        private List<Channel> subscribed_channels;

        public string[] SubscribedChannels => this.subscribed_channels.Select(x => x.Name).ToArray();


        /// <summary>
        /// Lista kanałów, którymi usługa TopologyService nadaje komunikaty.
        /// Z punkt widzenia takie kanału usługa jest oznaczona jako Source (źródło).
        /// </summary>
        private List<Channel> publication_channels;

        public string[] PublicationChannels => this.publication_channels.Select(x => x.Name).ToArray();


        public Service(string name)
        {
            this.Name = name;
            this.subscribed_channels = new List<Channel>();
            this.publication_channels = new List<Channel>();
            //this.IsActive = true; // skoro dodany, to aktywny
        }


        public override string ToString()
        {
            //string act = this.IsActive ? "ACTIVE" : "INACTIVE";
            string act = "";
            string istr = string.Join(",", this.subscribed_channels.Select(x => x.Name));
            string ostr = string.Join(",", this.publication_channels.Select(x => x.Name));
            return $"{Name}[{act}]: Subscribed: {istr}, Publication: {ostr}";
        }

        public void Deactivate()
        {
            this.IsActive = false;
            //todo: obsługa zdarzenia
        }

        public void Activate()
        {
            this.IsActive = true;
        }

        public Channel AddPublicationChannel(string channelName)
        {
            Channel ch = new Channel(channelName);
            this.publication_channels.Add(ch);
            return ch;
        }

        public Channel AddSubscribedChannel(string channelName)
        {
            Channel ch = new Channel(channelName);
            this.subscribed_channels.Add(ch);
            return ch;
        }

        public void RemovePublicationChannel(string channelName)
        {
            Channel ch = this.publication_channels.Find(x => x.Name == channelName);
            if (ch == null)
                return; //todo: obsługa błedu
            this.publication_channels.Remove(ch);
        }

        public void RemoveSubscribedChannel(string channelName)
        {
            Channel ch = this.subscribed_channels.Find(x => x.Name == channelName);
            if (ch == null)
                return; //todo: obsługa błedu
            this.subscribed_channels.Remove(ch);
        }
    }
}