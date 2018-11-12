﻿using Redux;
using System;

namespace StoreEnhancers
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = CreateDummyStoreEnhancer<int>("foo");
            var bar = CreateDummyStoreEnhancer<int>("bar");
            var composedEnhancer = Ops.ComposeEnhancers<int>(foo, bar);

            var store = Ops.CreateStore<int>(Ops.GetIdentityReducer<int>(), null, composedEnhancer);
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
