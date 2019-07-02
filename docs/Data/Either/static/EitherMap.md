# `Either.EitherMap<L, R, C>(Func<L, C> leftMorphism, Func<R, C> rightMorphism, IEither<L, R> either)`

Provides a catamorphism of the `either` to a singular value. If the value is `Left a`, apply the first function to `a`; otherwise, apply the second function to `b`.

## Alternatives

* `Either.EitherMap<L, R, C>(Func<L, C> leftMorphism, Func<R, C> rightMorphism)(IEither<L, R> either)`
* `Either.EitherMap<L, R, C>(Func<L, C> leftMorphism)(Func<R, C> rightMorphism)(IEither<L, R> either)`

## Arguments

* `leftMorphism (Func<L, C>)`: Maps the value of a `Left a` to `c`.
* `rightMorphism (Func<R, C>)`: Maps the value of a `Right b` to `c`.
* `either (Either<L, R>)`: The `Either`.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.
* `C`: The return type.

## Returns

* `(C)`: The result of the catamorphism of the `either`.

## Throws

* `ArgumentNullException` if the `leftMorphism`, `rightMorphism`, or `either` is `null`.

## Examples

```csharp
Either.EitherMap(
  error => error.Message,
  value => value % 2 == 0 ? "The value is even" : "The value is odd",
  Either.Right<ArgumentException, int>(1)
);
// => "The value is odd"

Either.EitherMap(
  error => error.Message,
  value => value % 2 == 0 ? "The value is even" : "The value is odd",
  Either.Left<ArgumentException, int>(new ArgumentException("The value is not a number"))
);
// => "The value is not a number"
```
