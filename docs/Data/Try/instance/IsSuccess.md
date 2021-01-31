# `Try#IsSuccess<A>()`

Determines whether or not the `Try` is a `Success`.

## Types

* `A`: The underlying success type.

## Returns

* `(bool)`: `true` for a `Success`; otherwise, `false`.

## Examples

```csharp
Try.Failure<string>(new Exception()).IsSuccess();
// => false

Try.Success<string>("a").IsSuccess();
// => true
```
