# `Either#Equals<A, B>(object other)`

Determine whether or not the `other` has the same value as the current `instance`.

## Arguments

* `other (object)`: The other object.

## Types

* `A`: The underlying left type.
* `B`: The underlying right type.

## Returns

* `(bool)`: `true` for equality; otherwise, `false`.

## Examples

```csharp
const left = Either.Left<string, string>("a");
const right = Either.Right<string, string>("a");

right.Equals("a");
// => false

right.Equals(left);
// => false

right.Equals(Either.Right<string, string>("a"));
// => true

left.Equals(Either.Left<string, string>("a"));
// => true
```
