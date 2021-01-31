# `Try.Successes<V>(IEnumerable<ITry<V>> enumerable)`

Extracts from an enumerable of `Try` all of the `Success` elements in extracted order.

## Arguments

* `enumerable (IEnumerable<ITry<V>>)` - The enumerable of `Try`.

## Types

* `V`: The underlying success type.

## Returns

* `(IEnumerable<V>)`: The enumerable of underlying `Success` values.

## Examples

```csharp
Try.Successes(new List<ITry<int>>
{
  Try.Failure<int>(new ArgumentException("Unkown value")),
  Try.Failure<int>(new ArgumentException("Unkown value")),
  Try.Success(0),
  Try.Success(1)
});
// => [0, 1]
```
