using System;

namespace Com.Mobiquity.Packer.IntegrationTestConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var output = Com.Mobiquity.Packer.Packer.pack(args[0]);
                Environment.Exit(200);
            }
            catch (Exception)
            {
                Environment.Exit(500);
            }
        }
    }
}