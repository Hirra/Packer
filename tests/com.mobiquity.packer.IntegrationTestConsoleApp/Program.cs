using System;

namespace Com.Mobiquity.Packer.IntegrationTestConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
        
            //absolute file path to a file on system
            var filePath = @"C:\temp\test_file";
            var output = Com.Mobiquity.Packer.Packer.pack(filePath); 
            Console.WriteLine(output);
        }
    }
}