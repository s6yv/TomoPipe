using System;
using Platom.Protocol.Schema.Exceptions;


namespace Platom.Protocol.Schema.Validators
{
    public static class EntryValidator
    {

        public static void Validate<T>(T value, string errorMessage, IValidator<T> validator)
        {
            if (validator == null)
                throw new NullReferenceException("validator");
            try
            {
                validator.Validate(value);
            }
            catch (ValidatorException ve)
            {
                string message = $"{errorMessage}: {ve.Message}";
                throw new ValidatorException(message, ve);
            }
        }
    }
}