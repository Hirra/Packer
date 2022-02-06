using Com.Mobiquity.Packer.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Com.Mobiquity.Packer.Services
{
    /// <summary>
    /// File reader
    /// </summary>
    /// <seealso cref="Com.Mobiquity.Packer.Services.IFileReader" />
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Reads the file from an absolute path.
        /// Return list of lines in the file
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        /// <exception cref="Com.Mobiquity.Packer.APIException">
        /// Invalid file path or invalid i/o operation
        /// </exception>
        public List<string> ReadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var errorMessage = "Invalid file path";
                Helper.Logger.Debug(errorMessage);
                throw new APIException(errorMessage);
            }

            try
            {
                var data = File.ReadAllLines(filePath).ToList();
                return data;
            }
            catch (IOException e)
            {
                Helper.Logger.Debug("Erroe while reading file content");
                throw new APIException(e.Message, e);
            }
        }
    }
}
