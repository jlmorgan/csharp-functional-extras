# `Validation`

The `Validation` type is a right-biased disjunction that represents two possibilities: either a `Failure` of `a` or a `Success` of `b`. By convention, the `Validation` is used to represent a value or failure result of some function or process as a `Failure` of the failure message or a `Success` of the value.

* [Constructors](./constructors)
    * [`.Failure`](./constructors/Failure.md)
    * [`.Success`](./constructors/Success.md)
* [Static Methods](./static)
    * [`.Concat`](./static/Concat.md)
    * [`.Failures`](./static/Failures.md)
    * [`.FromFailure`](./static/FromFailure.md)
    * [`.FromSuccess`](./static/FromSuccess.md)
    * [`.IsFailure`](./static/IsFailure.md)
    * [`.IsSuccess`](./static/IsSuccess.md)
    * [`.PartitionValidations`](./static/PartitionValidations.md)
    * [`.Successes`](./static/Successes.md)
    * [`.Validate`](./static/Validate.md)
    * [`.ValidationMap`](./static/ValidationMap.md)
* [Instance Methods](./instance)
    * [`#Equals`](./instance/Equals.md)
    * [`#IsFailure`](./instance/IsFailure.md)
    * [`#IsSuccess`](./instance/IsSuccess.md)
    * [`#ToString`](./instance/ToString.md)
