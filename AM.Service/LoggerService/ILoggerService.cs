using System;

namespace AM.Service
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogDebug<T>(T ex);
        void LogError(string message);
    }
}
