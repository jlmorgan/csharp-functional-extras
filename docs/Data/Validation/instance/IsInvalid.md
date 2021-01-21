# `Validation#IsInvalid<A, B>()`

Determines whether or not the `Validation` is an `Invalid`.

## Types

* `A`: The underlying invalid type.
* `B`: The underlying valid type.

## Returns

* `(bool)`: `true` for an `Invalid`; otherwise, `false`.

## Examples

```csharp
Validation.Invalid<string, string>("a").IsInvalid();
// => true

Validation.Valid<string, string>("a").IsInvalid();
// => false
```
