# `Try#Equals<A>(object other)`

Determines whether or not the `other` has the same value as the current `instance`.

## Arguments

* `other (object)`: The other object.

## Types

* `A`: The underlying success type.

## Returns

* `(bool)`: `true` for equality; otherwise, `false`.

## Examples

```csharp
Exception error = new Exception();
ITry<string> failure = Try.Failure<string>(error);
ITry<string> success = Try.Success("a");

success.Equals("a");
// => false

success.Equals(failure);
// => false

success.Equals(Try.Success("a"));
// => true

failure.Equals(Try.Failure<string>(error));
// => true
```
