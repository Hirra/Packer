namespace Com.Mobiquity.Packer.Services
{
    public interface IValidator<T>
    {
        public bool IsValid(T TObject);
    }
}
