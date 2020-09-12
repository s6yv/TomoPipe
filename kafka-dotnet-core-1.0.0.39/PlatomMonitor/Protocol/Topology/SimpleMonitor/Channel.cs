namespace PlatomMonitor.Topology.SimpleMonitor
{
    public class Channel
    {
        public string Name { get; private set; }
        //public bool IsActive { get; private set; }

        //private List<Service> subscribers;
        //public Service Publisher { get; private set; }
        //public Service[] Subscribers => subscribers.ToArray();
        //public string[] SubscribersNames => subscribers.Select(x => x.Name).ToArray();

        public Channel(string name)
        {
            this.Name = name;
            //this.IsActive = true; // skoro dodany, to aktywny
            //this.subscribers = new List<Service>();
        }
    }
}