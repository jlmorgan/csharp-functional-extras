# `Either#IsRight<A, B>()`

Determines whether or not the `Either` is a `Right`.

## Types

* `A`: The underlying left type.
* `B`: The underlying right type.

## Returns

* `(bool)`: `true` for a `Right`; otherwise, `false`.

## Examples

```csharp
Either.Left<string, string>("a").IsRight();
// => false

Either.Right<string, string>("a").IsRight();
// => true
```
