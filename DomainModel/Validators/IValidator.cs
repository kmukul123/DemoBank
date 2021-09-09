namespace DomainModel.Validators
{
    public interface IValidator<T>
    {
        bool Validate(T input);
    }
}
