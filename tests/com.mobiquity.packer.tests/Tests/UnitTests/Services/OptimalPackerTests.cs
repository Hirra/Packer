using Com.Mobiquity.Packer.Services;
using NUnit.Framework;

namespace Com.Mobiquity.Packer.Tests
{
    [TestFixture]
    public class OptimalPackerTests
    {
        private IPacker packer;

        [SetUp]
        public void Setup()
        {
            packer = new OptimalPacker();
        }

        [Test]
        public void Pack_SuccessFlow()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\example_input";
            var expected = "4\n-\n2,7\n8,9";

            //Act
            var actual = packer.OptimizePacking(filePath);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Pack_SuccessFlow_2()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\sample_input_2";
            var expected = "4\n-\n2,7\n6,8,9,11,12";

            //Act
            var actual = packer.OptimizePacking(filePath);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Pack_FilePathIsIncorrect_ThrowsException()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\no_file_of_this_name";

            //Act & Assert
            Assert.Throws<APIException>(() => packer.OptimizePacking(filePath));
        }

        [Test]
        public void Pack_EmptyFile_ThrowsException()
        {
            //Arrange
            var filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\empty_file";

            //Act & Assert
            Assert.Throws<APIException>(() => packer.OptimizePacking(filePath));
        }


        [TearDown]
        public void TearDown()
        {
            packer = null;
        }
    }
}
