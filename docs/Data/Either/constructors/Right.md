# `Either.Right<L, R>(R value)`

Encapsulates a right value.

## Arguments

* `value (R)`: A value.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.

## Returns

* `(IEither<L, R>)`: An `Either` of the `value`.

## Example

```csharp
Either.Right<int, string>("a");
// => Right("a")
```
