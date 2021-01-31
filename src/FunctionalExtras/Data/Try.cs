using static FunctionalExtras.Objects;

using System;
using System.Linq;
using System.Collections.Generic;

namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Try"/> type is a right-biased disjunction that represents two possibilities; either a
  /// <code>Failure</code> of <code>a</code> or a <code>Success</code> of <code>b</code>. By convention, the
  /// <see cref="Try"/> is used to represent a value or failure result of some function or process as a
  /// <code>Failure</code> or a <code>Success</code> of the value.
  /// </summary>
  public static class Try
  {
    /// <summary>
    /// Wraps a successfyk execution in a <see cref="Success{V}(V)"/> and a thrown <see cref="Exception"/> in a
    /// <see cref="Failure{V}(Exception)"/>.
    /// </summary>
    /// <typeparam name="V">The return type of the <code>supplier</code>.</typeparam>
    /// <param name="supplier">A function that supplies the value.</param>
    /// <returns>A <see cref="ITry"/> of the value.</returns>
    public static ITry<V> Attempt<V>(Func<V> supplier)
    {
      try
      {
        return new Success<V>(supplier());
      }
      catch(Exception exception)
      {
        return new Failure<V>(exception);
      }
    }

    /// <summary>
    /// Creates a <code>Failure</code> from an <see cref="Exception"/>.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <param name="exception">The value.</param>
    /// <returns>A <code>Failure</code> of the value.</returns>
    public static ITry<V> Failure<V>(Exception exception) => new Failure<V>(exception);

    /// <summary>
    /// Extracts from a collection of <see cref="Try"/> all of the <code>Failure</code> elements in extracted
    /// order.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Try"/></param>
    /// <returns>The enumerable of underlying <code>Failure</code> values.</returns>
    public static IEnumerable<Exception> Failures<V>(IEnumerable<ITry<V>> enumerable)
      => (enumerable ?? Enumerable.Empty<ITry<V>>())
        .Where(IsNotNull)
        .Where(IsFailure)
        .Select(FromFailure<V>(default));

    /// <summary>
    /// Curried implementation of <see cref="FromFailure{F, S}(IEnumerable{F}, ITry{F, S})"/>.
    /// </summary>
    public static Func<ITry<V>, Exception> FromFailure<V>(Exception defaultValue) => tryable
      => FromFailure(defaultValue, tryable);

    /// <summary>
    /// Extracts the value(s) of a <code>Failure</code>; otherwise, returns the <code>defaultValues</code>.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <param name="defaultValue">Value used if the <code>tryable</code> is not a <code>Failure</code>.</param>
    /// <param name="tryable">The <see cref="Try"/>.</param>
    /// <returns>The underlying failure value or the default exception.</returns>
    public static Exception FromFailure<V>(Exception defaultValue, ITry<V> tryable) => tryable is Failure<V> failure
      ? failure._value
      : defaultValue;

    /// <summary>
    /// Curried implementation of <see cref="FromSuccess{F, S}(S, ITry{F, S})"/>.
    /// </summary>
    public static Func<ITry<V>, V> FromSuccess<V>(V defaultValue) => tryable => FromSuccess(defaultValue, tryable);

    /// <summary>
    /// Extracts the value of a <code>Success</code>; otherwise, returns the <code>defaultValue</code>.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <param name="defaultValue">Value used if the <code>tryable</code> is not a <code>Success</code>.</param>
    /// <param name="tryable">The <see cref="Try"/>.</param>
    /// <returns>The underlying success value or the default.</returns>
    public static V FromSuccess<V>(V defaultValue, ITry<V> tryable) => tryable is Success<V> success
      ? success._value
      : defaultValue;

    /// <summary>
    /// Determines whether or not the instance is a <code>Failure</code>.
    /// </summary>
    /// <param name="tryable">The <see cref="Try"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns><code>true</code> for a <code>Failure</code>; otherwise, <code>false</code>.</returns>
    public static bool IsFailure<V>(ITry<V> tryable) => tryable.IsFailure();

    /// <summary>
    /// Determines whether or not the instance is a <code>Success</code>.
    /// </summary>
    /// <param name="tryable">The <see cref="Try"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns><code>true</code> for a <code>Success</code>; otherwise, <code>false</code>.</returns>
    public static bool IsSuccess<V>(ITry<V> tryable) => tryable.IsSuccess();

    /// <summary>
    /// Partitions a collection of <see cref="Try"/> into two collections. All <code>Failure</code> elements are
    /// extracted, in order, to the first position of the output. Similarly for the <code>Success</code> elements in the
    /// second position.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Try"/></param>
    /// <returns>A couple of a collection of the underlying <code>Failure</code> values and a collection of the
    /// underlying <code>Success</code> values.</returns>
    public static (IEnumerable<Exception>, IEnumerable<V>) PartitionTries<V>(IEnumerable<ITry<V>> enumerable)
      => (Failures(enumerable), Successes(enumerable));

    /// <summary>
    /// Creates a <code>Success</code> from an arbitrary value.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A <code>Success</code> of the value.</returns>
    public static ITry<V> Success<V>(V value) => new Success<V>(value);

    /// <summary>
    /// Extracts from a collection of <see cref="Try"/> all of the <code>Success</code> elements in extracted
    /// order.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Try"/></param>
    /// <returns>The enumerable of underlying <code>Success</code> values.</returns>
    public static IEnumerable<V> Successes<V>(IEnumerable<ITry<V>> enumerable)
      => (enumerable ?? Enumerable.Empty<ITry<V>>())
        .Where(IsNotNull)
        .Where(IsSuccess)
        .Select(FromSuccess<V>(default));

    /// <summary>
    /// Curried implementation of
    /// <see cref="TryMap{V, C}(Func{Exception, C}, Func{V, C}, ITry{V})"/>.
    /// </summary>
    public static Func<Func<V, C>, Func<ITry<V>, C>> TryMap<V, C>(Func<Exception, C> failureMorphism)
      => successMorphism => TryMap(failureMorphism, successMorphism);

    /// <summary>
    /// Partially curried implementation of <see cref="TryMap{V, C}(Func{Exception, C}, Func{V, C}, ITry{V})"/>.
    /// </summary>
    public static Func<ITry<V>, C> TryMap<V, C>(Func<Exception, C> failureMorphism, Func<V, C> successMorphism)
      => tryable => TryMap(failureMorphism, successMorphism, tryable);

    /// <summary>
    /// Provides a catamorphism of the <code>tryable</code> to a singular value. If the value is a <code>Failure</code>,
    /// apply the first function; otherwise, apply the second.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <typeparam name="C">The return type.</typeparam>
    /// <param name="failureMorphism">Maps the <see cref="Exception"/> to <code>c</code>.</param>
    /// <param name="successMorphism">Maps the value of a <code>Success s</code> to <code>c</code>.</param>
    /// <param name="tryable">The <see cref="Try"/>.</param>
    /// <returns>The result of the catamorphism of the <code>tryable</code>.</returns>
    /// <exception cref="ArgumentNullException">if the <code>failureMorphism</code>, <code>successMorphism</code>, or
    /// <code>tryable</code> is <code>null</code>.</exception>
    /// <see href="https://en.wikipedia.org/wiki/Catamorphism">Catamorphism</see>
    public static C TryMap<V, C>(Func<Exception, C> failureMorphism, Func<V, C> successMorphism, ITry<V> tryable)
      => RequireNonNull(tryable, $"{nameof(tryable)} must not be null").IsSuccess()
        ? RequireNonNull(successMorphism, $"{nameof(successMorphism)} must not be null")(FromSuccess(default, tryable))
        : RequireNonNull(failureMorphism, $"{nameof(failureMorphism)} must not be null")(FromFailure(default, tryable));
  }

  /// <summary>
  /// Encapsulates the falure value(s) of the disjunction.
  /// </summary>
  /// <typeparam name="A">The underlying failure type.</typeparam>
  /// <typeparam name="B">The underlying type.</typeparam>
  internal struct Failure<A> : ITry<A>
  {
    internal readonly Exception _value;

    internal Failure(Exception value) => _value = value;

    public override bool Equals(object obj) => obj is ITry<A> && Equals(obj as ITry<A>);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"Failure<{typeof(A)}> {_value}";

    public bool Equals(ITry<A> other) => other is Failure<A> failure && _value.Equals(failure._value);

    /// <summary>
    /// Determines whether or not the instance is a <code>Failure</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Failure</code>; otherwise, <code>false</code>.</returns>
    public bool IsFailure() => true;

    /// <summary>
    /// Determines whether or not the instance is a <code>Success</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Success</code>; otherwise, <code>false</code>.</returns>
    public bool IsSuccess() => false;
  }

  /// <summary>
  /// Encapsulates the success value of the disjunction.
  /// </summary>
  /// <typeparam name="A">The underlying failure type.</typeparam>
  /// <typeparam name="B">The underlying type.</typeparam>
  internal struct Success<A> : ITry<A>
  {
    internal readonly A _value;

    internal Success(A value) => _value = value;

    public override bool Equals(object obj) => obj is ITry<A> && Equals(obj as ITry<A>);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"Success<{typeof(A)}> {_value}";

    public bool Equals(ITry<A> other) => other is Success<A> success && _value.Equals(success._value);

    /// <summary>
    /// Determines whether or not the instance is a <code>Failure</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Failure</code>; otherwise, <code>false</code>.</returns>
    public bool IsFailure() => false;

    /// <summary>
    /// Determines whether or not the instance is a <code>Success</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Success</code>; otherwise, <code>false</code>.</returns>
    public bool IsSuccess() => true;
  }
}
