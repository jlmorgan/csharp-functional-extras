# `Validation#Equals<A, B>(object other)`

Determines whether or not the `other` has the same value as the current `instance`.

## Arguments

* `other (object)`: The other object.

## Types

* `A`: The underlying invalid type.
* `B`: The underlying valid type.

## Returns

* `(boolean)`: `true` for equality; otherwise, `false`.

## Examples

```csharp
IValidation<string, string> invalid = Validation.Invalid<string, string>("a");
IValidation<string, string> valid = Validation.Valid<string, string>("a");

valid.Equals("a");
// => false

valid.Equals(invalid);
// => false

valid.Equals(Validation.Valid<string, string>("a"));
// => true

invalid.Equals(Validation.Invalid<string, string>("a"));
// => true
```
