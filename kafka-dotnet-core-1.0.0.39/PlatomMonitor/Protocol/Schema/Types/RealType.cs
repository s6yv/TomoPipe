using Newtonsoft.Json;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    [PlatomSchemaType("real")]
    public class RealType : SchemaBaseType
    {
        [JsonProperty("minimum")] public double? Minimum { get; set; }
        [JsonProperty("maximum")] public double? Maximum { get; set; }


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

            if (!(value is double) && !(value is long))
                throw new ValidatorException("Wartość pola typu 'real' musi być liczbą całkowitą lub zmiennoprzecinkową (z separującą kropką) a jest {value.GetType().FullName}");

            double lvalue = (value is double) ? (double)value : (long)value;
            if (this.Minimum.HasValue && lvalue < this.Minimum.Value)
                throw new ValidatorException("Wartość liczbowa jest zbyt mała");
            if (this.Maximum.HasValue && lvalue > this.Maximum.Value)
                throw new ValidatorException("Wartość liczbowa jest zbyt duża");

        }

    }
}