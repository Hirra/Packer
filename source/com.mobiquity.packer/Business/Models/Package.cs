using System.Collections.Generic;

namespace Com.Mobiquity.Packer.Business.Models
{
    /// <summary>
    /// Collection of items to be shipped
    /// </summary>
    public class Package
    {
        public int PackgeWeight { get; set; }

        public List<Item> Items { get; set; }
    }
}
