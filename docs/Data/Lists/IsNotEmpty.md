# `IsNotEmpty(list)`

Determines whether or not the `list` is not `null` or empty.

## Arguments

* `list (List<T>)`: The list.

## Returns

* `(bool)`: `true` if the `list` is not empty; otherwise, `false`.

## Example

```csharp
IsNotEmpty(new List<object>());
// => false

IsNotEmpty(new List<object> { null });
// => true

IsNotEmpty(new List<int> { 0 });
// => true
```
