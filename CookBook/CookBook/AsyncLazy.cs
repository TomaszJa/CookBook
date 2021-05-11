using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CookBook
{
    //The AsyncLazy class combines the Lazy<T> and Task<T> types to
    //create a lazy-initialized task that represents the initialization of a resource.
    //The factory delegate that's passed to the constructor can either
    //be synchronous or asynchronous.Factory delegates will run on a thread pool thread,
    //and will not be executed more than once (even when multiple threads attempt to start them simultaneously).
    //When a factory delegate completes, the lazy-initialized value is available,
    //and any methods awaiting the AsyncLazy<T> instance receive the value.
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
