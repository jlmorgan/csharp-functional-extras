# `Validation.Concat<F, S>(IValidation<F, S> second, IValidation<F, S> first)`

Concatenates two `Failure` values together, replacing a `Success` with the `Failure`; otherwise, take the first `Success`.

## Alternatives

* `Validation.Concat<F, S>(IValidation<F, S> second)(IValidation<F, S> first)`

## Arguments

* `second (IValidation<F, S>)`: The second `Validation`.
* `first (IValidation<F, S>)`: The first `Validation`.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(IValidation<F, S>)`: The first `Success` for two successes, the first `Failure` for mixed; otherwise, a `Failure` of the concatenation of the failure values.

## Throws

* `ArgumentNullException` if either value is `null`.

## Examples

```csharp
Validation.concat(
  Validation.Success<string, int>(0),
  Validation.Success<string, int>(1)
);
// => Success(0)

Validation.concat(
  Validation.Success<string, int>(0),
  Validation.Failure<string, int>("a")
);
// => Failure(["a"])

Validation.concat(
  Validation.Failure<string, int>("a"),
  Validation.Success<string, int>(0)
);
// => Failure(["a"])

Validation.concat(
  Validation.Failure<string, int>("b"),
  Validation.Failure<string, int>("a")
);
// => Failure(["a", "b"])
```
