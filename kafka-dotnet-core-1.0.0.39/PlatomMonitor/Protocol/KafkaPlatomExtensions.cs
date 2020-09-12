using System.Threading;
using System.Threading.Tasks;
using KafkaNet;
using Platom.Protocol;

namespace TestServicesRunner
{
    public static class KafkaPlatomExtensions
    {
        //public static void SendPlatomMessage(this Producer kafkaProducer, CommonMessage msg)
        //    => SendPlatomMesssage(kafkaProducer, msg, CancellationToken.None);
        
        public static Task SendCommonMessage(this Producer kafkaProducer, CommonMessage msg, CancellationToken ct)
        {
            string jmsg = msg.AsJSON();
            KafkaNet.Protocol.Message kmsg = new KafkaNet.Protocol.Message(jmsg);
            Task t = kafkaProducer.SendMessageAsync(msg.Sequence.ChannelName, new[] {kmsg});
            return t;
        }

        public static Task SendStatusMessage(this Producer kafkaProducer, StatusMessage msg, CancellationToken ct)
        {
            string jmsg = msg.AsJSON();
            KafkaNet.Protocol.Message kmsg = new KafkaNet.Protocol.Message(jmsg);
            Task t = kafkaProducer.SendMessageAsync(msg.Sequence.ChannelName, new[] { kmsg });
            return t;
        }
    }
}