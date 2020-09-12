using Newtonsoft.Json;

namespace Platom.Protocol.Schema
{
    public abstract class SchemaBaseType
    {
        /// <summary>Nazwa typu danych</summary>
        [JsonProperty("name")]
        public string TypeName { get; set; }


        /// <summary>Czy pole w komunikacie może przyjąć wartość NULL?</summary>
        [JsonProperty("nullable")]
        public bool Nullable { get; set; }


        public abstract void ValidateSchemaInformation();
        public abstract void ValidateValue(object value);
    }
}