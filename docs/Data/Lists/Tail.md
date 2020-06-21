# `Tail<A>(list)`

Extracts the elements of a non-null, non-empty list excluding the first element.

## Arguments

* `list (List<A>)`: The list.

## Types

* `A`: The underlying type.

## Returns

* `(List<A>)`: Elements in the list excluding the first.

## Throws

* `ArgumentException` if the `list` is `null` or empty.

## Example

```csharp
Tail(null);
// => ArgumentException("list must be a non-empty List")

Tail(new List<int> { 0 });
// => List<int> { }

Tail(new List<in> { 1, 2, 3 });
// => List<int> { 2, 3 }
```
