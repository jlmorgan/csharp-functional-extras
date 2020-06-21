# `IsEmpty(list)`

Determines whether or not the `list` is `null` or empty.

## Arguments

* `list (List<T>)`: The list.

## Returns

* `(bool)`: `true` if the `list` is empty; otherwise, `false`.

## Example

```csharp
IsEmpty(new List<object>());
// => true

IsEmpty(new List<object> { null });
// => false

IsEmpty(new List<int> { 0 });
// => false
```
