using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Platom.Protocol
{
    public class StatusPayload 
    {
        [JsonProperty("service"), DefaultValue(null)]
        public string ServiceName { get; set; }

        [JsonProperty("publishes"), DefaultValue(new string[0])]
        public string[] PublicationChannels { get; set; }

        [JsonProperty("subscribes"), DefaultValue(new string[0])]
        public string[] SubscribedChannels { get; set; }

        [JsonProperty("next_alive_interval")]
        public int NextAliveInterval { get; set; }

        [JsonProperty("timeout")]
        public int CurrentTimeoutValue { get; set; }

        public static StatusPayload FromSequenceHeaders(string serviceName, IEnumerable<MessageSequence> publishingSequences,
            IEnumerable<MessageSequence> subscribedSequences, int currentTimeout, int nextAliveIterval)
        {
            if (publishingSequences == null)
                throw new NullReferenceException("publishingSequences");
            if (subscribedSequences == null)
                throw new NullReferenceException("subscribedSequences");

            foreach (MessageSequence seq in publishingSequences.Concat(subscribedSequences))
                if (serviceName != seq.ServiceName)
                    throw new ProtocolViolationException("Nazwa usługi nie jest spójna");


            StatusPayload sp = new StatusPayload();
            sp.ServiceName = serviceName;
            sp.PublicationChannels = publishingSequences.Select(x => x.ChannelName).ToArray();
            sp.SubscribedChannels = subscribedSequences.Select(x => x.ChannelName).ToArray();
            sp.CurrentTimeoutValue = currentTimeout;
            sp.NextAliveInterval = nextAliveIterval;

            return sp;
        }
    }
}
