namespace Com.Mobiquity.Packer.Services
{
    public interface IOptimalPackageItemsProducer<T>
    {
        public string ProducePackageItemCombination(T dataToOptimize);
    }
}
