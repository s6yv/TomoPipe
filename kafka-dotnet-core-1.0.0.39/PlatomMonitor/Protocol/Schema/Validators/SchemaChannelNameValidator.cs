namespace Platom.Protocol.Schema.Validators
{
    public class SchemaChannelNameValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidatorException("Nazwa kanału nie może być pusta");


            // Sprawdzenie znaków, wymagane przez Kafkę:
            // https://github.com/apache/kafka/blob/trunk/clients/src/main/java/org/apache/kafka/common/internals/Topic.java#L75
            foreach (char c in value)
            {
                bool validChar = (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || c == '.' ||
                                    c == '_' || c == '-';
                if (!validChar)
                    throw new ValidatorException($"W nazwie kanału znak '{c}' (kod ascii {(int)c}) jest niedopuszczalny.");
            }
        }
    }
}