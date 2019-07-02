# `Either.Rights<L, R>(IEnumerable<IEither<L, R>> enumerable)`

Extracts from an enumerable of `Either` all of the `Right` elements in extracted order.

## Arguments

* `enumerable (IEnumerable<IEither<L, R>>)` - The enumerable of `Either`.

## Types

* `L`: The underlying left type.
* `R`: The underlying right type.

## Returns

* `(IEnumerable<R>)`: The enumerable of underlying `Right` values.

## Examples

```csharp
Either.Rights(new List<IEither<string, int>>
{
  Either.Left<string, int>("a"),
  Either.Left<string, int>("b"),
  Either.Right<string, int>(0),
  Either.Right<string, int>(1)
});
// => [0, 1]
```
