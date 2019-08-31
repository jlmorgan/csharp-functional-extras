# `Validation#IsFailure<A, B>()`

Determines whether or not the `Validation` is a `Failure`.

## Types

* `A`: The underlying failure type.
* `B`: The underlying success type.

## Returns

* `(bool)`: `true` for a `Failure`; otherwise, `false`.

## Examples

```csharp
Validation.Failure<string, string>("a").IsFailure();
// => true

Validation.Success<string, string>("a").IsFailure();
// => false
```
