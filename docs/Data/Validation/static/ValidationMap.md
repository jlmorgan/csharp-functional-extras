# `Validation.ValidationMap<F, S, C>(Func<F, C> invalidMap, Func<S, C> validMap, IValidation<I, V> validation)`

Provides a catamorphism of the `validation` to a singular value. If the value is `Invalid a`, apply the first function to `a`; otherwise, apply the second function to `b`.

## Alternatives

* `Validation.ValidationMap<F, S, C>(Func<F, C> invalidMap, Func<S, C> validMap)(IValidation<I, V> validation)`
* `Validation.ValidationMap<F, S, C>(Func<F, C> invalidMap)(Func<S, C> validMap)(IValidation<I, V> validation)`

## Arguments

* `invalidMap (Function<F, C>)`: Maps the value of a `Invalid a` to `c`.
* `validMap (Function<S, C>)`: Maps the value of a `Valid b` to `c`.
* `validation (IValidation<I, V>)`: The `Validation`.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(C)`: The result of the catamorphism of the `validation`.

## Throws

* `ArgumentNullException` if the `invalidMap`, `validMap`, or `validation` is `null`.

## Examples

```csharp
Validation.ValidationMap<Exception, int, string>(
  exceptions => exceptions
    .Select(exception => exception.Message)
    .Aggregate((left, right) => $"{left}, {right}"),
  value => "The value is " + value % 2 == 0 ? "even" : "odd",
  Validation.Valid<Exception, int>(1)
);
// => "The value is odd"

Validation.ValidationMap<Exception, int, string>(
  exceptions => exceptions
    .Select(exception => exception.Message)
    .Aggregate((left, right) => $"{left}, {right}"),
  value => "The value is " + value % 2 == 0 ? "even" : "odd",
  Validation.Invalid<Exception, int>(new ArgumentException("The value is not a number"))
);
// => "The value is not a number"
```
