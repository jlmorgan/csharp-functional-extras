# `Either.PartitionEithers<L, R>(IEnumerable<IEither<L, R>> enumerable)`

Partitions an enumerable of `Either` into two enumerables. All `Left` elements are extracted, in order, to the first position of the output. Similarly for the `Right` elements in the second position.

## Arguments

* `enumerable (IEnumerable<IEither<L, R>>)` - The enumerable of `Either`.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.

## Returns

* `(Tuple<IEnumerable<L>, IEnumerable<R>>)`: A couple of an enumerable of the underlying `Left` values and an enumerable of the underlying `Right` values.

## Examples

```csharp
Either.PartitionEithers(new List<IEither<string, int>>
{
  Either.Left<string, int>("a"),
  Either.Left<string, int>("b"),
  Either.Right<string, int>(0),
  Either.Right<string, int>(1)
});
// => Tuple(["a", "b"], [0, 1])
```
