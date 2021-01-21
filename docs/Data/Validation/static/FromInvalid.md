# `Validation.FromInvalid<I, V>(IEnumerable<I> defaultValues, IValidation<I, V> validation)`

Extracts the value out of an `Invalid`; otherwise, returns the `defaultValues`.

## Alternatives

* `Validation.FromInvalid<I, V>(IEnumerable<I> defaultValues)(IValidation<I, V> validation)`

## Arguments

* `defaultValues (IEnumerable<I>)`: Values used if the `validation` is not an `Invalid`.
* `validation (IValidation<I, V>)`: The `Validation`.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(IEnumerable<I>)`: The underlying left value or default.

## Examples

```csharp
Validation.FromInvalid<int, string>(new List<int> { 0 }, Validation.Invalid<int, string>(1));
// => [1]

Validation.FromInvalid<int, string>(new List<int> { 0 }, "a");
// => [0]

Validation.FromInvalid<int, string>(new List<int> { 0 }, Validation.Valid<int, string>("a"));
// => [0]
```
