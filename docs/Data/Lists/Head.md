# `Head(list)`

Extracts the first element of a non-null, non-empty list.

## Arguments

* `list (List<T>)`: The list.

## Returns

* `(T)`: First item in the list.

## Throws

* `ArgumentException` if the `list` is `null` or empty.

## Example

```csharp
Head(new List<int>());
// => ArgumentException("list must be a not empty List")

Head(new List<int> { 0 });
// => 0

Head(new List<int> { 1, 2, 3 });
// => 1
```
