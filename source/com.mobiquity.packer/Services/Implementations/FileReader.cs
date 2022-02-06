using Com.Mobiquity.Packer.Common;
using System.Collections.Generic;
using System.IO; 

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
                List<string> packages = new List<string>();

                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        packages.Add(line);
                    }
                }

                return packages;
            }
            catch (IOException e)
            {
                Helper.Logger.Debug("Erroe while reading file content");
                throw new APIException(e.Message, e);
            }
        }
    }
}
