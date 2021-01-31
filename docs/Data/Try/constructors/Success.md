# `Try.Success<V>(V value)`

Encapsulates a success value.

## Arguments

* `value (V)`: A value.

## Types

* `V`: The underlying success type.

## Returns

* `(ITry<V>)`: An `Try` of the `value`.

## Example

```csharp
Try.Success<string>("a");
// => Success("a")
```
