using CounterExampleCore;
using Redux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterExampleWithLogger
{
    class Program
    {
        private static IStore counterStore = null;

        static void Main(string[] args)
        {
            var loggerMiddleware = StandardMiddleware.CreateStdoutLoggerMiddleware<int>();
            var enhancer = Ops.ApplyMiddleware<int>(loggerMiddleware);
            var counterReducer = new Counter();
            counterStore = Ops.CreateStore<int>(counterReducer, enhancer: enhancer);

            Console.WriteLine("Initial state: " + counterStore.GetState<int>()); // 0

            counterStore.Dispatch(Counter.Increment()); // 1
            counterStore.Dispatch(Counter.Decrement()); // 0
            counterStore.Dispatch(Counter.Increment()); // 1
            counterStore.Dispatch(Counter.Increment()); // 2

            // Shouldn't output anything.
            counterStore.Dispatch(new ReduxAction("DUMMY_ACTION"));

            counterStore.Dispatch(Counter.ChangeBy(-3)); // -1
            counterStore.Dispatch(Counter.ChangeBy(5));  // 4

            Console.ReadKey();
        }
    }
}
