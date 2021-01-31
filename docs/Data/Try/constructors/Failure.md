# `Try.Failure<V>(Exception value)`

Encapsulates a failure value.

## Arguments

* `value (Exception)`: An exception.

## Types

* `V`: The underlying success type.

## Returns

* `(ITry<V>)`: An `Try` of the `value`.

## Example

```csharp
Try.Failure<string>(new Exception());
// => Failure(Exception)
```
