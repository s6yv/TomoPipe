using System;
using System.Threading;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Newtonsoft.Json;
using Platom.Protocol;
using CommonMessage = Platom.Protocol.CommonMessage;

namespace TestServicesRunner
{
    public class CzyToPiateczekService : IDummyService
    {
        private KafkaOptions connection_options;
        private BrokerRouter connection_router;
        private CancellationTokenSource cts;
        private Thread th1, th2;
        private MessageSequence sequence_main;
        private string service_name;

        public bool Enabled { get; set; }

        public CzyToPiateczekService()
        {
        }

        public void Setup()
        {
            this.connection_options = new KafkaOptions(Platom.Configuration.KafkaUriForm);
            this.connection_router = new BrokerRouter(this.connection_options);

            this.cts = new CancellationTokenSource();

            this.service_name = "PiateczekDetector";
            this.sequence_main = MessageSequence.CreateProducerSequence(service_name, "PiateczekChannel", "schema_Piateczek");
            this.Enabled = true;
        }


        class PiateczekPayload : AbstractPayload
        {
            [JsonProperty("czy_piateczek")] public string IsFriday { get; set; }
            [JsonProperty("ktora_godzina")] public DateTime CurrentTime { get; set; }
        }




        private void ServiceNotifierThreadRoutine()
        {
            CancellationToken ct = this.cts.Token;

            MessageSequence status_sequence = MessageSequence.CreateProducerSequence(this.service_name, "status", "schema_status");

            using (Producer producer = new Producer(this.connection_router))
                while (!ct.IsCancellationRequested)
                {
                    // Przygotuj nagłowek usługi
                    StatusPayload status_payload =
                        StatusPayload.FromSequenceHeaders(this.service_name,
                            new[] {this.sequence_main}, new MessageSequence[0], 
                            20 * 1000, 16 * 1000);

                    if (this.Enabled)
                    {
                        status_sequence.Next();
                        StatusMessage msg = new StatusMessage(status_sequence, status_payload);
                        producer.SendStatusMessage(msg, ct);

                        lock (Program.Synchronized)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(
                                $"Usługa {status_sequence.ServiceName} wysłała komunikat #{status_sequence.SequenceNumber} kanałem {status_sequence.ChannelName}...");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }

                    Thread.Sleep(status_payload.NextAliveInterval);
                }

        }


        private void MainWorkerThreadRoutine()
        {
            CancellationToken ct = this.cts.Token;

            using (Producer producer = new Producer(this.connection_router))
                while (!ct.IsCancellationRequested)
                {

                    PiateczekPayload pp = new PiateczekPayload()
                    {
                        CurrentTime = DateTime.Now,
                        IsFriday = DateTime.Now.DayOfWeek == DayOfWeek.Friday
                            ? "Tak! Dziś jest piąteczek!"
                            : "Niestety, dziś nie jest piąteczek..."
                    };

                    CommonMessage msg = new CommonMessage_AbstractPayload(this.sequence_main, pp);
                    producer.SendCommonMessage(msg, ct);

                    //lock (Program.Synchronized)
                    //    Console.WriteLine(
                    //        $"Usługa {this.sequence_main.ServiceName} wysłała komunikat #{this.sequence_main.SequenceNumber} kanałem {this.sequence_main.ChannelName}...");

                    msg.Sequence.Next();

                    Thread.Sleep(6);
                }

        }


        public void Run()
        {
            if (this.th1 != null)
                throw new InvalidOperationException("Usługa została już wcześniej uruchomiona");

            this.th1 = new Thread(new ThreadStart(this.ServiceNotifierThreadRoutine));
            this.th2 = new Thread(new ThreadStart(this.MainWorkerThreadRoutine));
            this.th1.Start();
            this.th2.Start();
        }

        public void Stop()
        {
            this.cts.Cancel();
        }

        public void WaiteForTermination()
        {
            if (this.th1 == null)
                throw new InvalidOperationException("Usługa została już wcześniej uruchomiona");

            this.th2.Join();
            this.th1.Join();
            this.th1 = this.th2 = null;
        }
    }


}