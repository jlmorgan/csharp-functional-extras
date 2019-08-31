# `Validation.ValidationMap<F, S, C>(Func<F, C> failureMap, Func<S, C> successMap, IValidation<F, S> validation)`

Provides a catamorphism of the `validation` to a singular value. If the value is `Failure a`, apply the first function to `a`; otherwise, apply the second function to `b`.

## Alternatives

* `Validation.ValidationMap<F, S, C>(Func<F, C> failureMap, Func<S, C> successMap)(IValidation<F, S> validation)`
* `Validation.ValidationMap<F, S, C>(Func<F, C> failureMap)(Func<S, C> successMap)(IValidation<F, S> validation)`

## Arguments

* `failureMap (Function<F, C>)`: Maps the value of a `Failure a` to `c`.
* `successMap (Function<S, C>)`: Maps the value of a `Success b` to `c`.
* `validation (IValidation<F, S>)`: The `Validation`.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(C)`: The result of the catamorphism of the `validation`.

## Throws

* `ArgumentNullException` if the `failureMap`, `successMap`, or `validation` is `null`.

## Examples

```csharp
Validation.ValidationMap<Exception, int, string>(
  exceptions => exceptions
    .Select(exception => exception.Message)
    .Aggregate((left, right) => $"{left}, {right}"),
  value => "The value is " + value % 2 == 0 ? "even" : "odd",
  Validation.Success<Exception, int>(1)
);
// => "The value is odd"

Validation.ValidationMap<Exception, int, string>(
  exceptions => exceptions
    .Select(exception => exception.Message)
    .Aggregate((left, right) => $"{left}, {right}"),
  value => "The value is " + value % 2 == 0 ? "even" : "odd",
  Validation.Failure<Exception, int>(new ArgumentException("The value is not a number"))
);
// => "The value is not a number"
```
