namespace Platom.Protocol.Schema.Validators
{
    public class SchemaDescriptorValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidatorException("Opis nie może być pusty");
        }
    }
}