
# Redux.NET

A predictable state container for Microsoft.NET applications, inspired by
[Redux](https://redux.js.org/), and the [other Redux.NET](https://github.com/GuillaumeSalles/redux.NET),
(specifically, the lack of `CombineReducers` in that project).

DISCLAIMER: Right now, this is just a project for fun, and not even remotely
ready for production.

## Core API

Redux.NET library exposes a number of delegates, interfaces, static methods, and extension methods.

### Delegates

Following are the public delegates declared in the `Redux` namespace.

#### `Redux.StateChangedEventHandler<TState>`

```
public delegate void StateChangedEventHandler<TState>(
    IStore store,
    TState state
);
```

An event handler that handles a StateChanged event of a store.

 - **parameters**:
     - **`store`**: The store that publishes the event.
     - **`state`**: State of the store that publishes the event.

#### `Redux.Reducer<TState>`

```
public delegate TState Reducer<TState>(
    TState state,
    ReduxAction action
);
```

Represents a Redux reducer. Takes an existing state and an action as the input, reducers them, and returns the new state.

 - **parameters**:
     - **`state`**: State before the reduction. An object/value of type `TState`.
     - **`action`**: Action used for the reduction. An instance of `Redux.ReduxAction`.
 - **returns**: State after the reduction. An object/value of type `TState`.

#### `Redux.StoreCreator<TState>`

```
public delegate IStore StoreCreator<TState>(
    Reducer<TState> reducer,
    Func<TState> getPreloadedState,
    StoreEnhancer<TState> enhancer
);
```
Represents a function that can create a Redux store.

 - **parameters**:
     - **`reducer`**: A reducer function. A delegate of type `Redux.Reducer<TState>`.
     - **`getPreloadedState`**: A function that can be called to retrieve the initial state to pre-load the store. This is of type `System.Func<TState>`.
     - **`enhancer`**: Enhancer function that enhances the store. A delegate of type `Redux.StoreEnhancer<TState>`.
 - **returns**: A redux store. An object that implements `Redux.IStore`.

#### `Redux.StoreEnhancer<TState>`

```
public delegate StoreCreator<TState> StoreEnhancer<TState>(
    StoreCreator<TState> storeCreator
);
```
Represents a function that accepts a StoreCreator, enhances it, and returns a new store creator.

 - **parameters**:
    - **`storeCreator`**: StoreCreator to be enhanced. A delegate of type `Redux.StoreCreator<TState>`.
 - **returns**: Enhanced StoreCreator. A delegate of type `Redux.StoreCreator<TState>`.

### Interfaces

#### `Redux.IStore`

```
public interface IStore
{
    void Dispatch(ReduxAction action);
}
```

Represents a Redux store.

### Methods

Following static methods all belong to static class `Redux.Ops`.

#### `Redux.Ops.CreateStore<TState>`

```
public static IStore CreateStore<TState>(
    Reducer<TState> reducer)
```
or,
```
public static IStore CreateStore<TState>(
    Reducer<TState> reducer,
    Func<TState> getPreloadedState)
```
or,
```
public static IStore CreateStore<TState>(
    Reducer<TState> reducer,
    Func<TState> getPreloadedState,
    StoreEnhancer<TState> enhancer)
```

Creates a Redux store that serves a given type `TState`.

 - **parameters**:
     - **`reducer`**: A reducer function. A delegate of type `Redux.Reducer<TState>`.
     - **`getPreloadedState`**: A function that can be called to retrieve the initial state to pre-load the store. This is of type `System.Func<TState>`.
     - **`enhancer`**: Enhancer function that enhances the store. A delegate of type `Redux.StoreEnhancer<TState>`.
 - **returns**: A redux store. An object that implements `Redux.IStore`.

#### `Redux.Ops.CombineReducers`

```
public static Reducer<IDictionary<string, dynamic>> CombineReducers(
    IDictionary<string, object> reducerMapping
)
```

Combines a list of reducers into a single reducer.

 - **parameters**:
     - **`reducerMapping`**: An array or parameter list of reducers. All reducers should be of type `Redux.Reducer<T>`. Reducers with `T` for both value and reference types can be used.
 - **returns**: A combined reducer. A delegate of type `Reducer<IDictionary<string, dynamic>>`.

### Extension Methods

There are number of extension methods targeted for `IStore`. By implementing these generic methods as extension methods, `IStore` can be made non-generic.

#### `(IStore).GetState<TState>`

```
public static TState GetState<TState>(this IStore store)
```

Usage:

```
// when store is an IStore, and it stores an int
var state = store.GetState<int>();
```

Gets the state of a store.

#### `(IStore).Subscribe<TState>`

```
public static Action Subscribe<TState>(this IStore store, StateChangedEventHandler<TState> handler)
```

Usage,

```
// when store is an IStore, and it stores an int
var unsubscribe = store.Subscribe<int>((IStore s, int nextState) =>
    {
        // do something with the state.
    });
// ... later
unsubscribe();
```

Subscribes an eventhandler to a store's StateChanged event.

 - **parameters**:
     - **`handler`**: A function that will be invoked by the event after subscription. This is a delegate of type `Redux.StateChangedEventHandler`.
 - **returns**: A function that can be called in order to unsubscribe from the store.

#### `(IStore).ReplaceReducer<TState>`
# Redux.NET

A predictable state container for Microsoft.NET applications, inspired by
[Redux](https://redux.js.org/), and the [other Redux.NET](https://github.com/GuillaumeSalles/redux.NET),
(specifically, the lack of `CombineReducers` in that project).

DISCLAIMER: Right now, this is just a project for fun, and not even remotely
ready for production.

## Core API

Redux.NET library exposes a number of delegates, interfaces, static methods, and extension methods.

### Delegates

Following are the public delegates declared in the `Redux` namespace.

#### `Redux.StateChangedEventHandler<TState>`

```c#
public delegate void StateChangedEventHandler<TState>(
    IStore store,
    TState state
);
```

An event handler that handles a StateChanged event of a store.

 - **parameters**:
     - **`store`**: The store that publishes the event.
     - **`state`**: State of the store that publishes the event.

#### `Redux.Reducer<TState>`

```c#
public delegate TState Reducer<TState>(
    TState state,
    ReduxAction action
);
```

Represents a Redux reducer. Takes an existing state and an action as the input, reducers them, and returns the new state.

 - **parameters**:
     - **`state`**: State before the reduction. An object/value of type `TState`.
     - **`action`**: Action used for the reduction. An instance of `Redux.ReduxAction`.
 - **returns**: State after the reduction. An object/value of type `TState`.

#### `Redux.StoreCreator<TState>`

```c#
public delegate IStore StoreCreator<TState>(
    Reducer<TState> reducer,
    Func<TState> getPreloadedState,
    StoreEnhancer<TState> enhancer
);
```
Represents a function that can create a Redux store.

 - **parameters**:
     - **`reducer`**: A reducer function. A delegate of type `Redux.Reducer<TState>`.
     - **`getPreloadedState`**: A function that can be called to retrieve the initial state to pre-load the store. This is of type `System.Func<TState>`.
     - **`enhancer`**: Enhancer function that enhances the store. A delegate of type `Redux.StoreEnhancer<TState>`.
 - **returns**: A redux store. An object that implements `Redux.IStore`.

#### `Redux.StoreEnhancer<TState>`

```c#
public delegate StoreCreator<TState> StoreEnhancer<TState>(
    StoreCreator<TState> storeCreator
);
```
Represents a function that accepts a StoreCreator, enhances it, and returns a new store creator.

 - **parameters**:
    - **`storeCreator`**: StoreCreator to be enhanced. A delegate of type `Redux.StoreCreator<TState>`.
 - **returns**: Enhanced StoreCreator. A delegate of type `Redux.StoreCreator<TState>`.

### Interfaces

#### `Redux.IStore`

```c#
public interface IStore
{
    void Dispatch(ReduxAction action);
}
```

Represents a Redux store.

### Methods

Following static methods all belong to static class `Redux.Ops`.

#### `Redux.Ops.CreateStore<TState>`

```c#
public static IStore CreateStore<TState>(
    Reducer<TState> reducer)
```
or,
```c#
public static IStore CreateStore<TState>(
    Reducer<TState> reducer,
    Func<TState> getPreloadedState)
```
or,
```c#
public static IStore CreateStore<TState>(
    Reducer<TState> reducer,
    Func<TState> getPreloadedState,
    StoreEnhancer<TState> enhancer)
```

Creates a Redux store that serves a given type `TState`.

 - **parameters**:
     - **`reducer`**: A reducer function. A delegate of type `Redux.Reducer<TState>`.
     - **`getPreloadedState`**: A function that can be called to retrieve the initial state to pre-load the store. This is of type `System.Func<TState>`.
     - **`enhancer`**: Enhancer function that enhances the store. A delegate of type `Redux.StoreEnhancer<TState>`.
 - **returns**: A redux store. An object that implements `Redux.IStore`.

#### `Redux.Ops.CombineReducers`

```c#
public static Reducer<IDictionary<string, dynamic>> CombineReducers(
    IDictionary<string, object> reducerMapping
)
```

Combines a list of reducers into a single reducer.

 - **parameters**:
     - **`reducerMapping`**: An array or parameter list of reducers. All reducers should be of type `Redux.Reducer<T>`. Reducers with `T` for both value and reference types can be used.
 - **returns**: A combined reducer. A delegate of type `Reducer<IDictionary<string, dynamic>>`.

### Extension Methods

There are number of extension methods targeted for `IStore`. By implementing these generic methods as extension methods, `IStore` can be made non-generic.

#### `(IStore).GetState<TState>`

```c#
public static TState GetState<TState>(this IStore store)
```

Usage:

```c#
// when store is an IStore, and it stores an int
var state = store.GetState<int>();
```

Gets the state of a store.

#### `(IStore).Subscribe<TState>`

```c#
public static Action Subscribe<TState>(this IStore store, StateChangedEventHandler<TState> handler)
```

Usage:

```c#
// when store is an IStore, and it stores an int
var unsubscribe = store.Subscribe<int>((IStore s, int nextState) =>
    {
        // do something with the state.
    });
// ... later
unsubscribe();
```

Subscribes an event handler to a store's state changes.

 - **parameters**:
     - **`handler`**: A function that will be invoked by the event after subscription. This is a delegate of type `Redux.StateChangedEventHandler`.
 - **returns**: A function that can be called in order to unsubscribe from the store.

#### `(IStore).ReplaceReducer<TState>`

```c#
public static void ReplaceReducer<TState>(this IStore store, Reducer<TState> nextReducer)
```

Usage:

```c#
// when store is an IStore, and it stores an int
store.ReplaceReducer<int>(newReducer);
```

Replaces the reducer that is currently being used by the store.

 - **parameters**:
     - **`nextReducer`**: New reducer to replace the old one. This is a deleagate of type `Redux.Reducer<TState>`.

## License
MIT

```
public static void ReplaceReducer<TState>(this IStore store, Reducer<TState> nextReducer)
```

Usage:

```
// when store is an IStore, and it stores an int
store.ReplaceReducer<int>(newReducer);
```

Replaces the reducer that is currently being used by the store.

 - **parameters**:
     - **`nextReducer`**: New reducer to replace the old one. This is a deleagate of type `Redux.Reducer<TState>`.

## License

MIT
