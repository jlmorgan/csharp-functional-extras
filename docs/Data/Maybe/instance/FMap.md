# `Maybe#FMap<A, B>(Func<A, B> morphism)`

Maps the underlying value of a `Maybe` in a `null`-safe way.

## Alternatives

* `Maybes.FMap<A, B>(Func<A, B> morphism)(IMaybe<A> maybe)`
* `Maybes.FMap<A, B>(Func<A, B> morphism, IMaybe<A> maybe)`

## Types

* `A`: The underlying type of the `Maybe`.
* `B`: The return type of the morphism.

## Arguments

* `morphism (Func<A, B>)`: The morphism.

## Returns

* `(IMaybe<B>)`: The mapped `Maybe`.

## Throws

* `ArgumentNullException` if the `morphism` is `null`.

## Examples

```csharp
Func<int, int> Square = value => value * value;
Maybe.Just(4)
  .FMap(Square);
// => Just(16)

Maybe.Nothing()
  .FMap(Square);
// => Nothing()

Maybe.Just<List<string>>(new List<string> { null })
  .FMap(list => list[0]);
// => Nothing()
```
