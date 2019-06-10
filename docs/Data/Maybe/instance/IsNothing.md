# `Maybe<A>#IsNothing()`

Determines whether or not the `Maybe` is a `Nothing`.

## Types

* `A`: The underlying type.

## Returns

* `(bool)`: `true` for a `Nothing`; otherwise, `false`.

## Examples

```csharp
Maybe.Nothing<string>().IsNothing();
// => true

Maybe.Just<string>("a").IsNothing();
// => false
```
