# `Tuple<A, B>#Swap()`

The second element of the `Tuple`.

## Types

* `A`: The `first` type parameter.
* `B`: The `second` type parameter.

## Returns

* `(Tuple<B, A>)`: The swapped `Tuple`.

## Example

Requires `using Extensions;`.

```csharp
Tuple.Create("a", 1).Swap();
// => Tuple<int, string>(1, "a")
```
