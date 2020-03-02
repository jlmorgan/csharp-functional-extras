# `Validation.Validate<F, S>(Predicate<S> predicate, F failureValue, S value)`

Validates a value `b` and a `Success` of `b` if the `predicate` returns `true`; otherwise, a `Failure` of `a`.

## Alternatives

* `Validation.Validate<F, S>(Predicate<S> predicate, F failureValue)(S value)`
* `Validation.Validate<F, S>(Predicate<S> predicate)(F failureValue)(S value)`

## Arguments

* `predicate (Predicate<S>)`: The predicate.
* `failureValue (F)`: The failure value.
* `value (S)`: The value to test.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(IValidation<F, S>)`: A `Success` of the `value` if the `predicate` returns `true`; otherwise, a `Failure` of `failureValue`.

## Throws

* `ArgumentNullException` if the `predicate` is `null`.

## Examples

```csharp
Validation.validate<string, int>(
  value => value % 2 == 0,
  "The value must be even",
  0
);
// => Success(0)

Validation.validate<string, int>(
  value => value % 2 == 0,
  "The value must be even",
  1
);
// => Failure(["The value must be even"])
```
