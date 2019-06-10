# `Maybe.FromMaybe<V>(V defaultValue, IMaybe<V> maybe)`

Takes a `defaultValue` and a `Maybe` value. If the `Maybe` is `Nothing`, it returns the `defaultValue`; otherwise, it returns the underlying value of the `Just`.

## Alternatives

* `Maybe.FromMaybe<V>(V defaultValue)(IMaybe<V> maybe)`

## Arguments

* `defaultValue (V)`: The value to use if the `maybe` is `null` or `Nothing`.
* `maybe (IMaybe<V>)`: The `Maybe`.

## Types

* `V`: The underlying type.

## Returns

* `(V)`: The underlying value for a `Just`; otherwise, the `defaultValue`.

## Examples

```csharp
Maybe.FromMaybe("", Maybe.Nothing<string>());
// => ""

Maybe.FromMaybe("", Maybe.Just("a"));
// => "a"
```
