# `Validation.PartitionValidations<F, S>(IEnumerable<IValidation<F, S>> enumerable)`

Partitions a enumerable of `Validation` into two enumerables. All `Failure` elements are extracted, in order, to the first position of the output. Similarly for the `Success` elements in the second position.

## Arguments

* `enumerable (IEnumerable<IValidation<F, S>>)` - The enumerable of `Validation`.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(Tuple<IEnumerable<F>, IEnumerable<S>>)`: A couple of a enumerable of the underlying `Failure` values and a enumerable of the underlying `Success` values.

## Examples

```csharp
Validation.PartitionValidations<string, int>([
  Validation.Failure<string, int>("a"),
  Validation.Failure<string, int>(new List<string> { "b", "c" }),
  Validation.Success<string, int>(0),
  Validation.Success<string, int>(1)
]);
// => Tuple(["a", "b", "c"], [0, 1])
```
