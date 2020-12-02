﻿using Serilog;

namespace kFood.App_Start
{
    /// <summary>
    /// The log configuration
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// Configure logging
        /// </summary>
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(@"D:\Szymon\Programowanie\Logs\kFoodAPI.txt", 
                    rollingInterval: RollingInterval.Day, 
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Log.Logger.Information("*** START APPLICATION ***");
        }
    }
}