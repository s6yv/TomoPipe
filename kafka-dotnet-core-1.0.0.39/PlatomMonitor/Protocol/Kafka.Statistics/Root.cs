using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

//
// Źródlo: https://github.com/edenhill/librdkafka/blob/master/STATISTICS.md
//

namespace Platom.Protocol.Kafka.Statistics
{

    public class Root
    {

        /// <summary> Handle instance name </summary>
        [JsonProperty("name")] public string name { get; private set; }

        /// <summary> The configured (or default) client.id </summary>
        [JsonProperty("client_id")] public string client_id { get; private set; }

        /// <summary> Instance type (producer or consumer) </summary>
        [JsonProperty("type")] public string @type { get; private set; }

        /// <summary> librdkafka's internal monotonic clock (micro seconds) </summary>
        [JsonProperty("ts")] public ulong ts { get; private set; }

        /// <summary> Wall clock time in seconds since the epoch </summary>
        [JsonProperty("time")] public ulong time { get; private set; }


        /// <summary>  	Number of ops (callbacks, events, etc) waiting in queue for application to serve with rd_kafka_poll() </summary>
        [JsonProperty("replyq")] public ulong replyq { get; private set; }

        /// <summary> Current number of messages in producer queues </summary>
        [JsonProperty("msg_cnt")] public ulong msg_cnt { get; private set; }

        /// <summary> Current total size of messages in producer queues </summary>
        [JsonProperty("msg_size")] public ulong msg_size { get; private set; }


        /// <summary> Threshold: maximum number of messages allowed allowed on the producer queues </summary>
        [JsonProperty("msg_max")] public ulong msg_max { get; private set; }

        /// <summary> Threshold: maximum total size of messages allowed on the producer queues </summary>
        [JsonProperty("msg_size_max")] public ulong msg_size_max { get; private set; }

        /// <summary> Internal tracking of legacy vs new consumer API state </summary>
        [JsonProperty("simple_cnt")] public ulong simple_cnt { get; private set; }

        /// <summary> Number of topics in the metadata cache. </summary>
        [JsonProperty("metadata_cache_cnt")] public ulong metadata_cache_cnt { get; private set; }



        /// <summary> Dict of brokers, key is broker name, value is object </summary>
        [JsonProperty("brokers")] public Dictionary<string, KafkaBroker> brokers { get; private set; }



        /// <summary> Total number of requests sent to Kafka brokers </summary>
        [JsonProperty("tx")] public ulong tx { get; private set; }

        /// <summary> Total number of bytes transmitted to Kafka brokers </summary>
        [JsonProperty("tx_bytes")] public ulong tx_bytes { get; private set; }

        /// <summary>  	Total number of responses received from Kafka brokers </summary>
        [JsonProperty("rx")] public ulong rx { get; private set; }

        /// <summary> Total number of bytes received from Kafka brokers </summary>
        [JsonProperty("rx_bytes")] public ulong rx_bytes { get; private set; }

        /// <summary> Total number of messages transmitted (produced) to Kafka brokers </summary>
        [JsonProperty("txmsgs")] public ulong txmsgs { get; private set; }

        /// <summary> Total number of message bytes (including framing, such as per-Message framing and MessageSet/batch framing) transmitted to Kafka brokers </summary>
        [JsonProperty("txmsg_bytes")] public ulong txmsg_bytes { get; private set; }

        /// <summary> Total number of messages consumed, not including ignored messages (due to offset, etc), from Kafka brokers. </summary>
        [JsonProperty("rxmsgs")] public ulong rxmsgs { get; private set; }

        /// <summary> Total number of message bytes (including framing) received from Kafka brokers </summary>
        [JsonProperty("rxmsg_bytes")] public ulong rxmsg_bytes { get; private set; }
    }

}
