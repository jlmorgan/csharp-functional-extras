# `Validation.Valids<I, V>(IEnumerable<IValidation<I, V>> enumerable)`

Extracts from a enumerable of `Validation` all of the `Valid` elements in extracted order.

## Arguments

* `enumerable (IEnumerable<IValidation<I, V>>)` - The enumerable of `Validation`.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(IEnumerable<V>)`: The enumerable of underlying `Valid` values.

## Examples

```csharp
Validation.Valids<string, int>([
  Validation.Invalid<string, int>("a"),
  Validation.Invalid<string, int>("b"),
  Validation.Valid<string, int>(0),
  Validation.Valid<string, int>(1)
]);
// => [0, 1]
```
