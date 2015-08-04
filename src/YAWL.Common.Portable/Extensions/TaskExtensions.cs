using System;
using System.Threading.Tasks;

namespace YAWL.Common.Extensions
{
    public struct MaybeTimeout
    {
        public bool HasTimedOut { get; private set; }

        public MaybeTimeout(bool hasTimedOut)
            : this()
        {
            HasTimedOut = hasTimedOut;
        }
    }

    public struct MaybeTimeout<T>
    {
        public bool HasTimedOut { get; private set; }
        public T Result { get; private set; }

        public MaybeTimeout(bool hasTimedOut, T result)
            : this()
        {
            HasTimedOut = hasTimedOut;
            Result = result;
        }
    }

    public static class TaskExtensions
    {
        public static async Task<MaybeTimeout> WithTimeout(this Task task, TimeSpan timeOutAfter)
        {
            var timeoutTask = Task.Delay(timeOutAfter);
            var result = await Task.WhenAny(task, timeoutTask);

            return new MaybeTimeout(result != task);
        }

        public static async Task<MaybeTimeout<T>> WithTimeout<T>(this Task<T> task, TimeSpan timeOutAfter)
        {
            var timeoutTask = Task.Delay(timeOutAfter);
            var result = await Task.WhenAny(task, timeoutTask);

            return new MaybeTimeout<T>(result != task, result == task ? task.Result : default(T));
        }
    }
}
