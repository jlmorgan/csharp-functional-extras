# `Validation#Equals<A, B>(object other)`

Determines whether or not the `other` has the same value as the current `instance`.

## Arguments

* `other (object)`: The other object.

## Types

* `A`: The underlying failure type.
* `B`: The underlying success type.

## Returns

* `(boolean)`: `true` for equality; otherwise, `false`.

## Examples

```csharp
IValidation<string, string> failure = Validation.Failure<string, string>("a");
IValidation<string, string> success = Validation.Success<string, string>("a");

success.Equals("a");
// => false

success.Equals(failure);
// => false

success.Equals(Validation.Success<string, string>("a"));
// => true

failure.Equals(Validation.Failure<string, string>("a"));
// => true
```
