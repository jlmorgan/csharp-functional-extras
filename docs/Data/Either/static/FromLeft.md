# `Either.FromLeft<L, R>(L defaultValue, IEither<L, R> either)`

Extracts the value out of a `Left`; otherwise, returns the `defaultValue`.

## Alternatives

* `Either.FromLeft<L, R>(L defaultValue)(IEither<L, R> either)`

## Arguments

* `defaultValue (L)`: Value used if the `either` is not a `Left`.
* `either (IEither<L, R>)`: The `Either`.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.

## Returns

* `(L)`: The underlying left value.

## Examples

```csharp
Either.FromLeft(0, Either.Left<int, string>(1));
// => 1

Either.FromLeft<int, string>(0)(Either.Right<int, string>("a"));
// => 0
```
