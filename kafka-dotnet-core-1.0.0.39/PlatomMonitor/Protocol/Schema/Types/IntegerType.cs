using System;
using Newtonsoft.Json;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    [PlatomSchemaType("integer")]
    public class IntegerType : SchemaBaseType
    {
        [JsonProperty("minimum")] public Int64? Minimum { get; set; }
        [JsonProperty("maximum")] public Int64? Maximum { get; set; }


        public override void ValidateSchemaInformation()
        {
            if (Maximum.HasValue && Minimum.HasValue)
                if (Minimum.Value > Maximum.Value)
                    throw new ValidatorException($"Wartość minimum jest większa od maksimum");
        }

        public override void ValidateValue(object value)
        {
            // Sprawdzanie wartosci null
            if (this.Nullable && value == null)
                return; // jest null, dopuszczony

            if (!this.Nullable && value == null)
                throw new ValidatorException("Wartość null jest niedopuszczalna");

            if (!(value is long))
                throw new ValidatorException($"Wartość pola typu 'integer' musi być liczbą całkowitą a jest {value.GetType().FullName}");

            long lvalue = (long)value;
            if (this.Minimum.HasValue && lvalue < this.Minimum.Value)
                throw new ValidatorException("Wartość liczbowa jest zbyt mała");
            if (this.Maximum.HasValue && lvalue > this.Maximum.Value)
                throw new ValidatorException("Wartość liczbowa jest zbyt duża");

        }
    }
}