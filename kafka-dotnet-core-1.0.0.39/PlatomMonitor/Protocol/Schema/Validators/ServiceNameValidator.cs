namespace Platom.Protocol.Schema.Validators
{
    public class ServiceNameValidator : IValidator<string>
    {
        private bool allow_any_service;

        public ServiceNameValidator(bool allowAnyService)
        {
            this.allow_any_service = allowAnyService;
        }

        public void Validate(string value)
        {
            if (value == null)
                throw new ValidatorException("Nazwa nie może być pusta (null)");

            if (this.allow_any_service && value == "*")
                return; // ok, wszystkie usługi
            if (!this.allow_any_service && value == "*")
                throw new ValidatorException("Symbol wszystkich usług (*) jest niedopuszczalny");

            if (value == "")
                throw new ValidatorException("Nazwa nie może być pusta");
        }
    }
}