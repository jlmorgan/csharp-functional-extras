# `Maybe.IsNothing<V>(IMaybe<V> maybe)`

Determines whether or not the `maybe` is a `Nothing`.

## Types

* `V`: The underlying type.

## Returns

* `(bool)`: `true` for a `Nothing`; otherwise, `false`.

## Examples

```csharp
Maybe.IsNothing(Maybe.Nothing<string>());
// => true

Maybe.IsNothing(Maybe.Just<string>("a"));
// => false
```
