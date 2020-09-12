using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
//using System.Net.Configuration;
using System.Threading;
using Platom.Protocol;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable InconsistentNaming

namespace PlatomMonitor.PROTO
{
    public class ServicesActivityMonitor : IDisposable
    {
        private List<IServiceObserver> registered_observers;
        private Dictionary<string, MonitoredService> active_services;
        private Thread timer_thread;
        private CancellationTokenSource cancellation_source;
        private ConcurrentQueue<TargetedEvent> event_queue;
        private AutoResetEvent event_queue_trigger;


        public int Count => this.registered_observers.Count;


        public ServicesActivityMonitor()
        {
            //
            // Przygotuj kontenery
            this.registered_observers = new List<IServiceObserver>();
            this.active_services = new Dictionary<string, MonitoredService>();

            this.cancellation_source = new CancellationTokenSource();
            this.event_queue = new ConcurrentQueue<TargetedEvent>();

            //
            // Uruchom wątek timera pilnującego czasów komunikacji
            this.event_queue_trigger = new AutoResetEvent(false);
            this.timer_thread = new Thread(new ThreadStart(this.TimerThread))
            {
                Name = "ServicesActivityMonitor::TimerThread"
            };
            this.timer_thread.Start();
        }

        /// <summary>
        /// Metoda dodaje obserwatora zdarzeń związanych z pojawianiem się, aktualizacją i znikaniem usług z sieci Platom.
        /// Wszystkie metody interfejsu <see cref="IServiceObserver"/> uruchamiane są z tego samego wątku, przy czym nie jest to wątek, z którego uruchamiana jest metoda <see cref="Update"/>.
        /// </summary>
        /// <param name="observer">Obiekt dodawanego obserwatora.</param>
        public void RegisterServiceActivityObserver(IServiceObserver observer)
        {
            lock (this.registered_observers)
            {
                if (this.registered_observers.Contains(observer))
                    throw new ServicesActivityMonitorException("Taki obserwator już istnieje");
                this.registered_observers.Add(observer);


                // wygeneruj zdarzenia nadganiające dla nowego obserwatora;)
                lock (this.active_services)
                    foreach (MonitoredService ms in this.active_services.Values)
                    {
                        ServiceDiscoveryEventArgs args =
                            new ServiceDiscoveryEventArgs(ms.ServiceName, ServiceDiscoveryType.Catchup, ms.PublicationChannelNames, ms.SubscribedChannelNames);
                        this.event_queue.Enqueue(TargetedEvent.ToSpecificObserver(observer, args));
                        this.event_queue_trigger.Set();
                    }
            }
        }

        /// <summary>
        /// Metoda usuwa obserwatora usług.
        /// </summary>
        /// <param name="observer">Obiekt usuwanego obserwatora.</param>
        public void UnregisterServiceActivityObserver(IServiceObserver observer)
        {
            lock (this.registered_observers)
            {
                if (!this.registered_observers.Contains(observer))
                    throw new ServicesActivityMonitorException("Taki obserwator nie został wcześniej zarejestrowany");
                this.registered_observers.Remove(observer);
            }
        }


        /// <summary>
        /// Metoda aktualizaująca stan monitora <see cref="ServicesActivityMonitor"/> komunikatem statusowym <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Komunikat statusowy, zgodny ze schematem 'schema_status'.</param>
        public void Update(StatusMessage message)
        {
            //
            // Sprawdź, czy monitor widzał już wcześniej tę usługę
            lock (this.active_services)
                if (this.active_services.ContainsKey(message.Payload.ServiceName))
                {
                    // Tak. Aktualizuj informacje o aktywności usługi
                    MonitoredService ms = this.active_services[message.Payload.ServiceName];

                    ms.LastUpdateTimestamp = DateTime.Now;//;/ message.Sequence.TimeStamp;
                    ms.NextUpdateTimestamp = ms.LastUpdateTimestamp.AddMilliseconds(message.Payload.NextAliveInterval);
                    ms.TimeoutTimestamp = ms.LastUpdateTimestamp.AddMilliseconds(message.Payload.CurrentTimeoutValue);
                    ms.PublicationChannelNames = message.Payload.PublicationChannels.Clone() as string[];
                    ms.SubscribedChannelNames = message.Payload.SubscribedChannels.Clone() as string[];

                    // Komunikat o aktualizacji usłgui
                    ServiceUpdateEventArgs args = new ServiceUpdateEventArgs(ms.ServiceName, ms.PublicationChannelNames,
                        ms.SubscribedChannelNames);
                    this.event_queue.Enqueue(TargetedEvent.ToAllObservers(args));
                    this.event_queue_trigger.Set();

                }
                else
                {
                    // Nie, dodaj nową usługę
                    MonitoredService ms = new MonitoredService(message.Payload.ServiceName);
                    this.active_services.Add(message.Payload.ServiceName, ms);

                    ms.LastUpdateTimestamp = DateTime.Now;//;/ message.Sequence.TimeStamp;
                    ms.NextUpdateTimestamp = ms.LastUpdateTimestamp.AddMilliseconds(message.Payload.NextAliveInterval);
                    ms.TimeoutTimestamp = ms.LastUpdateTimestamp.AddMilliseconds(message.Payload.CurrentTimeoutValue);
                    ms.PublicationChannelNames = message.Payload.PublicationChannels.Clone() as string[];
                    ms.SubscribedChannelNames = message.Payload.SubscribedChannels.Clone() as string[];

                    // Komunikat o nowej usłudze
                    ServiceDiscoveryEventArgs args =
                        new ServiceDiscoveryEventArgs(ms.ServiceName, ServiceDiscoveryType.Normal, ms.PublicationChannelNames, ms.SubscribedChannelNames);
                    this.event_queue.Enqueue(TargetedEvent.ToAllObservers(args));
                    this.event_queue_trigger.Set();
                }
        }

