using Redux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreEnhancers
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = CreateDummyStoreEnhancer<int>("foo");
            var bar = CreateDummyStoreEnhancer<int>("bar");
            var composedEnhancer = Ops.ComposeEnhancers<int>(foo, bar);

            var store = Ops.CreateStore<int>(new IdentityReducer<int>(), null, composedEnhancer);
            Console.ReadKey();
        }

        static StoreEnhancer<TState> CreateDummyStoreEnhancer<TState>(string name)
        {
            return (storeCreator) =>
            {
                Console.WriteLine("Enhancer called: {0}", name);
                return storeCreator;
            };
        }
    }
}
