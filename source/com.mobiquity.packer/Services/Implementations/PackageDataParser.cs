using com.mobiquity.packer.Business.Models;
using com.mobiquity.packer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Package detail parser
    /// </summary>
    /// <seealso cref="com.mobiquity.packer.Services.IParser&lt;com.mobiquity.packer.Business.Models.Package&gt;" />
    public class PackageDataParser : IParser<Package>
    {
        public PackageDataParser()
        {
        }

        /// <summary>
        /// Parses the received string data
        /// into packge dto.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public Package Parse(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                Helper.Logger.Debug("Package details missing");
                return null;
            }

            var dataToParse = data.Split(Constants.PACKAGE_WEIGHT_DELIMITER);

            var packageWeightLimit = ParsePackageWeight(dataToParse[0]);
            if (packageWeightLimit == 0)
            {
                Helper.Logger.Debug("Package weight is zero, package removed from further processing");
                return null;
            }

            if (packageWeightLimit > (Constants.PACKAGE_WEIGHT_LIMIT * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND)))
            {
                Helper.Logger.Debug("Package exceed allowed maximum weight limit for a package, package removed from further processing");
                return null;
            }

            var packageItems = ParsePackgeItems(dataToParse[1], packageWeightLimit);
            if (packageItems == null || !packageItems.Any())
            {
                Helper.Logger.Debug("No items found to pack , package removed from further processing");
                return null;
            }

            return new Package
            {
                PackgeWeight = packageWeightLimit,
                Items = packageItems
            };
        }

        private int ParsePackageWeight(string packageWeight)
        {
            if (string.IsNullOrWhiteSpace(packageWeight))
            {
                Helper.Logger.Debug("Max weight not provided");
                return 0;
            }

            if (!int.TryParse(packageWeight, out var weightlimit))
            {
                Helper.Logger.Debug("Max weight is in valid format");
                return 0;
            }

            return weightlimit * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND);
        }

        private List<Item> ParsePackgeItems(string itemsDataString, int packageWeightLimit)
        {
            var items = new List<Item>();
            if (string.IsNullOrWhiteSpace(itemsDataString))
            {
                Helper.Logger.Debug("Not items provided");
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
                var item = PasrePackgeItem(itemString);
                if (ValidItem(item, packageWeightLimit))
                    items.Add(item);
            }

            return items
                .Take(Constants.UPTO_ITEM_COUNT)
                .ToList();
        }

        private Item PasrePackgeItem(string itemString)
        {
            var itemProperties = itemString.Split(Constants.ITEM_PROPERTIES_DELIMITER);
            if (itemProperties.Length == 0)
            {
                Helper.Logger.Debug("Items data is empty");
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

            return item;
        }

        private bool ValidItem(Item item, int packageWeightLimit)
        {
            if (item is null || item.Index <= 0 || item.Weight <= 0 || item.Cost <= 0)
            {
                return false;
            }

            if (item.Weight > (Constants.ITEM_WEIGHT_LIMIT) * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND))
            {
                Helper.Logger.Debug("Item weight exceeds the allowed limit, this item will be not be considered for packing");
                return false;
            }

            if (item.Weight > packageWeightLimit)
            {
                Helper.Logger.Debug("Item weight exceeds the package weight limit, this item will not be considered for packing");
                return false;
            }

            if (item.Cost > Constants.ITEM_COST_LIMIT)
            {
                Helper.Logger.Debug("Item cost exceeds the allowed limit, this item will be not be considered for packing");
                return false;
            }

            return true;
        }
    }
}
