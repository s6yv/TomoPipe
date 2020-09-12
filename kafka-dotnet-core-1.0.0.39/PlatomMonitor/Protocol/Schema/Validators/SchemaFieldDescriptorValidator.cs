namespace Platom.Protocol.Schema.Validators
{
    public class SchemaFieldDescriptorValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidatorException("Opis pola nie może być pusty");
        }
    }
}