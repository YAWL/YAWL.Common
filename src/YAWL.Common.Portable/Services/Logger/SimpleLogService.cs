// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace YAWL.Common.Services.Logger
{
    public class SimpleLogService : ILogService
    {
        private readonly List<ILogger> _loggers = new List<ILogger>();

        public void AddLogger(ILogger logger)
        {
            if (logger != null)
                _loggers.Add(logger);
        }

        public void RemoveLogger(ILogger logger)
        {
            if (logger != null)
                _loggers.Remove(logger);
        }

        public void Log(Exception ex)
        {
            foreach(var logger in _loggers)
                logger.LogException(ex);
        }
    }
}