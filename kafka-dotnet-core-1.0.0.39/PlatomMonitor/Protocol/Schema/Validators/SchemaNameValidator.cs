namespace Platom.Protocol.Schema.Validators
{
    public class SchemaNameValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidatorException("Brak nazwy schematu");
        }
    }
}