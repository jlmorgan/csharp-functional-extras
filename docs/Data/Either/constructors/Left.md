# `Either.Left<L, R>(L value)`

Encapsulates a left value.

## Arguments

* `value (L)`: A value.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.

## Returns

* `(IEither<L, R>)`: An `Either` of the `value`.

## Example

```csharp
Either.Left<int, string>(0);
// => Left(0)
```
