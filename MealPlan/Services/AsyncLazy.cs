using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.Services
{
    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> X)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(X));
        }

        public AsyncLazy(Func<Task<T>> X)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(X));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }

        public void Start()
        {
            _ = instance.Value;
        }
    }
}
