using NUnit.Framework;

namespace Com.Mobiquity.Packer.Tests
{
    [TestFixture]
    public class PackerTests
    {
        public Packer packer;

        [Test]
        public void Pack_SuccessFlow()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\example_input";
            var expected = "4\n-\n2,7\n8,9";

            //Act
            var actual = Packer.pack(filePath);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Pack_SuccessFlow_ItemsUnordered()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\example_input_with_items_unordered";
            var expected = "4\n-\n2,7\n8,9";

            //Act
            var actual = Packer.pack(filePath);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Pack_SuccessFlow_UnorderedItemIndexes()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\indexes_are_not_ordered";
            var expected = "5\n-\n1,8\n6,7";

            //Act
            var actual = Packer.pack(filePath);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Pack_FilePathIsIncorrect_ThrowsException()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\no_file_of_this_name";

            //Act & Assert
            Assert.Throws<APIException>(() => Packer.pack(filePath));
        }
    }

}
