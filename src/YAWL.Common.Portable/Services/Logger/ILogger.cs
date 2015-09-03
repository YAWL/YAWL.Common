// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;

namespace YAWL.Common.Services.Logger
{
    public interface ILogger
    {
        void LogException(Exception ex);
    }
}