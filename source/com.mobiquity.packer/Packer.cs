using com.mobiquity.packer.Services;
using System;

namespace com.mobiquity.packer
{
    public class Packer
    {
        /// <summary>
        /// To prevent any instantiation making the default constructor private
        /// </summary>
        private Packer()
        {
        }

        /// <summary>
        /// Endpoint for consumer
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        /// <exception cref="com.mobiquity.packer.APIException"></exception>
        public static string pack(string filePath)
        {
            try
            {
                IPacker optimalPacker = new OptimalPacker();
                return optimalPacker.OptimizePacking(filePath);
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message, ex);
            }
        }
    }
}
