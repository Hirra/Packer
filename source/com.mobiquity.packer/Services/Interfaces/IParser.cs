namespace Com.Mobiquity.Packer.Services
{
    public interface IParser<TResult>
    {
        public TResult Parse(string dataToParse);
    }
}
