using System.Collections.Generic;

namespace com.mobiquity.packer.Business.Models
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
