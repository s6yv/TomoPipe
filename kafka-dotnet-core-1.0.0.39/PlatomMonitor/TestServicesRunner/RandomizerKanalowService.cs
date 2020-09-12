using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Newtonsoft.Json;
using Platom.Protocol;
using CommonMessage = Platom.Protocol.CommonMessage;

namespace TestServicesRunner
{
    public class RandomizerKanalowService : IDummyService
    {
        private KafkaOptions connection_options;
        private BrokerRouter connection_router;
        private CancellationTokenSource cts;
        private Thread th1;//, th2, th3;

        private string service_name;
        private List<MessageSequence> publishing_sequences;
        private List<MessageSequence> subscribed_sequences;

        private Random rnd;
        private int id;

        public bool Enabled { get; set; }

        public RandomizerKanalowService()
        {
        }

        public void Setup()
        {
            this.service_name = "RandomizerKanalowService";
            this.connection_options = new KafkaOptions(Platom.Configuration.KafkaUriForm);
            this.connection_router = new BrokerRouter(this.connection_options);

            this.cts = new CancellationTokenSource();

            this.publishing_sequences = new List<MessageSequence>();
            this.subscribed_sequences = new List<MessageSequence>();

            this.rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                this.RandomlyModifyChannelList(this.publishing_sequences, "PUB-RANDOM", 1);
                this.RandomlyModifyChannelList(this.subscribed_sequences, "SUB-RANDOM", 1);
            }

            this.Enabled = true;

        }

        private void RandomlyModifyChannelList(List<MessageSequence> seq, string name, int forcedMode = -1)
        {
            int mode = forcedMode == -1 ? this.rnd.Next(4) : forcedMode; // 0, 1, 2, 3

            if (mode == 1 && seq.Count < 120 /*limit to 128 */) // czy dodać?
            {
                MessageSequence ms = MessageSequence.CreateProducerSequence(this.service_name, $"{name}-{++this.id}", "schema_KanalRandomizowany");
                seq.Add(ms);
            }
            else if (mode == 2) // usunąć?
            {
                if (seq.Count > 0)
                {
                    int idx = this.rnd.Next(seq.Count);
                    seq.RemoveAt(idx);
                }
            }

           // Console.WriteLine(string.Join(",", seq.Select(x => x.ChannelName)));
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
                            this.publishing_sequences, subscribed_sequences,
                            12 * 1000, 1 * 1000 / 4);

                    if (Enabled)
                    {
                        status_sequence.Next();
                        StatusMessage msg = new StatusMessage(status_sequence, status_payload);
//                        msg.Sequence.SequenceNumber = -23424;
                        producer.SendStatusMessage(msg, ct);

                        lock (Program.Synchronized)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(
                                $"Usługa {status_sequence.ServiceName} wysłała komunikat #{status_sequence.SequenceNumber} kanałem {status_sequence.ChannelName}...");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        this.RandomlyModifyChannelList(this.publishing_sequences, "PUB-RANDOM");
                        this.RandomlyModifyChannelList(this.subscribed_sequences, "SUB-RANDOM");
                    }

                    Thread.Sleep(status_payload.NextAliveInterval);
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

        //            CommonMessage msg = new CommonMessage(this.sequence_date, pp);
        //            producer.SendCommonMessage(msg, ct);

        //            lock (Program.Synchronized)
        //                Console.WriteLine(
        //                    $"Usługa {this.sequence_date.ServiceName} wysłała komunikat #{this.sequence_date.SequenceNumber} kanałem {this.sequence_date.ChannelName}...");
        //            msg.Sequence.Next();

        //            Thread.Sleep(1000);
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

        //            CommonMessage msg = new CommonMessage(this.sequence_time, pp);
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

            //this.th2.Join();
            //this.th3.Join();
            this.th1.Join();
            this.th1 /*= this.th2 = this.th3 */= null;
        }
    }
}