# `Validation#ToString<A, B>()`

Converts the `instance` to a `String` representation.

## Types

* `A`: The underlying invalid type.
* `B`: The underlying valid type.

## Returns

* `(String)`: The `instance` as a `String`.

## Examples

```csharp
Validation.Invalid<string, string>("a").ToString();
// => "Invalid(a)"

Validation.Valid<string, string>("a").ToString();
// => "Valid(a)"
```
