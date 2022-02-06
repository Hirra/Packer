using Com.Mobiquity.Packer.Business.Models;
using Com.Mobiquity.Packer.Common;
using System;
using System.Linq;

namespace Com.Mobiquity.Packer.Services
{
    public class PackageValidator : IValidator<Package>
    {
        public bool IsValid(Package package)
        {
             
            if (package.PackgeWeight == 0)
            { 
                Helper.Logger.Debug("Package weight is zero, removed from further processing");
                return false;
            }

            if (package.PackgeWeight > (Constants.PACKAGE_WEIGHT_LIMIT * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND)))
            {
                Helper.Logger.Debug("Package exceed allowed maximum weight limit per package, removed from further processing");
                return false;
            }

            if (package.Items == null || !package.Items.Any())
            {
                Helper.Logger.Debug("No items found to pack, removed from further processing");
                return false;
            }

            return true;
        }
    }
}
