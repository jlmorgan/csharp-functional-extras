# `Id(a)`

The `I` combinator or identity morphism.

## Arguments

* `a (A)`: The input value.

### Types

* `A`: Type of the argument.

## Returns

* `(<A>)`: The input value.

## Example

```csharp
DateTime value = new DateTime(1970, 1, 1);

Id(value) == value;
// => true
```
