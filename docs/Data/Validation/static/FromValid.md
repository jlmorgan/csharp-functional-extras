# `Validation.FromValid<I, V>(S defaultValue, IValidation<I, V> validation)`

Extracts the value out of a `Valid`; otherwise, returns the `defaultValue`.

## Alternatives

* `Validation.FromValid<I, V>(S defaultValue)(IValidation<I, V> validation)`

## Arguments

* `defaultValue (V)`: Value used if the `validation` is not a `Valid`.
* `validation (IValidation<I, V>)`: The `Validation`.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(V)`: The underlying right value or default.

## Examples

```csharp
Validation.FromValid<int, string>(0, Validation.Valid<int, string>(1));
// => 1

Validation.FromValid<int, string>(0, "a");
// => 0

Validation.FromValid<int, string>(0, Validation.Invalid<int, string>("a"));
// => 0
```
