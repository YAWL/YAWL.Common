// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;
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
