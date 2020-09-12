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

    public class KafkaBroker
    {

        /// <summary> Broker hostname, port and broker id </summary>
        [JsonProperty("name")] public object name { get; private set; }

        /// <summary> Broker id (-1 for bootstraps) </summary>
        [JsonProperty("nodeid")] public long nodeid { get; private set; }

        /// <summary> Broker hostname </summary>
        [JsonProperty("nodename")] public object nodename { get; private set; }

        /// <summary> Broker source (learned, configured, internal, logical) </summary>
        [JsonProperty("source")] public object source { get; private set; }

        /// <summary> Broker state (INIT, DOWN, CONNECT, AUTH, APIVERSION_QUERY, AUTH_HANDSHAKE, UP, UPDATE) </summary>
        [JsonProperty("state")] public object state { get; private set; }
        //
        //


        /// <summary> Time since last broker state change (microseconds) </summary>
        [JsonProperty("stateage")] public ulong stateage { get; private set; }

        /// <summary> Number of requests awaiting transmission to broker </summary>
        [JsonProperty("outbuf_cnt")] public ulong outbuf_cnt { get; private set; }

        /// <summary> Number of messages awaiting transmission to broker </summary>
        [JsonProperty("outbuf_msg_cnt")] public ulong outbuf_msg_cnt { get; private set; }

        /// <summary> Number of requests in-flight to broker awaiting response </summary>
        [JsonProperty("waitresp_cnt")] public ulong waitresp_cnt { get; private set; }

        /// <summary> Number of messages in-flight to broker awaitign response </summary>
        [JsonProperty("waitresp_msg_cnt")] public ulong waitresp_msg_cnt { get; private set; }

        /// <summary> Total number of requests sent </summary>
        [JsonProperty("tx")] public ulong tx { get; private set; }

        /// <summary> Total number of bytes sent </summary>
        [JsonProperty("txbytes")] public ulong txbytes { get; private set; }

        /// <summary> Total number of transmission errors </summary>
        [JsonProperty("txerrs")] public ulong txerrs { get; private set; }

        /// <summary> Total number of request retries </summary>
        [JsonProperty("txretries")] public ulong txretries { get; private set; }

        /// <summary> Total number of requests timed out </summary>
        [JsonProperty("req_timeouts")] public ulong req_timeouts { get; private set; }

        /// <summary> Total number of responses received </summary>
        [JsonProperty("rx")] public ulong rx { get; private set; }

        /// <summary> Total number of bytes received </summary>
        [JsonProperty("rxbytes")] public ulong rxbytes { get; private set; }

        /// <summary> Total number of receive errors </summary>
        [JsonProperty("rxerrs")] public ulong rxerrs { get; private set; }

        /// <summary> Total number of unmatched correlation ids in response
        /// (typically for timed out requests) </summary>
        [JsonProperty("rxcorriderrs")] public ulong rxcorriderrs { get; private set; }

        /// <summary> Total number of partial MessageSets received.
        /// The broker may return partial responses if the full MessageSet
        /// could not fit in remaining Fetch response size.
        /// </summary>
        [JsonProperty("rxpartial")] public ulong rxpartial { get; private set; }

        /// <summary> Total number of decompression buffer size increases </summary>
        [JsonProperty("zbuf_grow")] public ulong zbuf_grow { get; private set; }

        /// <summary> Total number of buffer size increases (deprecated, unused) </summary>
        [JsonProperty("buf_grow")] public ulong buf_grow { get; private set; }

        /// <summary> Broker thread poll wakeups </summary>
        [JsonProperty("wakeups")] public ulong wakeups { get; private set; }

        /// <summary> Number of connection attempts, including successful and failed,
        /// and name resolution failures.
        /// </summary>
        [JsonProperty("connects")] public ulong connects { get; private set; }

        /// <summary> Number of disconnects (triggered by broker, network, load-balancer, etc.). </summary>
        [JsonProperty("disconnects")] public ulong disconnects { get; private set; }


        //
        //


        /// <summary> Internal producer queue latency in microseconds. See Window stats below </summary>
        [JsonProperty("int_latency")] public LatencyInformation int_latency { get; private set; }

        /// <summary> Internal request queue latency in microseconds.
        /// This is the time between a request is enqueued on the transmit (outbuf) queue
        /// and the time the request is written to the TCP socket.
        /// Additional buffering and latency may be incurred by the TCP stack and network.
        /// </summary>
        [JsonProperty("outbuf_latency")] public LatencyInformation outbuf_latency { get; private set; }

        /// <summary> Broker latency / round-trip time in microseconds. </summary>
        [JsonProperty("rtt")] public LatencyInformation rtt { get; private set; }

        /// <summary> Broker throttling time in milliseconds. </summary>
        [JsonProperty("throttle")] public LatencyInformation throttle { get; private set; }
    }

}
