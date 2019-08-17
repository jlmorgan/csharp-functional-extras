# `Validation.Success<F, S>(S value)`

Encapsulates a success value.

## Arguments

* `value (S)`: A value.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(IValidation<F, S>)`: An `Validation` of the `value`.

## Example

```csharp
Validation.Success<int, string>("a");
// => Success("a")
```
