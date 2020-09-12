using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    [PlatomSchemaType("vector")]
    public class VectorType : SchemaBaseType
    {
        [JsonProperty("minlength"), DefaultValue(null)] public int? MinLength { get; set; }
        [JsonProperty("maxlength"), DefaultValue(null)] public int? MaxLength { get; set; }

        //[JsonProperty("default")] public object DefaultValue{ get; set; }

        [JsonProperty("type"), JsonConverter(typeof(SchemaBaseTypeJsonConverter))]
        public SchemaBaseType Type { get; set; }

        public override void ValidateSchemaInformation()
        {
            if (MaxLength.HasValue && !MinLength.HasValue)
                if (MaxLength.Value < 0)
                    throw new ValidatorException($"Długość maksimum musi być >= 0");

            if (!MaxLength.HasValue && MinLength.HasValue)
                if (MinLength.Value < 0)
                    throw new ValidatorException($"Długość minimum musi być >= 0");


            if (MaxLength.HasValue && MinLength.HasValue)
                if (MinLength.Value > MaxLength.Value)
                    throw new ValidatorException($"Długość minimum jest większa od maksimum");

            // Sprawdź typ elementów
            if (this.Type == null)
                throw new ValidatorException("Brak informacji o typie danych wektora");

            this.Type.ValidateSchemaInformation();

            //// sprawdź wartość domyślną
            //this.Type.ValidateValue(this.DefaultValue);
        }

        public override void ValidateValue(object value)
        {
            // Sprawdzanie wartosci null
            if (this.Nullable && value == null)
                return; // jest null, dopuszczony

            if (!this.Nullable && value == null)
                throw new ValidatorException("Wartość null jest niedopuszczalna");

            if (!(value is JArray))
                throw new ValidatorException($"Wartość pola typu 'vector' musi być tablicą/listą elementów (JArray) a jest {value.GetType().FullName}");

            JArray jarray = value as JArray;
            if (this.MinLength.HasValue && jarray.Count < this.MinLength.Value)
                throw new ValidatorException("Liczba elementów jest zbyt mała");
            if (this.MaxLength.HasValue && jarray.Count > this.MaxLength.Value)
                throw new ValidatorException("Liczba elementów jest zbyt duża");

            // sprwdzenie danych w wektorze
            foreach (JValue jvalue in jarray)
                this.Type.ValidateValue(jvalue.Value);
        }
    }
}