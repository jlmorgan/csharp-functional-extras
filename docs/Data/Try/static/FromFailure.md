# `Try.FromFailure<V>(Exception defaultValue, ITry<V> tryable)`

Extracts the value out of a `Failure`; otherwise, returns the `defaultValue`.

## Alternatives

* `Try.FromFailure<V>(Exception defaultValue)(ITry<V> tryable)`

## Arguments

* `defaultValue (Exception)`: Value used if the `tryable` is not a `Failure`.
* `tryable (ITry<V>)`: The `Try`.

## Types

* `V`: The underlying success type.

## Returns

* `(Exception)`: The underlying failure value.

## Examples

```csharp
Try.FromFailure(
  new ArgumentException("Unkown value"),
  Try.Failure<string>(new ArgumentException("The value is not a number"))
);
// => ArgumentException("The value is not a number")

Try.FromFailure<string>(new ArgumentException("Unknown value"))(Try.Success<string>("a"));
// => ArgumentException("Unkown value")
```
