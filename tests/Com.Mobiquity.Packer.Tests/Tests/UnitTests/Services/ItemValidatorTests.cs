using Com.Mobiquity.Packer.Business.Models;
using Com.Mobiquity.Packer.Services;
using NUnit.Framework;

namespace Com.Mobiquity.Packer.Tests
{
    [TestFixture]
    public class ItemValidatorTests
    {
        private IValidator<Item> itemValidator;
        private Item item;

        [SetUp]
        public void SetUP()
        {
            itemValidator = new ItemValidator();
            item = new Item
            {
                Index = 1,
                Weight = 1530,
                Cost = 20,
                PackageMaxWeightLimit = 800
            };
        }

        [Test]
        public void Item_IsValid_Success()
        {
            //Arrange
            this.item.PackageMaxWeightLimit = 8100;

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, Is.True);
        }
         

        [Test]
        public void ItemCostIsZero_ValidationFail()
        {
            //Arrange
            this.item.Cost = 0;

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, !Is.True);
        }

        [Test]
        public void ItemWeightIsZero_ValidationFail()
        {
            //Arrange
            this.item.Weight = 0;

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, !Is.True);
        }

        [Test]
        public void ItemIndexIsZero_ValidationFail()
        {
            //Arrange
            this.item.Index = 0;

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, !Is.True);
        }


        [Test]
        public void ItemExceedPackageWeightLimit_ValidationFail()
        {
            //Arrange
            this.item.Weight = this.item.Weight - 10; 

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, !Is.True);
        }

        [Test]
        public void ItemWithCostExceedingLimit_ValidationFail()
        {
            //Arrange
            this.item.Cost = 101;

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, !Is.True);
        } 

        [Test]
        public void ItemWithWeightExceedingLimit_ValidationFail()
        { 
            //Arrange
            this.item.Weight = 10001;

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, !Is.True);
        } 

        [Test]
        public void ItemIsNull_ValidationFail()
        {
            //Arrange
            this.item = null;

            //Act
            var actual = itemValidator.IsValid(this.item);

            //Assert
            Assert.That(actual, !Is.True);
        }


        [TearDown]
        public void TearDown()
        {
            itemValidator = null;
            item = null;
        }
    }
}
