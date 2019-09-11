# `Maybe<A>#Filter(Predicate<A> predicate)`

Tests the underlying value against the `predicate`, returning the `Just` of the value for `true`; otherwise, `Nothing`.

## Alternatives

* `Maybe.Filter<A>(Predicate<A> predicate)(IMaybe<A> maybe)`
* `Maybe.Filter<A>(Predicate<A> predicate, IMaybe<A> maybe)`

## Types

* `A`: The underlying type.

## Arguments

* `predicate (Predicate<A>)`: The predicate with which to test the value.

## Returns

* `(IMaybe<A>)`: The `Just` of the value for `true`; otherwise, `Nothing`.

## Throws

* `ArgumentNullException` if the `predicate` is `null`.

## Examples

```csharp
Predicate<int> isEven = value => value % 2 == 0;
Maybe.Just(0)
  .Filter(isEven);
// => Just(0)

Maybe.Just(1)
  .Filter(isEven);
// => Nothing()

Maybe.Nothing()
  .Filter(isEven);
// => Nothing()

Maybes.Filter(isEven)(Maybe.Just(0));
// => Just(0)

Maybes.Filter(isEven, Maybe.Just(1));
// => Nothing()

Maybes.Filter(isEven, Maybe.Nothing<int>());
// => Nothing()
```
