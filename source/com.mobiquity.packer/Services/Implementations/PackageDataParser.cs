using Com.Mobiquity.Packer.Business.Models;
using Com.Mobiquity.Packer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Mobiquity.Packer.Services
{
    /// <summary>
    /// Package data parser
    /// </summary>
    /// <seealso cref="Com.Mobiquity.Packer.Services.IParser&lt;Com.Mobiquity.Packer.Business.Models.Package&gt;" />
    public class PackageDataParser : IParser<Package>
    {
        private IValidator<Item> itemValidator;

        public PackageDataParser()
        {
            this.itemValidator = new ItemValidator();
        }

        /// <summary>
        /// Parses the received string data into packge dto.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns cref="Package"></returns>
        public Package Parse(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                Helper.Logger.Debug("Empty package");
                return null;
            }
            var dataToParse = data.Split(Constants.PACKAGE_WEIGHT_DELIMITER);

            var packageDto = new Package();
            packageDto.PackgeWeight = ParsePackageWeight(dataToParse[0]);
            if (packageDto.PackgeWeight > 0)
            {
                packageDto.Items = ParsePackgeItems(dataToParse[1], packageDto.PackgeWeight);
            }  
            return packageDto;
        }

        /// <summary>
        /// Parse package weight
        /// </summary>
        /// <param name="packageWeight"></param>
        /// <returns cref="int></returns>
        private int ParsePackageWeight(string packageWeight)
        {
            if (string.IsNullOrWhiteSpace(packageWeight))
            {
                Helper.Logger.Debug("Package weight limit not provided");
                return 0;
            }

            if (!int.TryParse(packageWeight, out var weightlimit))
            {
                Helper.Logger.Debug("Invalid data content as package weight limit in data string");
                return 0;
            }

            return weightlimit * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND);
        }

        /// <summary>
        /// Parse packge items
        /// </summary>
        /// <param name="itemsDataString"></param>
        /// <param name="packageWeightLimit"></param>
        /// <returns cref="Item"> list of items</returns>
        private List<Item> ParsePackgeItems(string itemsDataString, int packageWeightLimit)
        {
            var items = new List<Item>();
            if (string.IsNullOrWhiteSpace(itemsDataString))
            {
                Helper.Logger.Debug("Items data string is empty");
                return null;
            }

            var itemsToParse = itemsDataString
                                .Split(Constants.PACKAGE_ITEMS_DELIMITER.ToCharArray())
                                .Where(x => !string.IsNullOrWhiteSpace(x))
                                .ToList();

            if (!itemsToParse.Any())
            {
                Helper.Logger.Debug("No items to parse");
                return null;
            }

            foreach (var itemString in itemsToParse)
            {
                var item = PasrePackageItem(itemString, packageWeightLimit); 
                if (itemValidator.IsValid(item))
                {
                    items.Add(item);
                }                    
            }

            return items
                .Take(Constants.UPTO_ITEM_COUNT)
                .ToList();
        }

        /// <summary>
        /// Pasre package item
        /// </summary>
        /// <param name="itemString"></param>
        /// <returns cref="Item"></returns>
        private Item PasrePackageItem(string itemString ,int packageWeightLimit)
        {
            var itemProperties = itemString.Split(Constants.ITEM_PROPERTIES_DELIMITER);
            if (itemProperties.Length == 0)
            {
                Helper.Logger.Debug("Item details data string is empty");
                return null;
            }

            if (itemProperties.Count(x => !string.IsNullOrEmpty(x)) < 3)
            {
                Helper.Logger.Debug("Incomplete item details for parsing");
                return null;
            }

            var item = new Item();

            if (int.TryParse(itemProperties[0], out var index))
            {
                item.Index = index;
            }

            if (double.TryParse(itemProperties[1], out var weight))
            {
                item.Weight = (int)(weight * Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND));
            }

            if (int.TryParse(itemProperties[2].Substring(1), out var cost))
            {
                item.Cost = cost;
            }

            item.PackageMaxWeightLimit = packageWeightLimit;

            return item;
        }
    }
}
