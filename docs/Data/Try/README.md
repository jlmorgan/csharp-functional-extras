# `Try`

The `Try` type is a success-biased disjunction that represents two possibilities: tryable a `Failure` of `a` or a `Success` of `b`. By convention, the `Try` is used to represent a value or an error result of some function or process as a `Failure` of the error or a `Success` of the value.

* [Interface](./ITry.md)
* [Constructors](./constructors)
    * [`.Failure`](./constructors/Failure.md)
    * [`.Success`](./constructors/Success.md)
* [Static Methods](./static)
    * [`.FromFailure`](./static/FromFailure.md)
    * [`.FromSuccess`](./static/FromSuccess.md)
    * [`.Failures`](./static/Failures.md)
    * [`.PartitionTries`](./static/PartitionTries.md)
    * [`.Successes`](./static/Successes.md)
    * [`.TryMap`](./static/TryMap.md)
* [Instance Methods](./instance)
    * [`#Equals`](./instance/Equals.md)
    * [`#IsFailure`](./instance/IsFailure.md)
    * [`#IsSuccess`](./instance/IsSuccess.md)
    * [`#ToString`](./instance/ToString.md)
