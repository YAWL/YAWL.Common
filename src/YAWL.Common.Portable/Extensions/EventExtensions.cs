// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;

namespace YAWL.Common.Extensions
{
    public static class EventExtensions
    {
        public static void TryInvoke(this EventHandler eventHandler, object sender)
        {
            eventHandler?.Invoke(sender, EventArgs.Empty);
        }

        public static void TryInvoke<T>(this EventHandler<T> eventHandler, object sender, T arg)
        {
            eventHandler?.Invoke(sender, arg);
        }

        public static void TryInvoke<T>(this EventHandler<EventArgs<T>> eventHandler, object sender, T arg)
        {
            eventHandler?.Invoke(sender, EventArgs<T>.Create(arg));
        }

        public static void TryInvoke<T>(this EventHandler<EventArgs<T>> eventHandler, object sender, EventArgs<T> arg)
        {
            eventHandler?.Invoke(sender, arg);
        }
    }
}
