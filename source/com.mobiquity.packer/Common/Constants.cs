namespace com.mobiquity.packer.Common
{
    public static class Constants
    {
        // service constraints
        public static readonly int PACKAGE_WEIGHT_LIMIT = 100;
        public static readonly int ITEM_WEIGHT_LIMIT = 100;
        public static readonly int ITEM_COST_LIMIT = 100;
        public static readonly int UPTO_ITEM_COUNT = 15;

        //service contants
        public static readonly char PACKAGE_WEIGHT_DELIMITER = ':';
        public static readonly char ITEM_PROPERTIES_DELIMITER = ',';

        public static readonly string PACKAGE_ITEMS_DELIMITER = "()";
        public static readonly string DEFULT_PLACEHOLDER_OPTIMAL_INDEXES = "-";

        public static readonly int DECIMAL_PALCES_TO_ROUND = 2;
    }
}
