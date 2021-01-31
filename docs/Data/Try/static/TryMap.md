# `Try.TryMap<R, C>(Func<Exception, C> failureMorphism, Func<V, C> successMorphism, ITry<V> tryable)`

Provides a catamorphism of the `tryable` to a singular value. If the value is `Failure e`, apply the first function to `a`; otherwise, apply the second function to `b`.

## Alternatives

* `Try.TryMap<V, C>(Func<Exception, C> failureMorphism, Func<V, C> successMorphism)(ITry<V> tryable)`
* `Try.TryMap<V, C>(Func<Exception, C> failureMorphism)(Func<V, C> successMorphism)(ITry<V> tryable)`

## Arguments

* `failureMorphism (Func<Exception, C>)`: Maps the value of a `Failure e` to `c`.
* `successMorphism (Func<V, C>)`: Maps the value of a `Success b` to `c`.
* `tryable (Try<V>)`: The `Try`.

## Types

* `V`: The underlying success type.
* `C`: The return type.

## Returns

* `(C)`: The result of the catamorphism of the `tryable`.

## Throws

* `ArgumentNullException` if the `failureMorphism`, `successMorphism`, or `tryable` is `null`.

## Examples

```csharp
Try.TryMap(
  error => error.Message,
  value => value % 2 == 0 ? "The value is even" : "The value is odd",
  Try.Successes<int>(1)
);
// => "The value is odd"

Try.TryMap(
  error => error.Message,
  value => value % 2 == 0 ? "The value is even" : "The value is odd",
  Try.Failure<int>(new ArgumentException("The value is not a number"))
);
// => "The value is not a number"
```
