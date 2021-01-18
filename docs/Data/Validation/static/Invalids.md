# `Validation.Invalids<I, V>(IEnumerable<IValidation<I, V>> enumerable)`

Extracts from a enumerable of `Validation` all of the `Invalid` elements in extracted order.

## Arguments

* `enumerable (IEnumerable<IValidation<I, V>>)` - The enumerable of `Validation`.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(IEnumerable<I>)`: The enumerable of underlying `Invalid` values.

## Examples

```csharp
Validation.Invalids([
  Validation.Invalid<string, int>("a"),
  Validation.Invalid<string, int>(new List<string> { "b", "c" }),
  Validation.Valid<string, int>(0),
  Validation.Valid<string, int>(1)
]);
// => ["a", "b", "c"]
```
