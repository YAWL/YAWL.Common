using System;
using System.Diagnostics;

namespace YAWL.Common.Services.Logger
{
    public class DebugLogger : ILogger
    {
        public void LogException(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            Debug.WriteLine(ex.ToString());
        }
    }
}