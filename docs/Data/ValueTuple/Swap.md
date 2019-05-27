# `ValueTuple#Swap()`

The second element of the `ValueTuple`.

## Types

* `A`: The `first` type parameter.
* `B`: The `second` type parameter.

## Returns

* `(B, A)`: The swapped `ValueTuple`.

## Example

Requires `using Extensions;`.

```csharp
("a", 1).Swap();
// => (1, "a")
```
