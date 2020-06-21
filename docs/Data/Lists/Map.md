# `Map<A, B>(morphism, list)`

Maps the values of the `list` into a new `list`.

## Alternatives

* `Map<A, B>(morphism)(list)`

## Arguments

* `morphism (Func<A, B>)`: The morphism.
* `list (List<A>)`: The list.

## Types

* `A`: The underlying type.
* `B`: The mapped type.

## Returns

* `(List<A>)`: The mapped list.

## Example

```csharp
Func<int, int> increment = value => value + 1;

Map(increment, new List<int> { 1, 2, 3 });
// => List<int> { 2, 3, 4 }
```
