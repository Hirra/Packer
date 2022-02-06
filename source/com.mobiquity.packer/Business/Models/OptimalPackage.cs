using System.Collections.Generic;
using System.Linq;

namespace Com.Mobiquity.Packer.Business.Models
{
    /// <summary>
    /// Represnt entity containing items of optimal combination to maximize the cost
    /// data structure for storing values during optimal combination calculation
    /// </summary>
    public class OptimalPackage
    {
        public int PackageNetCost => this.Items.Sum(x => x.Cost);

        public List<Item> Items { get; set; }
    }
}
