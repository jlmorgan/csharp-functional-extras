# `Maybe<A>#toString()`

Converts the `instance` to a `string` representation.

## Types

* `A`: The underlying type.

## Returns

* `(string)`: The `instance` as a `string`.

## Examples

```csharp
IMaybe<string> maybe = Maybe.Just("a");

maybe.ToString();
// => "Just<System.String>(a)"

Maybe.Nothing<string>().toString();
// => "Nothing<System.String>()"
```
