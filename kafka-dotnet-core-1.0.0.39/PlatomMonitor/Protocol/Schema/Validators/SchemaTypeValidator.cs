using Platom.Protocol.Schema;

namespace Platom.Protocol.Schema.Validators
{
    public class SchemaTypeValidator : IValidator<SchemaBaseType>
    {
        public SchemaFieldEntry Context { get; set; }

        public SchemaTypeValidator(SchemaFieldEntry context)
        {
            this.Context = context;
        }

        public void Validate(SchemaBaseType value)
        {
            if (value == null)
                throw new ValidatorException("Brak informacji o typie danych");

            value.ValidateSchemaInformation();
        }
    }
}