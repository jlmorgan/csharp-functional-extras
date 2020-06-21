# `Init(list)`

Extracts the elements of a non-null, non-empty list excluding the last element.

## Arguments

* `list (List<T>)`: The list.

## Returns

* `(List<T>)`: Elements in the list excluding the last.

## Throws

* `ArgumentException` if the `list` is `null` or empty.

## Example

```csharp
Init(new List<int>());
// => ArgumentException("list must be a not empty List")

Init(new List<int> { 0 });
// => List<int> { 0 }

Init(new List<int> { 1, 2, 3 });
// => List<int> { 1, 2 }
```
