using System;
using System.Threading;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Newtonsoft.Json;
using Platom.Protocol;
using CommonMessage = Platom.Protocol.CommonMessage;

namespace TestServicesRunner
{
    public class KangarooService : IDummyService
    {
        private KafkaOptions connection_options;
        private BrokerRouter connection_router;
        private CancellationTokenSource cts;
        private Thread th1, th2, th3;

        private string service_name;
        private MessageSequence sequence_odd;
        private MessageSequence sequence_even;

        public bool Enabled { get; set; }


        public KangarooService()
        {
        }

        public void Setup()
        {
            this.service_name =  "Kangur";
            this.connection_options = new KafkaOptions(Platom.Configuration.KafkaUriForm);
            this.connection_router = new BrokerRouter(this.connection_options);

            this.cts = new CancellationTokenSource();

            this.sequence_odd = MessageSequence.CreateProducerSequence(this.service_name, "OddChannel", "schema_Kangaroo");
            this.sequence_even = MessageSequence.CreateProducerSequence(this.service_name, "EvenChannel", "schema_Kangaroo");
            this.Enabled = true;

        }


        //class TimePayload : AbstractPayload
        //{
        //    [JsonProperty("godzina")] public int Hour { get; set; }
        //    [JsonProperty("minuta")] public int Minute { get; set; }
        //    [JsonProperty("sekunda")] public int Second { get; set; }

        //    [JsonProperty("czas")] public DateTime CurrentTime { get; set; }
        //}

        //class DatePayload : AbstractPayload
        //{

        //    [JsonProperty("dzień")] public int Day { get; set; }
        //    [JsonProperty("miesiąc")] public int Month { get; set; }
        //    [JsonProperty("rok")] public int Year{ get; set; }

        //    [JsonProperty("data")] public DateTime CurentDate { get; set; }
        //}




        private void ServiceNotifierThreadRoutine()
        {
            CancellationToken ct = this.cts.Token;

            MessageSequence status_sequence = MessageSequence.CreateProducerSequence(this.service_name, "status", "schema_status");

            bool dir = true;
            using (Producer producer = new Producer(this.connection_router))
                while (!ct.IsCancellationRequested)
                {
                    // Przygotuj nagłowek usługi
                    StatusPayload status_payload =
                        StatusPayload.FromSequenceHeaders(this.service_name,
                            dir ? new[] {this.sequence_odd} : new[] {this.sequence_even},
                            dir ? new[] {this.sequence_even} : new[] {this.sequence_odd},
                            10 * 1000,
                            7 * 1000);

                    dir = !dir;

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

                    Thread.Sleep(1000+0*status_payload.NextAliveInterval);
                }
        }


        //private void DateWorkerThreadRoutine()
        //{
        //    CancellationToken ct = this.cts.Token;

        //    using (Producer producer = new Producer(this.connection_router))
        //        while (!ct.IsCancellationRequested)
        //        {

        //            DatePayload pp = new DatePayload()
        //            {
        //                CurentDate = DateTime.Now,
        //                Day = DateTime.Now.Day,
        //                Month = DateTime.Now.Month,
        //                Year = DateTime.Now.Year,
        //            };

        //            CommonMessage msg = new CommonMessage_AbstractPayload(this.sequence_date, pp);
        //            producer.SendCommonMessage(msg, ct);

        //            //lock (Program.Synchronized)
        //            //    Console.WriteLine(
        //            //        $"Usługa {this.sequence_date.ServiceName} wysłała komunikat #{this.sequence_date.SequenceNumber} kanałem {this.sequence_date.ChannelName}...");

        //            msg.Sequence.Next();

        //            Thread.Sleep(1);
        //        }
        //}

        //private void TimeWorkerThreadRoutine()
        //{
        //    CancellationToken ct = this.cts.Token;

        //    using (Producer producer = new Producer(this.connection_router))
        //        while (!ct.IsCancellationRequested)
        //        {

        //            TimePayload pp = new TimePayload()
        //            {
        //                CurrentTime = DateTime.Now,
        //                Hour = DateTime.Now.Hour,
        //                Minute = DateTime.Now.Minute,
        //                Second = DateTime.Now.Second,
        //            };

        //            CommonMessage msg = new CommonMessage_AbstractPayload(this.sequence_time, pp);
        //            producer.SendCommonMessage(msg, ct);

        //            lock (Program.Synchronized)
        //                Console.WriteLine(
        //                    $"Usługa {this.sequence_time.ServiceName} wysłała komunikat #{this.sequence_time.SequenceNumber} kanałem {this.sequence_time.ChannelName}...");
        //            msg.Sequence.Next();

        //            Thread.Sleep(1200);
        //        }
        //}


        public void Run()
        {
            if (this.th1 != null)
                throw new InvalidOperationException("Usługa została już wcześniej uruchomiona");

            this.th1 = new Thread(new ThreadStart(this.ServiceNotifierThreadRoutine));
            //this.th2 = new Thread(new ThreadStart(this.DateWorkerThreadRoutine));
            //this.th3 = new Thread(new ThreadStart(this.TimeWorkerThreadRoutine));

            this.th1.Start();
            //this.th2.Start();
            //this.th3.Start();
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
            this.th3.Join();
            this.th1.Join();
            this.th1 = this.th2 = this.th3 = null;
        }
    }
}