# `Validation.Failures<F, S>(IEnumerable<IValidation<F, S>> enumerable)`

Extracts from a enumerable of `Validation` all of the `Failure` elements in extracted order.

## Arguments

* `enumerable (IEnumerable<IValidation<F, S>>)` - The enumerable of `Validation`.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(IEnumerable<F>)`: The enumerable of underlying `Failure` values.

## Examples

```csharp
Validation.failures([
  Validation.Failure<string, int>("a"),
  Validation.Failure<string, int>(new List<string> { "b", "c" }),
  Validation.Success<string, int>(0),
  Validation.Success<string, int>(1)
]);
// => ["a", "b", "c"]
```
