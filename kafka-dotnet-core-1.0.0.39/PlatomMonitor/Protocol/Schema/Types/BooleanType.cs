using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    [PlatomSchemaType("boolean")]
    public class BooleanType : SchemaBaseType
    {
        //

        public override void ValidateSchemaInformation()
        {
            // akcja nie jest konieczna
        }

        public override void ValidateValue(object value)
        {
            // Sprawdzanie wartosci null
            if (this.Nullable && value == null)
                return; // jest null, dopuszczony

            if (!this.Nullable && value == null)
                throw new ValidatorException("Wartość null jest niedopuszczalna");

            if (!(value is long) && !(value is bool))
                throw new ValidatorException($"Wartość pola 'boolean' może być liczbą 0/1 lub symbolem true/false a jest {value.GetType().FullName}");

            bool? bvalue = null;
            if (value is long)
            {
                // przypadek: liczba całkowita
                long lvalue = (long) value;
                if (lvalue == 0)
                    bvalue = false;
                else if (lvalue == 1)
                    bvalue = true;
                else
                    throw new ValidatorException("Wartość pola 'boolean' może być liczbą 0/1");
            }

            if (value is bool)
                bvalue = (bool) value;
        }
    }
}