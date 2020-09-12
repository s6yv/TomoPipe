using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Platom.Protocol
{
    /// <summary>
    /// Struktura identyfikująca komunikat w sekwencji komunikatów danej usługi i danego kanału
    /// </summary>
    public class MessageSequence
    {
        [JsonProperty("timestamp")] public DateTime TimeStamp { get; set; }

        [JsonProperty("number"), DefaultValue(null)]
        public long? SequenceNumber { get; set; }

        [JsonProperty("channel"), DefaultValue(null)]
        public string ChannelName { get; set; }

        [JsonProperty("service"), DefaultValue(null)]
        public string ServiceName { get; set; }

        [JsonProperty("schema"), DefaultValue(null)]
        public string SchemaName { get; set; }

        public void Next()
        {
            this.TimeStamp = DateTime.Now;
            this.SequenceNumber++;
        }

        public static MessageSequence CreateProducerSequence(string sericeName, string channelName, string schemaName)
        {
            MessageSequence s = new MessageSequence()
            {
                TimeStamp = DateTime.Now,
                SequenceNumber = 0,
                ChannelName = channelName,
                SchemaName = schemaName,
                ServiceName = sericeName
            };

            return s;
        }
    }
}


