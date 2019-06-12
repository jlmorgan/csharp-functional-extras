# `Maybe<A>#Equals(object other)`

Determine whether or not the `other` has the same value as the current `instance`.

## Arguments

* `other (Object)`: The other object.

## Types

* `A`: The underlying type.

## Returns

* `(bool)`: `true` for equality; otherwise, `false`.

## Examples

```csharp
IMaybe<string> maybe = Maybe.Just("a");

maybe.Equals(null);
// => false

maybe.Equals("a");
// => false

maybe.Equals(new object());
// => false

maybe.Equals(Maybe.Nothing<string>());
// => false

maybe.Equals(Maybe.Just("a"));
// => true

Maybe.Nothing<string>().Equals(Maybe.Nothing<string>());
// => true
```
