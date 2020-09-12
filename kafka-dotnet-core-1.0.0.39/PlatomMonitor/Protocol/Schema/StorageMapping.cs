using Newtonsoft.Json;

namespace Platom.Protocol.Schema
{
    public class StorageMapping
    {
        [JsonProperty("column")] public string ColumnName { get; set; }

        [JsonProperty("serialization")] public string SerializationMethod { get; set; }
    }
}