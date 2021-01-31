# `Try.FromSuccess<A>(A defaultValue, ITry<A> tryable)`

Extracts the value out of a `Success`; otherwise, returns the `defaultValue`.

## Alternatives

* `Try.FromSuccess<V>(R defaultValue)(Try<V> tryable)`

## Arguments

* `defaultValue (V)`: Value used if the `tryable` is not a `Success`.
* `tryable (ITry<V>)`: The `Try`.

## Types

* `V`: The underlying success type.

## Returns

* `(V)`: The underlying success value or default.

## Examples

```csharp
Try.FromSuccess(0, Try.Success<int>(1));
// => 1

Try.FromSuccess<int>(0)(Try.Failure<int>(new ArgumentException("The value is not a number")));
// => 0
```
