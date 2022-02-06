namespace com.mobiquity.packer.Services
{
    public interface IOptimalPackageItemsProducer<T>
    {
        public string ProduceOptimalPackingItems(T dataToOptimize);
    }
}
