# `Maybe.MapMaybe<V, R>(Func<V, IMaybe<R>> morphism, List<V> list)`

A version of `map` which can throw out elements. If the result of the function is `Nothing`, no element is added to the result list; otherwise, the value is added.

## Alternatives

* `Maybe.MapMaybe<V, R>(Func<V, IMaybe<R>> morphism)(List<V> list)`

## Arguments

* `morphism (Func<V, IMaybe<R>>)`: The function that maps the value in the `list` to a `Maybe` of the result.
* `list (List<V>)`: The list of values.

## Types

* `R`: The underlying type of the result `list`.
* `V`: The underlying type of the `list`.

## Returns

* `(List<R>)`: A list of mapped `Just` values.

## Throws

* `ArgumentNullException` if the `morphism` is `null`.

## Examples

```csharp
Maybe.MapMaybe(
  value => value % 2 == 0 ? Maybe.Just(value) : Maybe.Nothing<int>(),
  new List<int> { 0, 1, 2, 3 }
);
// => [0, 2]

Maybe.MapMaybe(
  value => value % 2 == 0 ? Maybe.Just(value) : Maybe.Nothing<int>(),
  null
);
// => []

Maybe.MapMaybe(
  value => value % 2 == 0 ? Maybe.Just(value) : Maybe.Nothing<int>(),
  new List<int>()
);
// => []
```
