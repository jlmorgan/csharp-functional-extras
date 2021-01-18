# `Validation.Validate<I, V>(Predicate<V> predicate, F invalidValue, S value)`

Validates a value `b` and a `Valid` of `b` if the `predicate` returns `true`; otherwise, an `Invalid` of `a`.

## Alternatives

* `Validation.Validate<I, V>(Predicate<V> predicate, F invalidValue)(S value)`
* `Validation.Validate<I, V>(Predicate<V> predicate)(F invalidValue)(S value)`

## Arguments

* `predicate (Predicate<V>)`: The predicate.
* `invalidValue (I)`: The invalid value.
* `value (V)`: The value to test.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(IValidation<I, V>)`: A `Valid` of the `value` if the `predicate` returns `true`; otherwise, an `Invalid` of `invalidValue`.

## Throws

* `ArgumentNullException` if the `predicate` is `null`.

## Examples

```csharp
Validation.Validate<string, int>(
  value => value % 2 == 0,
  "The value must be even",
  0
);
// => Valid(0)

Validation.Validate<string, int>(
  value => value % 2 == 0,
  "The value must be even",
  1
);
// => Invalid(["The value must be even"])
```
