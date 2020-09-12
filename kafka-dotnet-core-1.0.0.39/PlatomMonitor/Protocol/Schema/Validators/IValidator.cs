namespace Platom.Protocol.Schema.Validators
{
    public interface IValidator<T>
    {
        void Validate(T value);
    }
}