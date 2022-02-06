using com.mobiquity.packer.Business.Models;
using com.mobiquity.packer.Common;
using com.mobiquity.packer.Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace com.mobiquity.packer.tests
{
    [TestFixture]
    public class PackageDataParserTests
    {
        private IParser<Package> parser;

        [SetUp]
        public void Setup()
        {
            parser = new PackageDataParser();
        }

        [Test]
        public void Parse_InputDataIsEmptyString_ShouldReurnNullAsParsedResult()
        {
            //Arrange 
            var dataToParse = string.Empty;

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_SuccessFlow_SingleItem_ValidatePackageWeight()
        {
            //Arrange
            var invalidPackgeWeightDelimeter = "8 : (1,7.3,€34)";
            var expected = 8 * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND);

            //Act
            var actual = parser.Parse(invalidPackgeWeightDelimeter);

            //Assert
            Assert.That(actual.PackgeWeight, Is.EqualTo(expected));
        }

        [Test]
        public void Parse_SuccessFlow_SingleItem_ValidateItem()
        {
            //Arrange
            var invalidPackgeWeightDelimeter = "8 : (1,7.3,€34)";
            var expected = new Item
            {
                Index = 1,
                Weight = (int)(7.3 * (int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND)),
                Cost = 34
            };

            //Act
            var actual = parser.Parse(invalidPackgeWeightDelimeter);

            //Assert
            Assert.That(actual.Items.Count, Is.EqualTo(1));
            Assert.That(actual.Items.First().Equals(expected), Is.EqualTo(true));
        }

        [Test]
        public void Parse_SuccessFlow_MultipleItems_ValidateData()
        {
            //Arrange
            var dataToParse = "96 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)  (10,85.31,€29) (11,14.55,€74) (12,3.98,€16) (13,26.24,€55) (14,63.69,€52) (15,76.25,€75)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, !Is.Null);
            Assert.That(actual.PackgeWeight, Is.EqualTo(9600));
            Assert.That(actual.Items.Count, Is.EqualTo(15));
        }

        [Test]
        public void Parse_InvalidPackgeWeightDelimiter_ReturnsNullAsParsedResult()
        {
            //Arrange
            var invalidPackgeWeightDelimeter = "8 $ (1,15.3,€34)";

            //Act
            var actual = parser.Parse(invalidPackgeWeightDelimeter);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_PackgeWeightIsZero_ReutrnsNullAsParsedResult()
        {
            //Arrange
            var dataToParse = "0 : (1,15.3,€34)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_PackgeWeightExceedMaxLimit_ReutrnsNullAsParsedResult()
        {
            //Arrange
            var dataToParse = "1000 : (1,15.3,€34)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_PackgeWeightIsEmpty_ReutrnsNullAsParsedResult()
        {
            //Arrange
            var dataToParse = " : (1,15.3,€34)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_InvalidItemDelimiter_SingleItemInPackage_ReturnsNullAsParsedPackage()
        {
            //Arrange
            var dataToParse = "8 : {1,15.3,€34)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_SingleItemInPackage_ItemCostIsZero_ReturnsNullAsParsedPackage()
        {
            //Arrange
            var dataToParse = "8 : (1,15.3,€0)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_SingleItemInPackage_ItemWeightIsZero_ReturnsNullAsParsedPackage()
        {
            //Arrange
            var dataToParse = "8 : (1,0.0,€34)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_SingleItemInPackage_ItemIndexIsZero_ReturnsNullAsParsedPackage()
        {
            //Arrange
            var dataToParse = "8 : (0,15.3,€34)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_2ItemsInPackage_InvalidItemDelimiterForOneItem_ReturnedPackgeHasOnlyOneItem()
        {
            //Arrange
            var dataToParse = "81 : (1,53.38,€45) {2,80.62,€98)";
            var expected = new Package
            {
                PackgeWeight = 81 * ((int)Math.Pow(10, Constants.DECIMAL_PALCES_TO_ROUND)),
                Items = new System.Collections.Generic.List<Item>
                {
                    new Item
                    {
                        Index = 1,
                        Weight = 5338,
                        Cost = 45
                    }
                }
            };

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual.Items.Count, Is.EqualTo(1));
            Assert.That(actual.PackgeWeight.Equals(expected.PackgeWeight), Is.EqualTo(true));
            Assert.That(actual.Items.First().Equals(expected.Items.First()), Is.EqualTo(true));
        }

        [Test]
        public void Parse_2ItemsInPackage_OneItemExceedPackageWeightLimit_ReturnedPackgeHasOnlyOneItem()
        {
            //Arrange
            var dataToParse = "81 : (1,53.38,€45) (2,88.62,€98)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual.Items.Count, Is.EqualTo(1));
        }

        [Test]
        public void Parse_EmptyStringAsItems_ReturnNullAsParsedResult()
        {
            //Arrange
            var dataToParse = "81 : ";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_EmptyAsItems_ReturnNullAsParsedResult()
        {
            //Arrange
            var dataToParse = "81 : () () ()";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Parse_2ItemsInPackage_OneItemWithInvalidItemSperator_ReturnedPackgeHasOnlyOneItem()
        {
            //Arrange
            var dataToParse = "81 : (1,53.38,€45) (2;88.62;€98)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual.Items.Count, Is.EqualTo(1));
        }

        [Test]
        public void Parse_2ItemsInPackage_OneItemWithIncompleteData_ReturnedPackgeHasOnlyOneItem()
        {
            //Arrange
            var dataToParse = "81 : (1,53.38,€45) (,88.62,€98)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual.Items.Count, Is.EqualTo(1));
        }

        [Test]
        public void Parse_2ItemsInPackage_OneItemWithEmptyData_ReturnedPackgeHasOnlyOneItem()
        {
            //Arrange
            var dataToParse = "81 : (1,53.38,€45) (,,)";

            //Act
            var actual = parser.Parse(dataToParse);

            //Assert
            Assert.That(actual.Items.Count, Is.EqualTo(1));
        }

        [TearDown]
        public void TearDown()
        {
            parser = null;
        }
    }
}
