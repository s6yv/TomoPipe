using Newtonsoft.Json;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    [PlatomSchemaType("string")]
    public class StringType : SchemaBaseType
    {
        [JsonProperty("minlength")] public int? MinLength { get; set; }
        [JsonProperty("maxlength")] public int? MaxLength { get; set; }


        public override void ValidateSchemaInformation()
        {
            if (MaxLength.HasValue && MinLength.HasValue)
                if (MinLength.Value > MaxLength.Value)
                    throw new ValidatorException($"Długość tekstu minimum jest większa od maksimum");

            if (MaxLength.HasValue && !MinLength.HasValue)
                if (MaxLength.Value < 0)
                    throw new ValidatorException($"Długość tekstu maksimum musi być >= 0");

            if (!MaxLength.HasValue && MinLength.HasValue)
                if (MinLength.Value < 0)
                    throw new ValidatorException($"Długość tekstu minimum musi być >= 0");

            if (MaxLength.HasValue && MaxLength.Value <= 0)
                throw new ValidatorException("Długość tekstu nie może być mniejsza bądź równa 0");
        }

        public override void ValidateValue(object value)
        {
            // Sprawdzanie wartosci null
            if (this.Nullable && value == null)
                return; // jest null, dopuszczony

            if (!this.Nullable && value == null)
                throw new ValidatorException("Wartość null jest niedopuszczalna");

            if (!(value is string))
                throw new ValidatorException($"Wartość pola typu 'string' musi być tekstem a jest {value.GetType().FullName}");

            string svalue = value as string;
            if (this.MinLength.HasValue && svalue.Length < this.MinLength.Value)
                throw new ValidatorException("Wartość tekstowa jest zbyt krótka");
            if (this.MaxLength.HasValue && svalue.Length > this.MaxLength.Value)
                throw new ValidatorException("Wartość tekstowa jest zbyt długa");
        }
    }
}