# `Validation.FromFailure<F, S>(IEnumerable<F> defaultValues, IValidation<F, S> validation)`

Extracts the value out of a `Failure`; otherwise, returns the `defaultValues`.

## Alternatives

* `Validation.FromFailure<F, S>(IEnumerable<F> defaultValues)(IValidation<F, S> validation)`

## Arguments

* `defaultValues (IEnumerable<F>)`: Values used if the `validation` is not a `Failure`.
* `validation (IValidation<F, S>)`: The `Validation`.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(IEnumerable<F>)`: The underlying left value or default.

## Examples

```csharp
Validation.FromFailure<int, string>(new List<int> { 0 }, Validation.Failure<int, string>(1));
// => [1]

Validation.FromFailure<int, string>(new List<int> { 0 }, "a");
// => [0]

Validation.FromFailure<int, string>(new List<int> { 0 }, Validation.Success<int, string>("a"));
// => [0]
```
