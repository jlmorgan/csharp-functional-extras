# `Bind(g, f)`

Composes a sequence of two functions `g` after `f` where `f` maps the input value to the first argument of `g`.

## Alternatives

* `Bind(g)(f)`

## Arguments

* `g (Func<B, A, C>)`: The second function of the sequence.
* `f (Func<A, B>)`: The first function of the sequence.

## Types

* `A`: Input type for the first function (`f`) and the second argument of the second function (`g`).
* `B`: Output type of the first function (`f`) and the first argument of the second function (`g`).
* `C`: Output type of the second function (`g`).

## Returns

* `(Func<A, C>)`: A function that takes the value and returns the result of the sequence.

## Example

```csharp
Func<int, int, int> Subtract = (b, a) => b - a;
Func<int, int> Square = a => a ^ 2;
Func<int, int> SquareAndSubtract = Bind(Subtract, Square);

SquareAndSubtract(3); // (3 * 3) - 3
// => 6

SquareAndSubtract(5); // (5 * 5) - 5
// => 20
```
