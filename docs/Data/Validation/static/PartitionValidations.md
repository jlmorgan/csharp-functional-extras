# `Validation.PartitionValidations<I, V>(IEnumerable<IValidation<I, V>> enumerable)`

Partitions a enumerable of `Validation` into two enumerables. All `Invalid` elements are extracted, in order, to the first position of the output. Similarly for the `Valid` elements in the second position.

## Arguments

* `enumerable (IEnumerable<IValidation<I, V>>)` - The enumerable of `Validation`.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(Tuple<IEnumerable<I>, IEnumerable<V>>)`: A couple of a enumerable of the underlying `Invalid` values and a enumerable of the underlying `Valid` values.

## Examples

```csharp
Validation.PartitionValidations<string, int>([
  Validation.Invalid<string, int>("a"),
  Validation.Invalid<string, int>(new List<string> { "b", "c" }),
  Validation.Valid<string, int>(0),
  Validation.Valid<string, int>(1)
]);
// => Tuple(["a", "b", "c"], [0, 1])
```
