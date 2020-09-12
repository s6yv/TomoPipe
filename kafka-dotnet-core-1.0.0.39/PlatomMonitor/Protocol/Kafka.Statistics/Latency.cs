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
    /// <summary>
    /// Rolling window statistics.
    /// The values are in microseconds unless otherwise stated.
    /// </summary>
    public class LatencyInformation
    {
        [JsonProperty("min")] public ulong min { get; private set; }
        [JsonProperty("max")] public ulong max { get; private set; }
        [JsonProperty("avg")] public ulong avg { get; private set; }
        [JsonProperty("sum")] public ulong sum { get; private set; }
        [JsonProperty("stddev")] public ulong stddev { get; private set; }
        [JsonProperty("p50")] public ulong p50 { get; private set; }
        [JsonProperty("p75")] public ulong p75 { get; private set; }
        [JsonProperty("p90")] public ulong p90 { get; private set; }
        [JsonProperty("p95")] public ulong p95 { get; private set; }
        [JsonProperty("p99")] public ulong p99 { get; private set; }
        [JsonProperty("p99_99")] public ulong p99_99 { get; private set; }
        [JsonProperty("outofrange")] public ulong outofrange { get; private set; }
        [JsonProperty("hdrsize")] public ulong hdrsize { get; private set; }
        [JsonProperty("cnt")] public ulong cnt { get; private set; }
    }

}
