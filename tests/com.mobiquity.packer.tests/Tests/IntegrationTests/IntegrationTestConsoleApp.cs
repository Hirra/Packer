using NUnit.Framework;
using System.Diagnostics;

namespace Com.Mobiquity.Packer.Tests
{
    [TestFixture]
    public class IntegrationTestConsoleApp
    {
        [Test]
        public void RefrencePackerInConsoleApp_SuccessFlow_Return200()
        {
            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            var filePath = currentDirectory + "\\TestData\\example_input";

            var consoleAppPath = currentDirectory.Replace("com.mobiquity.packer.tests", "com.mobiquity.packer.IntegrationTestConsoleApp") + "\\com.mobiquity.packer.IntegrationTestConsoleApp.exe";

            Process process = new Process();
            process.StartInfo.FileName = consoleAppPath;
            process.StartInfo.Arguments = filePath;
            process.Start();
            process.WaitForExit();

            var actual = process.ExitCode;
            Assert.That(actual, Is.EqualTo(200));
        }

        [Test]
        public void RefrencePackerInConsoleApp_ErrorFlow_Return500()
        {
            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            var filePath = currentDirectory + "\\TestData\\invalidFileName";

            var consoleAppPath = currentDirectory
                                .Replace("com.mobiquity.packer.tests", "com.mobiquity.packer.IntegrationTestConsoleApp")
                                + "\\com.mobiquity.packer.IntegrationTestConsoleApp.exe";

            Process process = new Process();
            process.StartInfo.FileName = consoleAppPath;
            process.StartInfo.Arguments = filePath;
            process.Start();
            process.WaitForExit();

            var actual = process.ExitCode;
            Assert.That(actual, Is.EqualTo(500));
        }
    }
}
