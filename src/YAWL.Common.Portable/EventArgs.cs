using System;

namespace YAWL.Common
{
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs(T value)
        {
            Value = value;
        }

        public static EventArgs<T> Create(T value)
        {
            return new EventArgs<T>(value);
        }
    }
}
