﻿namespace com.mobiquity.packer.Common
{
    public class PackerLogger
    {
        // public  ILogger logger;

        //      public  PackerLogger()
        //      {
        //          using var loggerFactory = LoggerFactory.Create(builder =>
        //          {
        //              builder.AddConsole();
        //          });
        //          logger = loggerFactory.CreateLogger<PackerLogger>();
        //      } 


        //private static ILoggerFactory _Factory = null;

        //public static void ConfigureLogger(ILoggerFactory factory)
        //{
        //	factory.AddDebug(LogLevel.None).AddStackify();
        //	factory.AddFile("logFileFromHelper.log"); //serilog file extension
        //}

        //public static ILoggerFactory LoggerFactory
        //{
        //	get
        //	{
        //		if (_Factory == null)
        //		{
        //			_Factory = new LoggerFactory();
        //			ConfigureLogger(_Factory);
        //		}
        //		return _Factory;
        //	}
        //	set { _Factory = value; }
        //}
        //public static ILogger CreateLogger() => LoggerFactory.CreateLogger();
    }
}