//using Microsoft.Extensions.Logging;
using NLog;
using System;

namespace AM.Service
{
    public class LoggerService : ILoggerService
    {
        // another way to use nLOG as separate service
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerService()
        {
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogDebug<T>(T ex)
        {
            logger.Debug(ex);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
