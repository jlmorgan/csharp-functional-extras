# `Validation#IsSuccess<A, B>()`

Determines whether or not the `Validation` is a `Success`.

## Types

* `A`: The underlying failure type.
* `B`: The underlying success type.

## Returns

* `(bool)`: `true` for a `Success`; otherwise, `false`.

## Examples

```csharp
Validation.Failure<string, string>("a").IsSuccess();
// => false

Validation.Success<string, string>("a").IsSuccess();
// => true
```
