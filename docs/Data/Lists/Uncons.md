# `Uncons<A>(list)`

Decomposes a list into its head and tail. Returns `Nothing<(A, List<A>)>` if the `list` is empty; otherwise a `Just<(A, List<A>)> (x, xs)` where `x` is the head of the `list` and `xs` is the tail.

## Arguments

* `list (List<A>)`: The list.

## Types

* `A`: The underlying type.

## Returns

* `(IMaybe<(A, List<A>)>)`: `Just` of the head and tail; otherwise, `Nothing`.

## Example

```csharp
Uncons(new List<int>());
// => Nothing<(int, List<int>)>

Uncons(new List<int> { 0 });
// => Just<(int, List<int>)> (0, List<int> { })

Uncons(new List<int> { 1, 2, 3 });
// => Just<(int, List<int>)> (0, List<int> { 2, 3 })
```
