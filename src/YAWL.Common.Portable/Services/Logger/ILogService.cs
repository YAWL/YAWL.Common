using System;

namespace YAWL.Common.Services.Logger
{
    public interface ILogService
    {
        void AddLogger(ILogger logger);
        void RemoveLogger(ILogger logger);

        void Log(Exception ex);
    }
}
