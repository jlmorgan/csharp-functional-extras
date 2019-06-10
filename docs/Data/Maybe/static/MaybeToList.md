# `Maybe.MaybeToList<V>(IMaybe<V> maybe)`

Returns an empty list for `Nothing`; otherwise, a singleton list of the underlying value of the `Just`.

## Arguments

* `maybe (IMaybe<V>)`: The `Maybe`.

## Types

* `(V)`: The underlying type.

## Returns

* `(List<V>)`: A singleton list of the underlying value within the `maybe` for a `Just`; otherwise, an empty list for `Nothing`.

## Examples

```csharp
Maybe.MaybeToList(Maybe.Just(1));
// => [1]

Maybe.MaybeToList(Maybe.Nothing<int>());
// => []

Maybe.MaybeToList<int>(null);
// => []
```
