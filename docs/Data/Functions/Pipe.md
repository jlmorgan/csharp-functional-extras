# `Pipe(f, g) | Pipe(f)(g)`

Composes two functions `f` before `g`.

## Arguments

* `f (Func<A, B>)`: The first function.
* `g (Func<B, C>)`: The second function.

### Types

* `A`: Input type to the first function (`f`) in the composition.
* `B`: Output type of the first function (`f`) and input type to the second (`g`).
* `C`: Output type of the second function (`g`).

## Returns

* `(Func<A, C>)`: Returns a function that maps a value of type `A` to type `C`.

## Example

```csharp
Func<int, Func<int, int>> Add = a => b => a + 1;
Func<int, int> Square = a => a * a;
Func<int, int> DecrementAndSquare = Pipe(add(-1), square);
Func<int, int> IncrementAndSquare = Pipe(add(1))(square);

DecrementAndSquare(3);
// => 4

IncrementAndSquare(2);
// => 9
```
