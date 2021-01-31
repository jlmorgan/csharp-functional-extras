# `Try#IsFailure<A>()`

Determines whether or not the `Try` is a `Failure`.

## Types

* `A`: The underlying success type.

## Returns

* `(bool)`: `true` for a `Failure`; otherwise, `false`.

## Examples

```csharp
Try.Failure<string>(new Exception()).IsFailure();
// => true

Try.Success<string>("a").IsFailure();
// => false
```
