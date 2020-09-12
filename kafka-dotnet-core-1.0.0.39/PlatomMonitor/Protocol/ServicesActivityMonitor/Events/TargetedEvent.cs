namespace PlatomMonitor.PROTO
{
    internal struct TargetedEvent
    {
        public IServiceObserver Target { get; }
        public ServiceEventArgs EventArgs { get; }
        public bool HasTarget => this.Target != null;

        private TargetedEvent(IServiceObserver target, ServiceEventArgs eventArgs)
        {
            this.Target = target;
            this.EventArgs = eventArgs;
        }

        public static TargetedEvent ToAllObservers(ServiceEventArgs eventArgs) => new TargetedEvent(null, eventArgs);
        public static TargetedEvent ToSpecificObserver(IServiceObserver target, ServiceEventArgs eventArgs) => new TargetedEvent(target, eventArgs);

        public override string ToString() => this.EventArgs.ToString() + this.EventArgs.GetType().Name;
    }
}