# `Try#ToString<A>()`

Converts the `instance` to a `String` representation.

## Types

* `A`: The underlying success type.

## Returns

* `(String)`: The `instance` as a `String`.

## Examples

```csharp
Try.Failure<string>(new Exception().ToString();
// => "Failure(Exception)"

Try.Success<string>("a").ToString();
// => "Success(a)"
```
