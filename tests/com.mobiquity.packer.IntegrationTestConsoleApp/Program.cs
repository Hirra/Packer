using System;

namespace com.mobiquity.packer.IntegrationTestConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var output = Packer.pack(args[0]);
                Environment.Exit(200);
            }
            catch (Exception ex)
            {
                Environment.Exit(500);
            }
        }
    }
}