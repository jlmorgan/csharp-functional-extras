# `Maybe.Just<V>(V value)`

Creates a `Just` of the value.

## Arguments

* `value (V)`: A non-null value.

## Types

* `V`: The value type.

## Returns

* `(Maybe<V>)`: A `Maybe` of the `value`.

## Example

```java
Maybe.Just<string>("a");
// => Just(a)
```
