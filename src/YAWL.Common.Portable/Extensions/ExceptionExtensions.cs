﻿using System;
using GalaSoft.MvvmLight.Ioc;
using YAWL.Common.Services.Logger;

namespace YAWL.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Log(this Exception ex)
        {
            var logService = SimpleIoc.Default.TryGetInstance<ILogService>();
            logService?.Log(ex);
        }
    }
}
