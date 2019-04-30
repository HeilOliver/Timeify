using log4net;
using Timeify.Common.DI;
using Timeify.Common.Services;

namespace Timeify.Infrastructure.Logger
{
    [Injectable(typeof(ILogger), InjectableAttribute.LifeTimeType.Container)]
    public class Logger : ILogger
    {
        private readonly ILog logger;

        public void LogDebug(string message)
        {
            logger?.Debug(message);
        }

        public void LogError(string message)
        {
            logger?.Error(message);
        }

        public void LogInfo(string message)
        {
            logger?.Info(message);
        }

        public void LogWarn(string message)
        {
            logger?.Warn(message);
        }
    }
}