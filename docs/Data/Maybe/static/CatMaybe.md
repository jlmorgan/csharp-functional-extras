# `Maybe.CatMaybes<V>(List<IMaybe<V>> list)`

Takes a list of `Maybe` and returns a list of the `Just` values.

## Arguments

* `list (List<IMaybe<V>>)`: List of `Maybe`.

## Types

* `V`: The underlying type.

## Returns

* `(List<V>)`: A list of the `Just` values.

## Examples

```csharp
Maybe.CatMaybes(new List<string>
{
  Maybe.Just("a"),
  Maybe.Nothing<string>(),
  Maybe.Just("b"),
  Maybe.Nothing<string>()
});
// => ["a", "b"]
```
