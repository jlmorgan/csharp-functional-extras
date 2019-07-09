# `IMaybe<A>`

## Types

* `A`: The underlying type.

## Constructors

* [`IMaybe<V> Just(V value)`][Just]
* [`IMaybe<V> Nothing()`][Nothing]

## Static Methods

* [`List<V> CatMaybes(List<IMaybe<V>> list)`][CatMaybes]
* [`V FromJust(IMaybe<V> maybe)`][FromJust]
* [`Func<IMaybe<V>, V> FromMaybe(V defaultValue)`][FromMaybe]
* [`V FromMaybe(V defaultValue, IMaybe<V> maybe)`][FromMaybe]
* [`bool IsJust(IMaybe<V> maybe)`][IsJust]
* [`bool IsNothing(IMaybe<V> maybe)`][IsNothing]
* [`IMaybe<V> ListToMaybe(List<V> list)`][ListToMaybe]
* [`Func<List<V>, List<R>> MapMaybe(Func<V, IMaybe<R>> morphism)`][MapMaybe]
* [`List<R> MapMaybe(Func<V, IMaybe<R>> morphism, List<V> list)`][MapMaybe]
* [`Func<Func<V, R>, Func<IMaybe<V>, R>> MaybeMap(R defaultValue)`][MaybeMap]
* [`Func<IMaybe<V>, R> MaybeMap(R defaultValue, Func<V, R> morphism)`][MaybeMap]
* [`R MaybeMap(R defaultValue, Func<V, R> morphism, IMaybe<V> maybe)`][MaybeMap]
* [`List<V> MaybeToList(IMaybe<V> maybe)`][MaybeToList]

## Instance Methods

* `bool IsJust()`: Determines whether or not the `Maybe` is a `Just`.
* `bool IsNothing()`: Determines whether or not the `Maybe` is a `Nothing`.

[CatMaybes]: ./static/CatMaybes.md
[FromJust]: ./static/FromJust.md
[FromMaybe]: ./static/FromMaybe.md
[IsJust]: ./static/IsJust.md
[IsNothing]: ./static/IsNothing.md
[Just]: ./constructors/Just.md
[ListToMaybe]: ./static/ListToMaybe.md
[MapMaybe]: ./static/MapMaybe.md
[MaybeMap]: ./static/MaybeMap.md
[MaybeToList]: ./static/MaybeToList.md
[Nothing]: ./constructors/Nothing.md
