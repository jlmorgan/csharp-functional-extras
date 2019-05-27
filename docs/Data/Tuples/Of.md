# `Of(A first)(B second)`

Curried implementation of `Tuple.Create{T1, T2}(T1, T2)`.

## Arguments

* `first (A)`: The first element.
* `second (B)`: The second element.

## Types

* `A`: The `first` type parameter.
* `B`: The `second` type parameter.

## Returns

* `(Tuple<B, A>)`: A `Tuple` of two values.

## Example

Requires `using Extensions;`.

```csharp
Tuples.Of<string, int>("a")(1);
// => Tuple<string, int>("a", 1)
```
