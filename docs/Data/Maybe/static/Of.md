# `Maybe.Of<V>(V value)`

Creates a `Maybe` of the `value` where:

* `null` &rarr; `Nothing`
* `a` &rarr; `Just(a)`

## Arguments

* `value (V)`: The value.

## Types

* `V`: The underlying type.

## Returns

* `(IMaybe<V>)`: `Nothing` if the `value` is `null`; otherwise, `Just` of the `value`.

## Examples

```csharp
Maybe.Of<object>();
// => Nothing()

Maybe.Of<object>(null);
// => Nothing()

Maybe.Of<bool>(false);
// => Just(false)
```
