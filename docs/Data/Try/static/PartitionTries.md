# `Try.PartitionTries<A>(IEnumerable<ITry<V>> enumerable)`

Partitions an enumerable of `Try` into two enumerables. All `Failure` elements are extracted, in order, to the first position of the output. Similarly for the `Successes` elements in the second position.

## Arguments

* `enumerable (IEnumerable<ITry<V>>)` - The enumerable of `Try`.

## Types

* `V`: The underlying success type.

## Returns

* `(Tuple<IEnumerable<Exception>, IEnumerable<V>>)`: A couple of an enumerable of the underlying `Failure` values and an enumerable of the underlying `Success` values.

## Examples

```csharp
Try.PartitionTries(new List<ITry<int>>
{
  Try.Failure<int>(new ArgumentException("Unkown value")),
  Try.Failure<int>(new ArgumentException("Unkown value")),
  Try.Success(0),
  Try.Success(1)
});
// => Tuple([ArgumentException("Unkown value"), ArgumentException("Unkown value")], [0, 1])
```
