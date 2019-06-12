# `Maybe.ListToMaybe<V>(List<V> list)`

Returns `Nothing` for an empty `list` or `Just` of the first element in the `list`.

## Arguments

* `list (List<V>)`: The `list` of values.

## Types

* `V`: The underlying type.

## Returns

* `(IMaybe<V>)`: A `Just` of the first non-null value; otherwise, `Nothing`.

## Examples

```csharp
Maybe.ListToMaybe(new List<string> { null, "", null, "a" });
// => Just("")

Maybe.ListToMaybe<string>(null);
// => Nothing()

Maybe.ListToMaybe(new List<string>());
// => Nothing()

Maybe.ListToMaybe(new List<string> { null });
// => Nothing()
```
