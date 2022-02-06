using Com.Mobiquity.Packer.Business.Models;
using Com.Mobiquity.Packer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Mobiquity.Packer.Services
{
    public class ItemValidator : IValidator<Item>
    {
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="item"></param>
        /// <returns cref="bool"></returns>
        public bool IsValid(Item item)
        {
            if (item is null || item.Index <= 0 || item.Weight <= 0 || item.Cost <= 0)
            {
                Helper.Logger.Debug("Item is empty or incomplete details");
                return false;
            }

            if (item.Weight > (Constants.ITEM_WEIGHT_LIMIT) * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND))
            {
                Helper.Logger.Debug("Item weight exceeds the allowed item weight limit, this item will be not be considered for packing");
                return false;
            }

            if (item.Weight > item.PackageMaxWeightLimit)
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
