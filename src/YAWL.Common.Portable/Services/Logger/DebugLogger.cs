// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

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