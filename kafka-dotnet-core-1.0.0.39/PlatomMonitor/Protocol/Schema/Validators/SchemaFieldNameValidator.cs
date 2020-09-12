namespace Platom.Protocol.Schema.Validators
{
    public class SchemaFieldNameValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidatorException("Nazwa pola nie może być pusta");
        }
    }
}