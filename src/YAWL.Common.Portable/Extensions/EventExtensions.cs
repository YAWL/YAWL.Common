using System;

namespace YAWL.Common.Extensions
{
    public static class EventExtensions
    {
        public static void TryInvoke(this EventHandler eventHandler, object sender)
        {
            if (eventHandler != null)
                eventHandler.Invoke(sender, EventArgs.Empty);
        }

        public static void TryInvoke<T>(this EventHandler<T> eventHandler, object sender, T arg)
        {
            if (eventHandler != null)
                eventHandler.Invoke(sender, arg);
        }

        public static void TryInvoke<T>(this EventHandler<EventArgs<T>> eventHandler, object sender, T arg)
        {
            if (eventHandler != null)
                eventHandler.Invoke(sender, EventArgs<T>.Create(arg));
        }

        public static void TryInvoke<T>(this EventHandler<EventArgs<T>> eventHandler, object sender, EventArgs<T> arg)
        {
            if (eventHandler != null)
                eventHandler.Invoke(sender, arg);
        }
    }
}
