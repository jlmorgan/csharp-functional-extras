# `Maybe.IsJust<V>(IMaybe<V> maybe)`

Determines whether or not the `maybe` is a `Just`.

## Types

* `V`: The underlying type.

## Returns

* `(bool)`: `true` for a `Just`; otherwise, `false`.

## Examples

```csharp
Maybe.IsJust(Maybe.Just<string>("a"));
// => true

Maybe.IsJust(Maybe.Nothing<string>());
// => false
```
