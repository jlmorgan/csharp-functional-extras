# `Const(a)`

The `K` combinator. Creates a unary function that ignores the input value and returns the original value.

## Arguments

* `a (A)`: A value.

## Types

* `A`: Type of the value to return.
* `B`: Type of the value to ignore.

## Returns

* `(Func<B, A>)`: A unary function that takes a value of type `B` and returns the original value of type `A`.

## Example

```csharp
Func<int, int> AlwaysOne = Const<int, int>(1);

AlwaysOne(0);
// => 1

AlwaysOne(2);
// => 1
```
