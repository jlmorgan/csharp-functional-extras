# `Either.FromRight<L, R>(R defaultValue, IEither<L, R> either)`

Extracts the value out of a `Right`; otherwise, returns the `defaultValue`.

## Alternatives

* `Either.FromRight<L, R>(R defaultValue)(Either<L, R> either)`

## Arguments

* `defaultValue (R)`: Value used if the `either` is not a `Right`.
* `either (IEither<L, R>)`: The `Either`.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.

## Returns

* `(R)`: The underlying right value or default.

## Examples

```csharp
Either.FromRight(0, Either.Right<string, int>(1));
// => 1

Either.FromRight<string, int>(0)(Either.Left<string, int>("a"));
// => 0
```
