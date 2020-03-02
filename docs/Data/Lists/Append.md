# `Append<A>(second, first)`

Appends two lists together.

## Alternatives

* `Append<A>(second)(first)`

## Arguments

* `second (List<A>)`: The list to append.
* `first (List<A>)`: The list on which to append.

## Returns

* `(List<A>)`: The appended list.

## Example

```csharp
List<int> second = new List { 2, 3 };
List<int> first = new List { 0, 1 };

Append(second, first);
// => List<int> { 0, 1, 2, 3 }
```
