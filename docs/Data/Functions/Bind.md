# `Bind(g, f)`

Composes a sequence of two functions `g` after `f`.

## Alternatives

* `Bind(g)(f)`

## Arguments

* `g (Func<B, A, C>)`: The second function.
* `f (Func<A, B>)`: The first function.

## Types

* `A`: Input type for the first function (`f`) and the second argument of the second function (`g`).
* `B`: Output type of the first function (`f`) and the first argument of the second function (`g`).
* `C`: Output type of the second function (`g`).

## Returns

* `(Func<A, C>)`: A function that maps a value of type `A` to type `C`.

## Example

```csharp
Func<int, Func<int, int>> Add = (a, b) => a + b;
Func<int, int> Square = a => a * a;
Func<int, int> SquareAndAdd = Bind(Add, Square);

SquareAndAdd(3);
// => 12

SquareAndAdd(5);
// => 30
```
