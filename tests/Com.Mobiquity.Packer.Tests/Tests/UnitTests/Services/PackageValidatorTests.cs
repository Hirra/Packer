using Com.Mobiquity.Packer.Business.Models;
using Com.Mobiquity.Packer.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace Com.Mobiquity.Packer.Tests
{
    [TestFixture]
    public class PackageValidatorTests
    {
        private IValidator<Package> packageValidator;
        private Package package;

        [SetUp]
        public void SetUP()
        {
            packageValidator = new PackageValidator();
            package = new Package
            {
                PackgeWeight = 8100,
                Items = new List<Item>
                {
                     new Item
                    {
                        Index = 1,
                        Weight = 1530,
                        Cost = 20,
                        PackageMaxWeightLimit = 8100
                    }
                } 
            };
        }

        [Test]
        public void PackageValidation_Success ()
        {
            //Arrange 

            //Act
            var actual = packageValidator.IsValid(this.package);

            //Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void PackgeWeightIsZero_ValidationFail()
        { 
            //Arrange
            this.package.PackgeWeight = 0;

            //Act
            var actual = packageValidator.IsValid(this.package);

            //Assert
            Assert.That(actual, !Is.True);
        }

        [Test]
        public void PackgeWeightExceedMaxLimit_ValidationFail()
        { 
            //Arrange
            this.package.PackgeWeight = 10001;

            //Act
            var actual = packageValidator.IsValid(this.package);

            //Assert
            Assert.That(actual, !Is.True);
        }


        [TearDown]
        public void TearDown()
        {
            packageValidator = null;
            package = null;
        }

    }
}
