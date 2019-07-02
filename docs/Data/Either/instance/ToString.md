# `Either#ToString<A, B>()`

Converts the `instance` to a `String` representation.

## Types

* `A`: The underlying left type.
* `B`: The underlying right type.

## Returns

* `(String)`: The `instance` as a `String`.

## Examples

```csharp
Either.Left<string, string>("a").ToString();
// => "Left(a)"

Either.Right<string, string>("a").ToString();
// => "Right(a)"
```
