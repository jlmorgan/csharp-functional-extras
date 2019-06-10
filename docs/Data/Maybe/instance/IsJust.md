# `Maybe<A>#IsJust()`

Determines whether or not the `Maybe` is a `Just`.

## Types

* `A`: The underlying type.

## Returns

* `(bool)`: `true` for a `Just`; otherwise, `false`.

## Examples

```csharp
Maybe.Just<string>("a").IsJust();
// => true

Maybe.Nothing<string>().IsJust();
// => false
```
