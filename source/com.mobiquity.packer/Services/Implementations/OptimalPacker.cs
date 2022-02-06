using Com.Mobiquity.Packer.Business.Models;
using Com.Mobiquity.Packer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Mobiquity.Packer.Services
{
    /// <summary>
    /// Entry point to logic from consume exposed endpoint
    /// responsible for the calls for file reading, data parsing,
    /// optimial packages calcultion and returning the optimzaed packing information
    /// to consumer endpoint
    /// </summary>
    /// <seealso cref="Com.Mobiquity.Packer.Services.IPacker" />
    public class OptimalPacker : IPacker
    {
        private IFileReader fileReader;
        private IParser<Package> packageParser;
        private IValidator<Package> packageValidator;
        private IOptimalPackageItemsProducer<Package> optimalItemsProducer;

        /// <summary>
        /// OptimalPacker
        /// </summary>
        public OptimalPacker()
        {
            this.fileReader = new FileReader();
            this.packageParser = new PackageDataParser();
            this.packageValidator = new PackageValidator();
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
        /// <exception cref="Com.Mobiquity.Packer.APIException">
        /// No data to parse
        /// or
        /// Error in packing proess
        /// </exception>
        public string OptimizePacking(string filePath)
        {
            try
            {
                List<string> optimizeItemIndexes = new List<string>();
                List<string> packagesStringList = ReadFileContents(filePath);

                var optimalPackages = new List<string>();
                foreach (var package in packagesStringList)
                { 
                    var packageDto = packageParser.Parse(package); 
                    if (!packageValidator.IsValid(packageDto))
                    {
                        Helper.Logger.Debug("Parsed package failed validation"); 
                        optimizeItemIndexes.Add(Constants.DEFULT_PLACEHOLDER_OPTIMAL_INDEXES);
                        continue;
                    }
                    else 
                    {
                        var indexes = optimalItemsProducer.ProducePackageItemCombination(packageDto);
                        optimizeItemIndexes.Add(indexes);
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

        private List<string> ReadFileContents(string filePath)
        {
            var packagesStringList = fileReader.ReadFile(filePath);
            if (!packagesStringList.Any(x => !string.IsNullOrEmpty(x)))
            {
                string errorMessage = "Prodive file path does not contain any content to parse.";
                Helper.Logger.Debug(errorMessage);
                throw new APIException(errorMessage);
            }

            return packagesStringList;
        }
    }
}