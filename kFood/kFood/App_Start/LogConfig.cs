using Serilog;

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
                .WriteTo.File(@"D:\Szymon\Programowanie\Logs\testing.txt", 
                    rollingInterval: RollingInterval.Day, 
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}