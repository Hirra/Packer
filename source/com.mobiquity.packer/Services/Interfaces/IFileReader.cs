using System.Collections.Generic;

namespace com.mobiquity.packer.Services
{
    public interface IFileReader
    {
        public List<string> ReadFile(string filePath);
    }
}
