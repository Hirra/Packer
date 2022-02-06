using Com.Mobiquity.Packer.Services;
using NUnit.Framework;

namespace Com.Mobiquity.Packer.Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        private IFileReader fileReader;
        private string filePath;

        [SetUp]
        public void Setup()
        {
            fileReader = new FileReader();
            filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\example_input";
        }

        [Test]
        public void ReadFile_SuccessFlow()
        {
            //Arrange
            var expected = 4;

            //Act
            var fileDataLinesCollection = fileReader.ReadFile(filePath);

            //assert
            Assert.That(fileDataLinesCollection.Count, Is.EqualTo(expected));
        }

        [Test]
        public void ReadFile_FilePathEmptyString_ThrowsException()
        {
            //Act & Assert
            Assert.Throws<APIException>(() => fileReader.ReadFile(string.Empty));
        }

        [Test]
        public void ReadFile_FileDoesNotExistAtPath_ThrowsException()
        {
            //Arrange
            this.filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\file_does_not_exists_at_path";

            //Act & Assert
            Assert.Throws<APIException>(() => fileReader.ReadFile(filePath));
        }

        [Test]
        public void ReadFile_FileContentEmpty_EmptyList()
        {
            //Arrange
            this.filePath = $"{TestContext.CurrentContext.TestDirectory}\\" + "TestData\\empty_file";

            //Act
            var actual = fileReader.ReadFile(filePath);

            //Assert
            Assert.That(actual.Count, Is.EqualTo(0));
        }

        [TearDown]
        public void TearDown()
        {
            fileReader = null;
        }
    }
}
