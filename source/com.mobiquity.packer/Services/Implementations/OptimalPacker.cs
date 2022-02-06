using com.mobiquity.packer.Business.Models;
using com.mobiquity.packer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Entry point to logic from consume exposed endpoint
    /// responsible for the calls for file reading, data parsing,
    /// optimial packages calcultion and returning the optimzaed packing information
    /// to consumer endpoint
    /// </summary>
    /// <seealso cref="com.mobiquity.packer.Services.IPacker" />
    public class OptimalPacker : IPacker
    {
        private IFileReader fileReader;
        private IParser<Package> packageParser;
        private IOptimalPackageItemsProducer<Package> optimalItemsProducer;

        /// <summary>
        /// OptimalPacker
        /// </summary>
        public OptimalPacker()
        {
            this.fileReader = new FileReader();
            this.packageParser = new PackageDataParser();
            this.optimalItemsProducer = new OptimalPackageItemsCombinationProducer();
        }

        /// <summary>
        /// Optimizes the packing.
        /// Recieved the file path
        /// Request FileReader for filedata
        /// Recived parsed data from data parser
        /// calls optimal packing producer on parsed data
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>optimized packing details </returns>
        /// <exception cref="com.mobiquity.packer.APIException">
        /// No data to parse
        /// or
        /// Error in packing proess
        /// </exception>
        public string OptimizePacking(string filePath)
        {
            try
            {
                List<string> optimizeItemIndexes = new List<string>();

                var packagesStringList = fileReader.ReadFile(filePath);
                if (!packagesStringList.Any(x => !string.IsNullOrEmpty(x)))
                {
                    string errorMessage = "Prodive file path does not contain any content to parse.";
                    Helper.Logger.Debug(errorMessage);
                    throw new APIException(errorMessage);
                }

                var optimalPackages = new List<string>();
                foreach (var package in packagesStringList)
                {
                    var packageDto = packageParser.Parse(package);
                    if (packageDto != null)
                    {
                        var indexes = optimalItemsProducer.ProducePackageItemCombination(packageDto);
                        optimizeItemIndexes.Add(indexes);
                    }
                    else
                    {
                        optimizeItemIndexes.Add(Constants.DEFULT_PLACEHOLDER_OPTIMAL_INDEXES);
                    }
                }

                return string.Join("\n", optimizeItemIndexes);
            }
            catch (Exception e)
            {
                string errorMessage = "error in packing process";
                Helper.Logger.Debug(errorMessage);
                throw new APIException(errorMessage, e);
            }
        }

    }
}