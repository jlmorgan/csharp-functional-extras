# `Validation.Concat<I, V>(IValidation<I, V> second, IValidation<I, V> first)`

Concatenates two `Invalid` values together, replacing a `Valid` with the `Invalid`; otherwise, take the first `Valid`.

## Alternatives

* `Validation.Concat<I, V>(IValidation<I, V> second)(IValidation<I, V> first)`

## Arguments

* `second (IValidation<I, V>)`: The second `Validation`.
* `first (IValidation<I, V>)`: The first `Validation`.

## Types

* `I`: The underlying invalid type.
* `V`: The underlying valid type.

## Returns

* `(IValidation<I, V>)`: The first `Valid` for two valids, the first `Invalid` for mixed; otherwise, an `Invalid` of the concatenation of the invalid values.

## Throws

* `ArgumentNullException` if either value is `null`.

## Examples

```csharp
Validation.Concat(
  Validation.Valid<string, int>(0),
  Validation.Valid<string, int>(1)
);
// => Valid(0)

Validation.Concat(
  Validation.Valid<string, int>(0),
  Validation.Invalid<string, int>("a")
);
// => Invalid(["a"])

Validation.Concat(
  Validation.Invalid<string, int>("a"),
  Validation.Valid<string, int>(0)
);
// => Invalid(["a"])

Validation.Concat(
  Validation.Invalid<string, int>("b"),
  Validation.Invalid<string, int>("a")
);
// => Invalid(["a", "b"])
```
