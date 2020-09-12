using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Platom.Protocol.Schema
{
    public class SchemaFieldEntry
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("type") , JsonConverter(typeof(SchemaBaseTypeJsonConverter))]
        public SchemaBaseType Type { get; set; }

        [JsonProperty("mapping")] public StorageMapping Mapping { get; set; }


        public override string ToString()
        {
            return $"{Name}: {Type.TypeName}";
        }
    }
}
