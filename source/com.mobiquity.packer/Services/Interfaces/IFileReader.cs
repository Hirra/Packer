using System.Collections.Generic;

namespace Com.Mobiquity.Packer.Services
{
    public interface IFileReader
    {
        public List<string> ReadFile(string filePath);
    }
}
