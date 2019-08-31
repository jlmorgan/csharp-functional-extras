# `Validation.Successes<F, S>(IEnumerable<IValidation<F, S>> enumerable)`

Extracts from a enumerable of `Validation` all of the `Success` elements in extracted order.

## Arguments

* `enumerable (IEnumerable<IValidation<F, S>>)` - The enumerable of `Validation`.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(IEnumerable<S>)`: The enumerable of underlying `Success` values.

## Examples

```csharp
Validation.Successes<string, int>([
  Validation.Failure<string, int>("a"),
  Validation.Failure<string, int>("b"),
  Validation.Success<string, int>(0),
  Validation.Success<string, int>(1)
]);
// => [0, 1]
```
