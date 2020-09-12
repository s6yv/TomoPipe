using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Confluent.Kafka;

namespace ConsoleApp1
{
    class Content
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int ID { get; set; }
    }

    delegate void MeasurementAcquiredDelegate(string experiment_name, int size, double mtt);

    class PingerEntity
    {
        private Random rng;
        private string name;
        private BrokerRouter producer_connection_router;
        private ConsumerOptions consumer_options;

        private CancellationTokenSource producer_token_source;
        private CancellationTokenSource consumer_token_source;

        private Thread producer_thread;
        private Thread consumer_thread;

        public MeasurementAcquiredDelegate OnMeasurement { get; set; }

        public int DataLow { get; set; }
        public int DataHigh { get; set; }

        public double FrequencyLow { get; set; }
        public double FrequencyHigh { get; set; }

        public string Topic { get; set; }


        public PingerEntity(string experimentName, int dataLow, int dataHigh, double freqLow, double freqHigh,
            Random rng)
        {
            this.rng = rng ?? new Random();
            this.name = experimentName;

            this.producer_token_source = new CancellationTokenSource();
            this.consumer_token_source = new CancellationTokenSource();

            this.DataLow = dataLow;
            this.DataHigh = dataHigh;
            this.FrequencyHigh = freqHigh;
            this.FrequencyLow = freqLow;

            this.SetupKafkaConnection();
        }

        private void SetupKafkaConnection()
        {
            this.Topic = $"topic_" + Guid.NewGuid().ToString();

            try
            {
                KafkaOptions producerConnectionOptions = new KafkaOptions(new Uri("http://212.191.89.18:9092"));
                this.producer_connection_router = new BrokerRouter(producerConnectionOptions);

                KafkaOptions consumerConnectionOptions = new KafkaOptions(new Uri("http://212.191.89.18:9092"));
                BrokerRouter consumerConnectionRouter = new BrokerRouter(consumerConnectionOptions);
                this.consumer_options = new ConsumerOptions(this.Topic, consumerConnectionRouter);
                this.consumer_options.MinimumBytes = 1;
                this.consumer_options.MaxWaitTimeForMinimumBytes = new TimeSpan(0, 0, 0, 5);

            }
            catch (Exception ex) // cholera wie, czym kafka rzuca
            {
                //
                Console.WriteLine($"{this}: {ex.Message}");
            }
        }


        private void ProducerThread()
        {
            CancellationToken ct = this.producer_token_source.Token;
            int id = 0;
            byte[] payload = new byte[this.DataHigh];
            rng.NextBytes(payload);

            using (Producer producer = new Producer(this.producer_connection_router))
                while (!ct.IsCancellationRequested)
                {
                    // Losuj częstotliwość oraz dane
                    int interval_high = (int) Math.Round(1000.0 / this.FrequencyHigh);
                    int interval_low = (int) Math.Round(1000.0 / this.FrequencyLow);

                    int interval = this.rng.Next(interval_high, interval_low + 1);
                    int length = this.rng.Next(this.DataLow, this.DataHigh + 1);

                    //
                    // Przygotuj blok danych
                    Content content = new Content() {ID = id++, Timestamp = DateTime.Now};
                    content.Text = String.Join("", payload.Take(length).Select(bt => bt.ToString("X2")));

                    //
                    // Serializuj i wyślij
                    string jcontent = JsonConvert.SerializeObject(content);
                    Console.WriteLine(
                        $"{this} ID={content.ID}: Wysyłanie {jcontent.Length} bajtów, przerwa={interval} ms... ");
                    producer.SendMessageAsync(this.Topic, new[] {new Message(jcontent)}); //.Wait();
                    Console.Out.Flush();

                    //
                    // No iczekaj
                    Thread.Sleep(interval);
                }
        }

        private void ConsumerThread()
        {
            CancellationToken ct = this.consumer_token_source.Token;
            IEnumerator<Message> message_source = null;

            using (Consumer consumer = new Consumer(this.consumer_options))
                while (!ct.IsCancellationRequested)
                {

                    //
                    // Odbierz oczekujący komunikat tak szybko, jak tylko się pojawi
                    if (message_source == null)
                        message_source = consumer.Consume().GetEnumerator();

                    bool result = message_source.MoveNext();
                    Debug.Assert(result, "Ale że jak to???");

                    //
                    // Deserializuj
                    Content content =
                        JsonConvert.DeserializeObject<Content>(Encoding.UTF8.GetString(message_source.Current.Value));

                    //
                    // Wyświetl czas
                    // MTT - mean trip time
                    // RTT - round trip time
                    TimeSpan delta = DateTime.Now - content.Timestamp;
                    double mtt = delta.TotalMilliseconds / 2.0;
                    Console.WriteLine($"{this} ID={content.ID}: MTT={mtt:N3}, RTT={(delta.TotalMilliseconds):N3}");
                    Console.Out.Flush();

                    if (this.OnMeasurement != null)
                        this.OnMeasurement(this.name, content.Text.Length, mtt);

                }
        }

        public void Start(MeasurementAcquiredDelegate measDelegate)
        {
            this.OnMeasurement = measDelegate;

            // 
            this.producer_thread = new Thread(new ThreadStart(ProducerThread));
            this.consumer_thread = new Thread(new ThreadStart(ConsumerThread));

            // Do dzieła
            this.producer_thread.Start();
            this.consumer_thread.Start();
        }

        public void Wait(int experimentTime)
        {
            // Czekaj zadany czas i końscz zabawę
            Thread.Sleep(experimentTime * 1000);
            this.Terminate();
        }

        public void Terminate()
        {
            // Zakończ wątki, NAJPIERW konsument, POTEM producent
            this.consumer_token_source.Cancel();
            if (this.consumer_thread.IsAlive)
                this.consumer_thread.Join();

            this.producer_token_source.Cancel();
            if (this.producer_thread.IsAlive)
                this.producer_thread.Join();
        }

        public override string ToString()
        {
            //return $"[{this.DataLow}-{this.DataHigh} B; {this.FrequencyLow}-{this.FrequencyHigh} Hz]";
            return $"[{this.name}]";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            /*
            KafkaOptions producerConnectionOptions = new KafkaOptions(new Uri("http://212.191.89.18:9092"));
            BrokerRouter br = new BrokerRouter(producerConnectionOptions);
            Producer producer = new Producer(br);

            Message msg = new Message("abcd");
            producer.SendMessageAsync("tjtest", new[] {msg});

            Thread.Sleep(1000);

    */ /*
            KafkaOptions consumerConnectionOptions = new KafkaOptions(new Uri("http://212.191.89.18:9092"));
            
            BrokerRouter consumerConnectionRouter = new BrokerRouter(consumerConnectionOptions);
            ConsumerOptions opt = new ConsumerOptions("tjtest", consumerConnectionRouter);
            

            Consumer consumer = new Consumer(opt);
             foreach(Message x in consumer.Consume())
             {
                 Console.WriteLine(x);
             }

            Topic topic = consumer.GetTopic("tjtest");

            */

            var config = new ConsumerConfig
            {

                BootstrapServers = "212.191.89.18:9092",
                GroupId = "csharp-consumerj",
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };

           // const int commitPeriod = 5;

            using (var consumer = new ConsumerBuilder<Ignore, string>(config)
                // Note: All handlers are called on the main .Consume thread.
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                    // possibly manually specify start offsets or override the partition assignment provided by
                    // the consumer group by returning a list of topic/partition/offsets to assign to, e.g.:
                    // 
                    // return partitions.Select(tp => new TopicPartitionOffset(tp, externalOffsets[tp]));
                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
                })
                .Build())
            {

                //
                consumer.Subscribe("tjtest");
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume();

                        if (consumeResult.IsPartitionEOF)
                        {
                            Console.WriteLine(
                                $"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                            continue;
                        }

                        Console.WriteLine(
                            $"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Value}");


                        try
                        {
                            consumer.Commit(consumeResult);
                        }
                        catch (KafkaException e)
                        {
                            Console.WriteLine($"Commit error: {e.Error.Reason}");
                        }
                        //

                    }
                    catch (Exception ex)
                    {
                        ///
                    }
                }

            }
        }
    }
}
