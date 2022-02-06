using Com.Mobiquity.Packer.Services;
using System;

namespace Com.Mobiquity.Packer
{
    /// <summary>
    /// 
    /// </summary>
    public class Packer
    {
        /// <summary>
        /// To prevent any instantiation making the default constructor private
        /// </summary>
        private Packer()
        {
        }

        /// <summary>
        /// API enpoint expose for consumer.For calcualting the optimal combinations for each packge in packge list
        /// </summary>
        /// <param name="filePath">The file path. File contains a list of packages to optimize</param>
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
