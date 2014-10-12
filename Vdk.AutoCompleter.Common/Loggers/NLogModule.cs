using System;
using NLog;

namespace Vdk.AutoCompleter.Common.Loggers
{
    public class NLogModule : LogModule<Logger>
    {
        protected override Logger CreateLoggerFor(Type type)
        {
            return LogManager.GetLogger(type.FullName);
        }
    }
}