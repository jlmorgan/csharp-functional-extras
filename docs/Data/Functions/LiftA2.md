# `LiftA2(h, g, f)`

Composes a sequence of two functions `g` and `f` and combines the results (`h`) where `f` maps the input value to the first argument of `h` and `g` maps the input value to the second argument of `h`.

## Alternatives

* `LiftA2(h)(g)(f)`
* `LiftA2(h, g)(f)`

## Arguments

* `h (Func<B, C, D>)`: The combining function of the sequence.
* `g (Func<A, C>)`: The second function of the sequence.
* `f (Func<A, B>)`: The first function of the sequence.

## Types

* `A`: Input type for the first (`f`) and second (`g`) functions.
* `B`: Output type of the first function (`f`) and the first argument of the combining function (`h`).
* `C`: Output type of the second function (`f`) and the second argument of the combining function (`h`).
* `D`: Output type of the combining function (`g`).

## Returns

* `(Func<A, D>)`: A function that takes the value and returns the result of the sequence.

## Example

```csharp
Func<int, int> Half = a => a / 2;
Func<int, int, int> Subtract = (a, b) => a - b;
Func<int, int> Square = a => a ^ 2;
Func<int, int> SubtractHalfFromSquare = LiftA2(Subtract, Half, Square);

SubtractHalfFromSquare(3); // (3 * 3) - (3 / 2)
// => 7.5

SubtractHalfFromSquare(5); // (5 * 5) - (5 / 2)
// => 22.5
```