        private void TimerThread()
        {
            Debug.WriteLine($"Uruchomiono wątek {Thread.CurrentThread.Name}");
            CancellationToken ct = this.cancellation_source.Token;
            while (!ct.IsCancellationRequested)
            {
                // Czekaj 100ms lub na sygnał
                this.event_queue_trigger.WaitOne(100);

                // sprawdź, czy któraś usługa nie milczy zbyt długo?
                DateTime now = DateTime.Now;
                lock (this.active_services)
                {
                    // Przygotuj listę usług, które od dawna się nie odzywały i usuń je z listy
                    // Dla każdej usuwanej usługi emituj komunikat
                    List<MonitoredService> to_be_removed = new List<MonitoredService>();
                    foreach (MonitoredService ms in this.active_services.Values)
                        if (now > ms.TimeoutTimestamp)
                            to_be_removed.Add(ms);

                    // Usuń usługi, które zamilkły i powiadom o tym obserwatorów
                    foreach (MonitoredService ms in to_be_removed)
                    {
                        this.active_services.Remove(ms.ServiceName);
                        ServiceTimeoutEventArgs stea = new ServiceTimeoutEventArgs(ms.ServiceName);
                        this.event_queue.Enqueue(TargetedEvent.ToAllObservers(stea));
                    }
                }

                // spróbuj pobrać zdarzenie z kolejki i je wysłać
                while (this.event_queue.TryDequeue(out TargetedEvent te))
                {
                    Debug.WriteLine($"TimerThread: Otrzymano zdarzenie {te}");

                    //
                    // Zdarzenie jest zaadresowane do określonego obserwatora, ale tego nie ma
                    lock(this.registered_observers)
                        if (te.HasTarget && !this.registered_observers.Contains(te.Target))
                            continue;

                    //
                    // Nowa usługa?
                    if (te.EventArgs is ServiceDiscoveryEventArgs dargs)
                        if (te.HasTarget)
                            te.Target.OnServiceDiscovery(dargs);
                        else
                            lock (this.registered_observers)
                                foreach (IServiceObserver observer in this.registered_observers)
                                    observer.OnServiceDiscovery(dargs);

                    //
                    // Czy aktualizacja?
                    if (te.EventArgs is ServiceUpdateEventArgs uargs)
                        if (te.HasTarget)
                            te.Target.OnServiceUpdate(uargs);
                        else
                            lock (this.registered_observers)
                                foreach (IServiceObserver observer in this.registered_observers)
                                    observer.OnServiceUpdate(uargs);

                    //
                    // Czy timeout? (usługa zamilkła)
                    if (te.EventArgs is ServiceTimeoutEventArgs targs)
                        if (te.HasTarget)
                            te.Target.OnServiceTimeout(targs);
                        else
                            lock (this.registered_observers)
                                foreach (IServiceObserver observer in this.registered_observers)
                                    observer.OnServiceTimeout(targs);
                }
            }
        }

        /// <summary>
        /// Metoda kończy działanie monitora.
        /// </summary>
        public void Dispose()
        {
            // Zatrzymaj wątek timera
            this.cancellation_source.Cancel();
            this.timer_thread.Join();

            // Zrób porządek
            this.cancellation_source.Dispose();
            this.event_queue_trigger.Dispose();
        }
    }
}