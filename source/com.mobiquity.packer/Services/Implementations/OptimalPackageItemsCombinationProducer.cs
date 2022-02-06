using com.mobiquity.packer.Business.Models;
using com.mobiquity.packer.Common;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Optimal package items producer
    /// </summary>
    public class OptimalPackageItemsCombinationProducer : IOptimalPackageItemsProducer<Package>
    {
        /// <summary>
        /// Produces the optimal packing items.
        /// 0/1 Knapsack Problem Using Dynamic Programming - is used as logic based to calculat the cost optimal combination of packing items 
        /// </summary>
        /// <param name="dataToOptimize">The data to optimize.</param>
        /// <returns cref="string"></returns>
        public string ProducePackageItemCombination(Package dataToOptimize)
        {
            var package = dataToOptimize;

            if (package.Items == null || !package.Items.Any())
            {

                Helper.Logger.Debug("Package does not contain any items, exiting optimal items packing calculation");
                return Constants.DEFULT_PLACEHOLDER_OPTIMAL_INDEXES;
            }

            int rowsCount = package.Items.Max(x => x.Index);
            int columnCount = package.PackgeWeight;

            OptimalPackage[,] costMatrixForOptimalCombinationCalculation = new OptimalPackage[rowsCount + 1, columnCount + 1];

            SetZeroIndexRowAndColumnValues(rowsCount, columnCount, costMatrixForOptimalCombinationCalculation);

            OptimizePackageItemsCombination(package, rowsCount, columnCount, costMatrixForOptimalCombinationCalculation);

            Helper.Logger.Debug("Optimization completed, get the list of items for packing");
            var indexes = costMatrixForOptimalCombinationCalculation[rowsCount, columnCount].Items.Select(x => x.Index).ToList();

            if (!indexes.Any())
                return Constants.DEFULT_PLACEHOLDER_OPTIMAL_INDEXES;

            return string.Join(",", indexes);
        }

        /// <summary>
        /// Sets the zero index row and column values.
        /// Will be use as base case or minimum values to compare
        /// </summary>
        /// <param name="rowsCount">The rows count.</param>
        /// <param name="columnCount">The column count.</param>
        /// <param name="CostMatrix">The cost matrix.</param>
        private void SetZeroIndexRowAndColumnValues(int rowsCount, int columnCount, OptimalPackage[,] CostMatrix)
        {
            for (int row = 0; row <= rowsCount; row++)
            {
                CostMatrix[row, 0] = new OptimalPackage { Items = new List<Item>() };
            }

            for (int column = 0; column <= columnCount; column++)
            {
                CostMatrix[0, column] = new OptimalPackage { Items = new List<Item>() };
            }
        }

        /// <summary>
        /// Optimize package items combination for maximum cost
        /// Traverse the matrix and updated the cells based on knapsack problem alogithemtic solution using dynamic programming apporach to resolve it
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="maxItemIndex">Maximum no. of the item.</param>
        /// <param name="maxPackageWeight">The maximum package weight.</param>
        /// <param name="costMartix">The cost martix.</param>
        private void OptimizePackageItemsCombination(Package package, int maxItemIndex, int maxPackageWeight, OptimalPackage[,] costMartix)
        {
            Helper.Logger.Debug("Processing packge item combination optimization");

            for (int itemIndex = 1; itemIndex <= maxItemIndex; itemIndex++)
            {
                for (int currenWeightLimit = 1; currenWeightLimit <= maxPackageWeight; currenWeightLimit++)
                {
                    var packingItemAtCurrentIndex = package.Items.FirstOrDefault(x => x.Index == itemIndex);
                    var currentItemWeight = packingItemAtCurrentIndex is null ? 0 : packingItemAtCurrentIndex.Weight;
                    if (currentItemWeight > currenWeightLimit)
                    {
                        costMartix[itemIndex, currenWeightLimit] = costMartix[itemIndex - 1, currenWeightLimit];
                    }
                    else
                    {
                        var tempPackage = costMartix[itemIndex - 1, currenWeightLimit - currentItemWeight];
                        int existingCost = costMartix[itemIndex - 1, currenWeightLimit] != null ? costMartix[itemIndex - 1, currenWeightLimit].PackageNetCost : 0;
                        int newCost = (tempPackage != null ? tempPackage.PackageNetCost : 0) + (packingItemAtCurrentIndex is null ? 0 : packingItemAtCurrentIndex.Cost);

                        if (existingCost > newCost)
                        {
                            costMartix[itemIndex, currenWeightLimit] = costMartix[itemIndex - 1, currenWeightLimit];
                        }
                        else
                        {
                            List<Item> itemList = Helper.DeepCopy<List<Item>>(tempPackage.Items);
                            if (packingItemAtCurrentIndex != null)
                                itemList.Add(packingItemAtCurrentIndex);

                            costMartix[itemIndex, currenWeightLimit] = new OptimalPackage { Items = itemList };
                        }
                    }

                }
            }

        }
    }
}
