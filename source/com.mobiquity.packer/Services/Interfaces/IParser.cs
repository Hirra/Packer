namespace com.mobiquity.packer.Services
{
    public interface IParser<TResult>
    {
        public TResult Parse(string dataToParse);
    }
}
