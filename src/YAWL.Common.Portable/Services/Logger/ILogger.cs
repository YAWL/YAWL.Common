using System;

namespace YAWL.Common.Services.Logger
{
    public interface ILogger
    {
        void LogException(Exception ex);
    }
}