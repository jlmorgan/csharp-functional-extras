# `Validation.FromSuccess<F, S>(S defaultValue, IValidation<F, S> validation)`

Extracts the value out of a `Success`; otherwise, returns the `defaultValue`.

## Alternatives

* `Validation.FromSuccess<F, S>(S defaultValue)(IValidation<F, S> validation)`

## Arguments

* `defaultValue (S)`: Value used if the `validation` is not a `Success`.
* `validation (IValidation<F, S>)`: The `Validation`.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(S)`: The underlying right value or default.

## Examples

```csharp
Validation.FromSuccess<int, string>(0, Validation.Success<int, string>(1));
// => 1

Validation.FromSuccess<int, string>(0, "a");
// => 0

Validation.FromSuccess<int, string>(0, Validation.Failure<int, string>("a"));
// => 0
```
