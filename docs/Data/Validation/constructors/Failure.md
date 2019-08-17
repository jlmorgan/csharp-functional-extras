# `Validation.Failure<F, S>(F value)`

Encapsulates a failure value.

## Alternatives

* `Validation.Failure<F, S>(IEnumerable<F> values)`

## Arguments

* `value (F)`: The value.

### Argument Overloads

* `values (IEnumerable<F>)`: The values.

## Types

* `F`: The underlying failure type.
* `S`: The underlying success type.

## Returns

* `(IValidation<F, S>)`: An `Validation` of the `value`.

## Example

```csharp
Validation.Failure<int, string>(0);
// => Failure([0])

Validation.Failure<int, string>(new List<int> { 0, 1, 2 });
// => Failure([0, 1, 2])
```
