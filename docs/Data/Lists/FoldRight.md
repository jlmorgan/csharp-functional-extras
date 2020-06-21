# `FoldRight<A, B>(fold, initialValue, list)`

Folds a list from last to head.

## Alternatives

* `FoldRight<A, B>(fold)(initialValue)(list)`
* `FoldRight<A, B>(fold, initialValue)(list)`

## Arguments

* `fold (Func<A, B, B>)`: Folding function.
* `initialValue (B)`: Initial value.
* `list (List<A>)`: The list.

## Returns

* `(B)`: The result of the fold.

## Throws

* `ArgumentException` if the `fold` is `null`.

## Example

```csharp
List<string> letters = new List<string> { "a", "b", "c" };
Func<string, string, string> fold = (accumulator, value) => accumulator + value;
string initialValue = "";

FoldRight(fold, initialValue, letters);
// => 'cba'
```
