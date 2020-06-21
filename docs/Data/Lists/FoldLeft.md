# `FoldLeft<A, B>(fold, initialValue, list)`

Folds a list from head to last.

## Alternatives

* `FoldLeft<A, B>(fold)(initialValue)(list)`
* `FoldLeft<A, B>(fold, initialValue)(list)`

## Arguments

* `fold (Func<B, A, B>)`: Folding function.
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

FoldLeft(fold, initialValue, letters);
// => 'cba'
```
