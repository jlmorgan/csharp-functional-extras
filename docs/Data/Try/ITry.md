# `ITry<A>`

## Types

* `A`: The underlying success type.

## Constructors

* [`ITry<A> Failure(Exception value)`][Failure]
* [`ITry<A> Success(A value)`][Success]

## Static Methods

* [`Func<ITry<A>, Exception> FromFailure(Exception defaultValue)`][FromFailure]
* [`Exception FromFailure(Exception defaultValue, ITry<A> tryable)`][FromFailure]
* [`Func<ITry<A>, R> FromSuccess(R defaultValue)`][FromSuccess]
* [`R FromSuccess(R defaultValue, ITry<A> tryable)`][FromSuccess]
* [`IEnumerable<Exception> Failures(IEnumerable<ITry<A>> enumerable)`][Failures]
* [`(IEnumerable<Exception>, IEnumerable<R>) PartitionITries(IEnumerable<ITry<A>> enumerable)`][PartitionITries]
* [`IEnumerable<R> Successes(IEnumerable<ITry<A>> enumerable)`][Successes]
* [`Func<Func<R, C>, Func<ITry<A>, C>> TryMap(Func<Exception, C>)`][TryMap]
* [`Func<ITry<A>, C> TryMap(Func<Exception, C>, Func<R, C>)`][TryMap]
* [`C TryMap(Func<Exception, C>, Func<R, C>, ITry<A>)`][TryMap]

## Instance Methods

* `boolean IsFailure()`: Determines whether or not the `ITry` is a `Failure`.
* `boolean IsSuccess()`: Determines whether or not the `ITry` is a `Success`.

[TryMap]: ./static/TryMap.md
[FromFailure]: ./static/FromFailure.md
[FromSuccess]: ./static/FromSuccess.md
[Failure]: ./constructors/Failure.md
[Failures]: ./static/Failures.md
[PartitionITries]: ./static/PartitionITries.md
[Success]: ./constructors/Success.md
[Successes]: ./static/Successes.md
