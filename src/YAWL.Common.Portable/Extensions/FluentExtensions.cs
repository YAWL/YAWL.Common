// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;

namespace YAWL.Common.Extensions
{
    public static class FluentExtensions
    {
        public static void With<T>(this T value, Action<T> action)
            where T : class
        {
            if (value == null)
                return;

            action(value);
        }

        public static TR With<T, TR>(this T value, Func<T, TR> func)
            where T : class
        {
            if (value == null)
                return default(TR);

            return func(value);
        }
    }
}
