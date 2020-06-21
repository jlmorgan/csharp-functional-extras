# `Of(A first, B second)`

Static constructor for `ValueTuple{T1, T2}(T1, T2)`.

## Alternatives

* `Of(A first)(B second)`

## Arguments

* `first (A)`: The first element.
* `second (B)`: The second element.

## Types

* `A`: The `first` type parameter.
* `B`: The `second` type parameter.

## Returns

* `(A, B)`: A `Tuple` of two values.

## Example

```csharp
ValueTuples.Of<string, int>("a", 1);
// => ("a", 1)

ValueTuples.Of<string, int>("a")(1);
// => ("a", 1)
```
