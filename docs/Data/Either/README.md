# `Either`

The `Either` type is a right-biased disjunction that represents two possibilities: either a `Left` of `a` or a `Right` of `b`. By convention, the `Either` is used to represent a value or an error result of some function or process as a `Left` of the error or a `Right` of the value.

* [Interface](./IEither.md)
* [Constructors](./constructors)
    * [`.Left`](./constructors/Left.md)
    * [`.Right`](./constructors/Right.md)
* [Static Methods](./static)
    * [`.EitherMap`](./static/EitherMap.md)
    * [`.FromLeft`](./static/FromLeft.md)
    * [`.FromRight`](./static/FromRight.md)
    * [`.Lefts`](./static/Lefts.md)
    * [`.PartitionEithers`](./static/PartitionEithers.md)
    * [`.Rights`](./static/Rights.md)
* [Instance Methods](./instance)
    * [`#Equals`](./instance/Equals.md)
    * [`#IsLeft`](./instance/IsLeft.md)
    * [`#IsRight`](./instance/IsRight.md)
    * [`#ToString`](./instance/ToString.md)
