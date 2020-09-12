using System.Collections.Generic;
using Newtonsoft.Json;
using Platom.Protocol;

namespace Platom.Protocol
{
    /// <summary>
    /// Komunikat przesyłany w sieci komunikacji rozproszonej projektu Platom
    /// </summary>
    public class CommonMessage
    {
        /// <summary> Struktura identyfikująca komunikat w sekwencji komunikatów danej usługi i danego kanału </summary>
        [JsonProperty("sequence")] public MessageSequence Sequence { get; set; }

        ///// <summary> Zawartość komunikatu </summary>
        //[JsonProperty("payload")] public BasePayload Payload { get; set; }

        public CommonMessage()
        {
            this.Sequence = null;
        }

        public CommonMessage(MessageSequence sequence)
        {
            this.Sequence = sequence;
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


    /// <summary>
    /// Komunikat przesyłany w sieci komunikacji rozproszonej projektu Platom
    /// </summary>
    public class CommonMessage_AbstractPayload : CommonMessage
    {
        /// <summary> Zawartość komunikatu </summary>
        [JsonProperty("payload")] public AbstractPayload Payload { get; set; }

        public CommonMessage_AbstractPayload()
        {
            this.Sequence = null;
            this.Payload = null;
        }

        public CommonMessage_AbstractPayload(MessageSequence sequence, AbstractPayload payload)
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

    /// <summary>
    /// Komunikat przesyłany w sieci komunikacji rozproszonej projektu Platom
    /// </summary>
    public class CommonMessage_DictionaryPayload : CommonMessage
    {
        /// <summary> Zawartość komunikatu </summary>
        [JsonProperty("payload")] public IDictionary<string,object> Payload { get; set; }


        public CommonMessage_DictionaryPayload()
        {
          this.Payload = new Dictionary<string, object>();
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