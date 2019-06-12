# `Maybe.FromJust<V>(IMaybe<V> maybe)`

Extracts the value out of a `Just` and throws an error if its argument is a `Nothing`.

## Arguments

* `maybe (Maybe<V>)`: The `Maybe`.

## Types

* `V`: The underlying type.

## Returns

* `(V)`: The underlying value.

## Throws

* `ArgumentException` if the `maybe` is `null` or `Nothing`.

## Examples

```csharp
Maybe.FromJust(Maybe.Just("a"));
// => "a"

Maybe.FromJust(Maybe.Nothing<string>());
// => throws ArgumentException
```
