using CounterExampleCore;
using Redux;
using System;

namespace MultiMiddleware
{
    class Program
    {
        static void Main(string[] args)
        {
            var thunkMiddleware = StandardMiddleware.CreateThunkMiddleware<int>();
            var loggerMiddleware = StandardMiddleware.CreateStdoutLoggerMiddleware<int>();
            var thunkFirstEnhancer = Ops.ApplyMiddleware<int>(thunkMiddleware, loggerMiddleware);
            var loggerFirstEnhancer = Ops.ApplyMiddleware<int>(loggerMiddleware, thunkMiddleware);

            CreateTestWithEnhancer("Thunk first", thunkFirstEnhancer);
            CreateTestWithEnhancer("Logger first", loggerFirstEnhancer);

            // You will notice that when thunk middleware is the first parameter of ApplyMiddleware,
            // the messages inside `incrementIfOdd` prints int between the messages in the logger.
            // In other words, logger middleware calls thunk middleware. This happens because the
            // middleware is composed right to left, so the last middleware in the list gets called
            // first, and it in turn calls the other middleware.
        }

        private static void CreateTestWithEnhancer(string testName, StoreEnhancer<int> enhancer)
        {
            IStore counterStore = null;
            var counterReducer = new Counter();
            counterStore = Ops.CreateStore<int>(counterReducer, () => 101, enhancer);
            counterStore.Subscribe<int>((IStore store, int state) =>
            {
                Console.WriteLine("[{0}] counter: {1}", testName, state);
            });

            ReduxThunk<int> incrementIfOdd = (dispatch, getState) =>
            {
                var count = getState();
                Console.WriteLine("[{0}] incrementIfOdd called. counter: {1}", testName, count);

                if (count % 2 != 0)
                {
                    dispatch(Counter.Increment());
                }
            };
            var incrementIfOddAction = new ThunkAction<int>("INCREMENT_IF_ODD", incrementIfOdd);

            Console.WriteLine("[{0}] Initial state: {1}", testName, counterStore.GetState<int>()); // 0

            counterStore.Dispatch(incrementIfOddAction); // 102
            counterStore.Dispatch(incrementIfOddAction); // 102, unchanged.

            Console.ReadKey();
        }
    }
}
