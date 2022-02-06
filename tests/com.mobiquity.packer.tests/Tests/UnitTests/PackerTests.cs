using NUnit.Framework;

namespace com.mobiquity.packer.tests
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
        public void Pack_FilePathIsIncorrect_ThrowsException()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\no_file_of_this_name";

            //Act & Assert
            Assert.Throws<APIException>(() => Packer.pack(filePath));
        }
    }

}
