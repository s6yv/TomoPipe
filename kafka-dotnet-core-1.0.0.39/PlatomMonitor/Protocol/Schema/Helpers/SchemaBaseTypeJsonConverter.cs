using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Platom.Protocol.Schema
{
    public class SchemaBaseTypeJsonConverter : JsonConverter<SchemaBaseType>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, SchemaBaseType value, JsonSerializer serializer)
        {
        }

        public override SchemaBaseType ReadJson(JsonReader reader, Type objectType, SchemaBaseType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            SchemaBaseType target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        protected  SchemaBaseType Create(Type objectType, JObject jObject)
        {
            if (jObject["name"] == null)
                throw new Newtonsoft.Json.JsonReaderException("Brak pola 'name'");
            string type_name = jObject.Value<string>("name");

            // znajdź typ o odpowiednim atrybucie
            Assembly self = Assembly.GetExecutingAssembly();
            foreach (Type type in self.GetTypes())
            {
                PlatomSchemaTypeAttribute attr = type.GetCustomAttribute<PlatomSchemaTypeAttribute>();
                if (attr == null)
                    continue;

                if (attr.TypeName == type_name)
                {
                    SchemaBaseType obj = Activator.CreateInstance(type) as SchemaBaseType;
                    return obj;
                }
            }

            throw new Newtonsoft.Json.JsonReaderException($"Nieznany typ '{type_name}'");
        }


    }
}