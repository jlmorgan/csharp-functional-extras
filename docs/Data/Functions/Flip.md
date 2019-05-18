# `Flip(f)`

Flips the argument order of the specified bi-function or curried binary function.

## Arguments

### Curried Function

* `f (Func<A, Func<B, C>>)`: A curried binary function.

### Bi-Function

* `f (Func<A, B, C>)`: A bi-function.

### Types

* `A`: Type of the first argument.
* `B`: Type of the second argument.
* `C`: Return type of the function (`f`).

## Returns

### Flipped Curried Function

* `(Func<B, Func<A, C>>)`: The curried flipped binary function.

### Flipped Bi-Function

* `(Func<B, A, C>)`: The flipped bi-function.

## Example

```csharp
Func<string, string, string> Append = (a, b) => a + b;
Func<string, string, string> Prepend = Flip(Append);

Prepend("a", "b");
// => "ba"

Func<string, Func<string, string>> First = a => b => a;
Func<string, Func<string, string>> Second = Flip(First);

First("a")("b");
// => "a"

Second("a")("b");
// => "b"
```
