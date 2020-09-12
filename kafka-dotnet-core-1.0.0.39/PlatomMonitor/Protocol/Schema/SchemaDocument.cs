using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Platom.Protocol.Schema
{
    public class SchemaDocument
    {
        [JsonProperty("header")] public SchemaHeader Header { get; set; }

        [JsonProperty("fields")] public List<SchemaFieldEntry> Entries { get; set; }
    }
}   
