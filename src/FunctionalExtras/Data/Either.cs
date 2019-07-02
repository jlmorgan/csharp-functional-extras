using static FunctionalExtras.Objects;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Either"/> type is a right-biased disjunction that represents two possibilities: either a
  /// <code>Left</code> of <code>a</code> or a <code>Right</code> of <code>b</code>. By convention, the
  /// <see cref="Either"/> is used to represent a value or an error result of some function or process as a
  /// <code>Left</code> of the error or a <code>Right</code> of the value.
  /// </summary>
  /// <see cref="IEither{A, B}"/>
  public static class Either
  {
    /// <summary>
    /// Curried implementation of <see cref="Either.EitherMap{T, U, C}(Func{T, C}, Func{U, C}, IEither{T, U})"/>.
    /// </summary>
    public static Func<Func<U, C>, Func<IEither<T, U>, C>> EitherMap<T, U, C>(Func<T, C> leftMorphism) =>
      rightMorphism => EitherMap(leftMorphism, rightMorphism);

    /// <summary>
    /// Partially curried implementation of <see cref="EitherMap{T, U, C}(Func{T, C}, Func{U, C}, IEither{T, U})"/>.
    /// </summary>
    public static Func<IEither<T, U>, C> EitherMap<T, U, C>(Func<T, C> leftMorphism, Func<U, C> rightMorphism) =>
      either => EitherMap(leftMorphism, rightMorphism, either);

    /// <summary>
    /// Provides a catamorphism of the <code>either</code> to a singular value. If the value is <code>Left a</code>,
    /// apply the first function to <code>a</code>; otherwise, apply the second function to <code>b</code>.
    /// </summary>
    /// <returns>The result of the catamorphism of the <code>either</code>.</returns>
    /// <param name="leftMorphism">Maps the value of a <code>Left t</code> to <code>c</code>.</param>
    /// <param name="rightMorphism">Maps the value of a <code>Right u</code> to <code>c</code>..</param>
    /// <param name="either">The <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left value type parameter.</typeparam>
    /// <typeparam name="U">The underlying right value type parameter.</typeparam>
    /// <typeparam name="C">The return type parameter.</typeparam>
    /// <exception cref="ArgumentNullException">if the <code>leftMorphism</code>, <code>rightMorphism</code>, or
    /// <code>either</code> is <code>null</code>.</exception>
    /// <see href="https://en.wikipedia.org/wiki/Catamorphism">Catamorphism</see>
    public static C EitherMap<T, U, C>(Func<T, C> leftMorphism, Func<U, C> rightMorphism, IEither<T, U> either) =>
      RequireNonNull(either, $"{nameof(either)} must not be null").IsRight()
        ? RequireNonNull(rightMorphism, $"{nameof(rightMorphism)} must not be null")(FromRight(default, either))
        : RequireNonNull(leftMorphism, $"{nameof(leftMorphism)} must not be null")(FromLeft(default, either));

    /// <summary>
    /// Curried implementation of <see cref="Either.FromLeft{T, U}(T, IEither{T, U})"/>.
    /// </summary>
    public static Func<IEither<T, U>, T> FromLeft<T, U>(T defaultValue) => either => FromLeft(defaultValue, either);

    /// <summary>
    /// Extracts the value of a <code>Left</code>; otherwise, returns the <code>defaultValue</code>.
    /// </summary>
    /// <returns>The underlying left value or default.</returns>
    /// <param name="defaultValue">Value used if the <code>either</code> is not a <code>Left</code>.</param>
    /// <param name="either">The <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static T FromLeft<T, U>(T defaultValue, IEither<T, U> either) => either is Left<T, U> left
      ? left._value
      : defaultValue;

    /// <summary>
    /// Curried implementation of <see cref="Either.FromRight{T, U}(U, IEither{T, U})"/>.
    /// </summary>
    public static Func<IEither<T, U>, U> FromRight<T, U>(U defaultValue) => either => FromRight(defaultValue, either);

    /// <summary>
    /// Extracts the value of a <code>Right</code>; otherwise, returns the <code>defaultValue</code>.
    /// </summary>
    /// <returns>The underlying right value or default.</returns>
    /// <param name="defaultValue">Value used if the <code>either</code> is not a <code>Right</code>.</param>
    /// <param name="either">The <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static U FromRight<T, U>(U defaultValue, IEither<T, U> either) => either is Right<T, U> right
      ? right._value
      : defaultValue;

    /// <summary>
    /// Determines whether or not the instance is a <code>Left</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Left</code>; otherwise, <code>false</code>.</returns>
    /// <param name="either">The <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static bool IsLeft<T, U>(IEither<T, U> either) => either.IsLeft();

    /// <summary>
    /// Determines whether or not the instance is a <code>Right</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Right</code>; otherwise, <code>false</code>.</returns>
    /// <param name="either">The <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static bool IsRight<T, U>(IEither<T, U> either) => either.IsRight();

    /// <summary>
    /// Creates a <code>Left</code> from an arbitrary <paramref name="value"/>.
    /// </summary>
    /// <returns>A <code>Left</code> of the value.</returns>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static IEither<T, U> Left<T, U>(T value) => new Left<T, U>(value);

    /// <summary>
    /// Extracts from an enumerable of <see cref="Either"/> all of the <code>Left</code> elements in extracted order.
    /// </summary>
    /// <returns>The underlying <code>Left</code> values.</returns>
    /// <param name="enumerable">The enumerable of <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static IEnumerable<T> Lefts<T, U>(IEnumerable<IEither<T, U>> enumerable) =>
      (enumerable ?? new List<IEither<T, U>>())
        .Where(IsNotNull)
        .Where(IsLeft)
        .Select(FromLeft<T, U>(default))
        .ToList();

    /// <summary>
    /// Partitions an enumerable of <see cref="Either"/> into two enumerables. All <code>Left</code> elements are
    /// extracted, in order, to the first position of the output. Similarly for the <code>Right</code> elements in the
    /// second position.
    /// </summary>
    /// <returns>A couple of an enumerable of the underlying <code>Left</code> values and an enumerable of the
    /// underlying <code>Right</code> values.</returns>
    /// <param name="enumerable">The enumerable of <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static (IEnumerable<T>, IEnumerable<U>) PartitionEithers<T, U>(IEnumerable<IEither<T, U>> enumerable) =>
      (Lefts(enumerable), Rights(enumerable));

    /// <summary>
    /// Creates a <code>Right</code> from an arbitrary <paramref name="value"/>.
    /// </summary>
    /// <returns>A <code>Right</code> of the value.</returns>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static IEither<T, U> Right<T, U>(U value) => new Right<T, U>(value);

    /// <summary>
    /// Extracts from an enumerable of <see cref="Either"/> all of the <code>Right</code> elements in extracted order.
    /// </summary>
    /// <returns>The underlying <code>Right</code> values.</returns>
    /// <param name="enumerable">The enumerable of <see cref="Either"/>.</param>
    /// <typeparam name="T">The underlying left type parameter.</typeparam>
    /// <typeparam name="U">The underlying right type parameter.</typeparam>
    public static IEnumerable<U> Rights<T, U>(IEnumerable<IEither<T, U>> enumerable) =>
      (enumerable ?? new List<IEither<T, U>>())
        .Where(IsNotNull)
        .Where(IsRight)
        .Select(FromRight<T, U>(default))
        .ToList();
  }

  internal struct Left<A, B> : IEither<A, B>
  {
    internal readonly A _value;

    internal Left(A value) => _value = value;

    public override bool Equals(object obj) => obj is IEither<A, B> && Equals(obj as IEither<A, B>);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"Left<{typeof(A)}> {_value}";

    public bool Equals(IEither<A, B> other) => other is Left<A, B> left && _value.Equals(left._value);

    /// <summary>
    /// Determines whether or not the instance is a <code>Left</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Left</code>; otherwise, <code>false</code>.</returns>
    public bool IsLeft() => true;

    /// <summary>
    /// Determines whether or not the instance is a <code>Right</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Right</code>; otherwise, <code>false</code>.</returns>
    public bool IsRight() => false;
  }

  internal struct Right<A, B> : IEither<A, B>
  {
    internal readonly B _value;

    internal Right(B value) => _value = value;

    public override bool Equals(object obj) => obj is IEither<A, B> && Equals(obj as IEither<A, B>);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"Right<{typeof(B)}> {_value}";

    public bool Equals(IEither<A, B> other) => other is Right<A, B> right && _value.Equals(right._value);

    /// <summary>
    /// Determines whether or not the instance is a <code>Left</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Left</code>; otherwise, <code>false</code>.</returns>
    public bool IsLeft() => false;

    /// <summary>
    /// Determines whether or not the instance is a <code>Right</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Right</code>; otherwise, <code>false</code>.</returns>
    public bool IsRight() => true;
  }
}
