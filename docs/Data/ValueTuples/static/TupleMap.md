# `TupleMap<A, B, C>(Func<A, B> firstMorphism, Func<A, C> secondMorphism, A value)`

Maps the `value` into the elements of the [`Tuple`][Tuple] after applying a morphism for each position.

## Alternatives

* `TupleMap<A, B, C>(Func<A, B> firstMorphism)(Func<A, C> secondMorphism)(A value)`
* `TupleMap<A, B, C>(Func<A, B> firstMorphism, Func<A, C> secondMorphism)(A value)`

## Arguments

* `firstMorphism (Func<A, B>)`: The function to map the `value` into the first element.
* `secondMorphism (Func<A, C>)`: The function to map the `value` into the second element.
* `value`: The value.

## Types

* `A`: The value type.
* `B`: The underlying first type for the returned `Tuple`.
* `C`: The underlying second type for the returned `Tuple`.

## Returns

* `(B, C)`: The mapped tuple.

## Example

```csharp
ValueTuples.TupleMap<int, int, int>(Increment, Decrement, 10);
// => (11, 9)
```

[Tuple]: ..
