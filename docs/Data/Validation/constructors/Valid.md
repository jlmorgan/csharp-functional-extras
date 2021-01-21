# `Validation.Valid<I, V>(S value)`

Encapsulates a valid value.

## Arguments

* `value (V)`: A value.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(IValidation<I, V>)`: An `Validation` of the `value`.

## Example

```csharp
Validation.Valid<int, string>("a");
// => Valid("a")
```
