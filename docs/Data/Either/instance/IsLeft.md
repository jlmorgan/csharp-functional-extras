# `Either#IsLeft<A, B>()`

Determines whether or not the `Either` is a `Left`.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.

## Returns

* `(bool)`: `true` for a `Left`; otherwise, `false`.

## Examples

```csharp
Either.Left<string, string>("a").IsLeft();
// => true

Either.Right<string, string>("a").IsLeft();
// => false
```
