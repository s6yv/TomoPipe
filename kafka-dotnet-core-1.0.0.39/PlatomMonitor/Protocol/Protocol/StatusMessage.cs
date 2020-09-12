using Newtonsoft.Json;
using Platom.Protocol;

namespace Platom.Protocol
{
    /// <summary>
    /// Komunikat przesyłany w sieci komunikacji rozproszonej projektu Platom, kanałem status.
    /// </summary>
    public class StatusMessage
    {
        /// <summary> Struktura identyfikująca komunikat w sekwencji komunikatów danej usługi kanału status</summary>
        [JsonProperty("sequence")] public MessageSequence Sequence { get; set; }

        /// <summary> Zawartość komunikatu </summary>
        [JsonProperty("payload")] public StatusPayload Payload { get; set; }


        public StatusMessage(MessageSequence sequence, StatusPayload payload)
        {
            this.Sequence = sequence;
            this.Payload = payload;
        }

        /// <summary>
        /// Metoda pobiera treść komunikatu w formie tekstu JSON
        /// </summary>
        /// <returns>Tekst JSON</returns>
        public string AsJSON()
        {
            string str = JsonConvert.SerializeObject(this);
            return str;
        }
    }
}