# `Maybe<A>`

The `Maybe` type is a disjunction that wraps an arbitrary value. A `Maybe` `a` either contains a value of type `a` (read: `Just a`) or empty (read: `Nothing`). `Maybe` provides a way to deal with error or exceptional behavior.

## Table of Contents

* [Interface](./IMaybe.md)
* [Constructors](./constructors)
    * [`.Just`](./constructors/Just.md)
    * [`.Nothing`](./constructors/Nothing.md)
* [Static Methods](./static)
    * [`.CatMaybes`](./static/CatMaybes.md)
    * [`.FromJust`](./static/FromJust.md)
    * [`.FromMaybe`](./static/FromMaybe.md)
    * [`.IsJust`](./static/IsJust.md)
    * [`.IsNothing`](./static/IsNothing.md)
    * [`.ListToMaybe`](./static/ListToMaybe.md)
    * [`.MapMaybe`](./static/MapMaybe.md)
    * [`.MaybeMap`](./static/MaybeMap.md)
    * [`.MaybeToList`](./static/MaybeToList.md)
    * [`.Of`](./static/Of.md)
* [Instance Methods](./instance)
    * [`#Equals`](./instance/Equals.md)
    * [`#Filter`](./instance/Filter.md)
    * [`#FMap`](./instance/FMap.md)
    * [`#HashCode`](./instance/HashCode.md)
    * [`#IsJust`](./instance/IsJust.md)
    * [`#IsNothing`](./instance/IsNothing.md)
    * [`#ToString`](./instance/ToString.md)
