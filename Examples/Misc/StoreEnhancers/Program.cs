using Redux;
using System;

namespace StoreEnhancers
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = CreateDummyStoreEnhancer("foo");
            var bar = CreateDummyStoreEnhancer("bar");
            var composedEnhancer = Ops.ComposeEnhancers<int>(foo, bar);

            var store = Ops.CreateStore(Ops.GetIdentityReducer(), null, composedEnhancer);
            Console.ReadKey();
        }

        static StoreEnhancer CreateDummyStoreEnhancer(string name)
        {
            return (storeCreator) =>
            {
                Console.WriteLine("Enhancer called: {0}", name);
                return storeCreator;
            };
        }
    }
}
