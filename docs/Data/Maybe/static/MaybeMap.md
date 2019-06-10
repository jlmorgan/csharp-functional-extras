# `Maybe.MaybeMap<V, R>(R defaultValue, Func<V, R> morphism, IMaybe<V> maybe)`

If the `Maybe` value is a `Nothing`, it returns the `defaultValue`; otherwise, applies the `morphism` to the underlying value in the `Just` and returns the result.

## Alternatives

* `Maybe.MaybeMap<V, R>(R defaultValue, Func<V, R> morphism)(IMaybe<V> maybe)`
* `Maybe.MaybeMap<V, R>(R defaultValue)(Func<V, R> morphism)(IMaybe<V> maybe)`

## Arguments

* `defaultValue (V)`: The value to use if the `maybe` is `null` or `Nothing`.
* `morphism (Func<V, R>)`: The function to map the underlying value of the `maybe` to the same type as the return type.
* `maybe (IMaybe<V>)`: The `Maybe`.

## Types

* `(R)`: The default value and return type.
* `(V)`: The underlying type of the `maybe`.

## Returns

* `(R)`: The mapped underlying value for a `Just`; otherwise, the `defaultValue`.

## Throws

* `ArgumentNullException` if the `morphism` is `null`.

## Examples

```csharp
Maybe.MaybeMap(0, value => value + 1, Maybe.Just(1));
// => 2

Maybe.MaybeMap<int, int>(0, value => value + 1, null);
// => 1

Maybe.MaybeMap(0, value => value + 1, Maybe.Nothing<int>());
// => 1
```
