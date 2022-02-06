using System;

namespace com.mobiquity.packer.Business.Models
{
    /// <summary>
    /// Items withn a packge
    /// </summary>
    [Serializable]
    public class Item
    {
        public int Index { get; set; }
        public int Weight { get; set; }
        public int Cost { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                var other = obj as Item;
                if (this.Index == other.Index && this.Weight == other.Weight && this.Cost == other.Cost)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
