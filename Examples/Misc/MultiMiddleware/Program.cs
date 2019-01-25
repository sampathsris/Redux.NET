using CounterExampleCore;
using Redux;
using System;

namespace MultiMiddleware
{
    class Program
    {
        static void Main(string[] args)
        {
            var thunkMiddleware = StandardMiddleware.CreateThunkMiddleware();
            var loggerMiddleware = StandardMiddleware.CreateStdoutLoggerMiddleware();
            var thunkFirstEnhancer = Ops.ApplyMiddleware(thunkMiddleware, loggerMiddleware);
            var loggerFirstEnhancer = Ops.ApplyMiddleware(loggerMiddleware, thunkMiddleware);

            CreateTestWithEnhancer("Thunk first", thunkFirstEnhancer);
            CreateTestWithEnhancer("Logger first", loggerFirstEnhancer);

            // You will notice that when thunk middleware is the first parameter of ApplyMiddleware,
            // the messages inside `incrementIfOdd` prints int between the messages in the logger.
            // In other words, logger middleware calls thunk middleware. This happens because the
            // middleware is composed right to left, so the last middleware in the list gets called
            // first, and it in turn calls the other middleware.
        }

        private static void CreateTestWithEnhancer(string testName, StoreEnhancer enhancer)
        {
            //PrimitiveStore<int> counterStore = null;
            //counterStore = Ops.CreateStore<int>(Counter.Reduce, 101, enhancer);
            //counterStore.Subscribe<int>((IStore store, int state) =>
            //{
            //    Console.WriteLine("[{0}] counter: {1}", testName, state);
            //});

            //ReduxThunk<int> incrementIfOdd = (dispatch, getState) =>
            //{
            //    var count = getState();
            //    Console.WriteLine("[{0}] incrementIfOdd called. counter: {1}", testName, count);

            //    if (count % 2 != 0)
            //    {
            //        dispatch(Counter.Increment());
            //    }
            //};
            //var incrementIfOddAction = new ThunkAction<int>("INCREMENT_IF_ODD", incrementIfOdd);

            //Console.WriteLine("[{0}] Initial state: {1}", testName, counterStore.State); // 0

            //counterStore.Dispatch(incrementIfOddAction); // 102
            //counterStore.Dispatch(incrementIfOddAction); // 102, unchanged.

            //Console.ReadKey();
        }
    }
}
