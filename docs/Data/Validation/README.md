# `Validation`

The `Validation` type is a right-biased disjunction that represents two possibilities: either an `Invalid` of `a` or a `Valid` of `b`. By convention, the `Validation` is used to represent a value or invalid result of some function or process as an `Invalid` of the invalid message or a `Valid` of the value.

* [Constructors](./constructors)
    * [`.Invalid`](./constructors/Invalid.md)
    * [`.Valid`](./constructors/Valid.md)
* [Static Methods](./static)
    * [`.Concat`](./static/Concat.md)
    * [`.FromInvalid`](./static/FromInvalid.md)
    * [`.FromValid`](./static/FromValid.md)
    * [`.Invalids`](./static/Invalids.md)
    * [`.IsInvalid`](./static/IsInvalid.md)
    * [`.IsValid`](./static/IsValid.md)
    * [`.PartitionValidations`](./static/PartitionValidations.md)
    * [`.Validate`](./static/Validate.md)
    * [`.ValidationMap`](./static/ValidationMap.md)
    * [`.Valids`](./static/Valids.md)
* [Instance Methods](./instance)
    * [`#Equals`](./instance/Equals.md)
    * [`#IsInvalid`](./instance/IsInvalid.md)
    * [`#IsValid`](./instance/IsValid.md)
    * [`#ToString`](./instance/ToString.md)
