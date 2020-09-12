using System;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    [PlatomSchemaType("timestamp")]
    public class TimestampType : SchemaBaseType
    {
        //
        public override void ValidateSchemaInformation()
        {
            // żadna akcja nie jest konieczna
        }

        public override void ValidateValue(object value)
        {
            // Sprawdzanie wartosci null
            if (this.Nullable && value == null)
                return; // jest null, dopuszczony

            if (!this.Nullable && value == null)
                throw new ValidatorException("Wartość null jest niedopuszczalna");

            if (!(value is DateTime))
                throw new ValidatorException($"Wartość pola typu 'timestamp' musi być znacznikiem czasu w formacie ISO 8601 a jest {value.GetType().FullName}");
        }
    }
}