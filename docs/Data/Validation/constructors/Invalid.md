# `Validation.Invalid<I, V>(F value)`

Encapsulates an invalid value.

## Alternatives

* `Validation.Invalid<I, V>(IEnumerable<I> values)`

## Arguments

* `value (I)`: The value.

### Argument Overloads

* `values (IEnumerable<I>)`: The values.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(IValidation<I, V>)`: An `Validation` of the `value`.

## Example

```csharp
Validation.Invalid<int, string>(0);
// => Invalid([0])

Validation.Invalid<int, string>(new List<int> { 0, 1, 2 });
// => Invalid([0, 1, 2])
```
