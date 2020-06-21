# `Last(list)`

Extracts the last element of a non-null, non-empty list.

## Arguments

* `list (List<T>)`: The list.

## Returns

* `(T)`: Last item in the list.

## Throws

* `ArgumentException` if the `list` is `null` or empty.

## Example

```csharp
last(new List<int>());
// => ArgumentException("list must be a non-empty List")

last(new List<int> { 0 });
// => 0

last(new List<int { 1, 2, 3 });
// => 3
```
