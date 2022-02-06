using com.mobiquity.packer.Business.Models;
using com.mobiquity.packer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Package data parser
    /// </summary>
    /// <seealso cref="com.mobiquity.packer.Services.IParser&lt;com.mobiquity.packer.Business.Models.Package&gt;" />
    public class PackageDataParser : IParser<Package>
    { 
        /// <summary>
        /// Parses the received string data into packge dto.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns cref="Package"></returns>
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
                Helper.Logger.Debug("Package weight is zero, removed from further processing");
                return null;
            }

            if (packageWeightLimit > (Constants.PACKAGE_WEIGHT_LIMIT * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND)))
            {
                Helper.Logger.Debug("Package exceed allowed maximum weight limit per package, removed from further processing");
                return null;
            }

            var packageItems = ParsePackgeItems(dataToParse[1], packageWeightLimit);
            if (packageItems == null || !packageItems.Any())
            {
                Helper.Logger.Debug("No items found to pack, removed from further processing");
                return null;
            }

            return new Package
            {
                PackgeWeight = packageWeightLimit,
                Items = packageItems
            };
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
                var item = PasrePackageItem(itemString);
                if (ValidItem(item, packageWeightLimit))
                    items.Add(item);
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
        private Item PasrePackageItem(string itemString)
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

            return item;
        }

        /// <summary>
        /// Valid item data
        /// </summary>
        /// <param name="item"></param>
        /// <param name="packageWeightLimit"></param>
        /// <returns cref="bool"></returns>
        private bool ValidItem(Item item, int packageWeightLimit)
        {
            if (item is null || item.Index <= 0 || item.Weight <= 0 || item.Cost <= 0)
            {
                return false;
            }

            if (item.Weight > (Constants.ITEM_WEIGHT_LIMIT) * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND))
            {
                Helper.Logger.Debug("Item weight exceeds the allowed item weight limit, this item will be not be considered for packing");
                return false;
            }

            if (item.Weight > packageWeightLimit)
            {
                Helper.Logger.Debug("Item weight exceeds the package weight limit, this item will not be considered for packing");
                return false;
            }

            if (item.Cost > Constants.ITEM_COST_LIMIT)
            {
                Helper.Logger.Debug("Item cost exceeds the allowed item cost limit, this item will be not be considered for packing");
                return false;
            }

            return true;
        }
    }
}
