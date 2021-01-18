# `Validation#IsValid<A, B>()`

Determines whether or not the `Validation` is a `Valid`.

## Types

* `A`: The underlying invalid type.
* `B`: The underlying valid type.

## Returns

* `(bool)`: `true` for a `Valid`; otherwise, `false`.

## Examples

```csharp
Validation.Invalid<string, string>("a").IsValid();
// => false

Validation.Valid<string, string>("a").IsValid();
// => true
```
