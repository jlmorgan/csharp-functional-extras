# `Length(list)`

Gets the length of the `list`; otherwise, defaults to `0`.

## Arguments

* `list (List<T>)`: The list.

## Returns

* `(int)`: The length of the list.

## Example

```csharp
Length(new List<int>());
// => 0

Length(new List<int> { 0 });
// => 1

Length(new List<int> { 1, 2, 3 });
// => 3
```
