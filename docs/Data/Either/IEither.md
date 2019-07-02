# `IEither<A, B>`

## Types

* `A`: The underlying left type.
* `B`: The underlying right type.

## Constructors

* [`IEither<L, R> left(L value)`][Left]
* [`IEither<L, R> right(R value)`][Right]

## Static Methods

* [`Func<Func<R, C>, Func<IEither<L, R>, C>> EitherMap(Func<L, C>)`][EitherMap]
* [`Func<IEither<L, R>, C> EitherMap(Func<L, C>, Func<R, C>)`][EitherMap]
* [`C EitherMap(Func<L, C>, Func<R, C>, IEither<L, R>)`][EitherMap]
* [`Func<IEither<L, R>, L> FromLeft(L defaultValue)`][FromLeft]
* [`L FromLeft(L defaultValue, IEither<L, R> either)`][FromLeft]
* [`Func<IEither<L, R>, R> FromRight(R defaultValue)`][FromRight]
* [`R FromRight(R defaultValue, IEither<L, R> either)`][FromRight]
* [`IEnumerable<L> Lefts(IEnumerable<IEither<L, R>> enumerable)`][Lefts]
* [`(IEnumerable<L>, IEnumerable<R>) PartitionIEithers(IEnumerable<IEither<L, R>> enumerable)`][PartitionIEithers]
* [`IEnumerable<R> Rights(IEnumerable<IEither<L, R>> enumerable)`][Rights]

## Instance Methods

* `boolean IsLeft()`: Determines whether or not the `IEither` is a `Left`.
* `boolean IsRight()`: Determines whether or not the `IEither` is a `Right`.

[EitherMap]: ./static/EitherMap.md
[FromLeft]: ./static/FromLeft.md
[FromRight]: ./static/FromRight.md
[Left]: ./constructors/Left.md
[Lefts]: ./static/Lefts.md
[PartitionIEithers]: ./static/PartitionIEithers.md
[Right]: ./constructors/Right.md
[Rights]: ./static/Rights.md
