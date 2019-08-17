# `Validation#ToString<A, B>()`

Converts the `instance` to a `String` representation.

## Types

* `A`: The underlying failure type.
* `B`: The underlying success type.

## Returns

* `(String)`: The `instance` as a `String`.

## Examples

```csharp
Validation.Failure<string, string>("a").ToString();
// => "Failure(a)"

Validation.Success<string, string>("a").ToString();
// => "Success(a)"
```
