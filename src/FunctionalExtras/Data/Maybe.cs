using static FunctionalExtras.Objects;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Maybe"/> type is a disjunction that wraps an arbitrary value. The <see cref="Maybe"/>
  /// <code>a</code> either contains a value of type <code>a</code> (read: <code>Just a</code>) or empty (read:
  /// <code>Nothing</code>). <see cref="Maybe"/> provides a way to deal with error or exceptional behavior.
  /// </summary>
  /// <see cref="IMaybe{A}"/>
  public static class Maybe
  {
    /// <summary>
    /// Takes a list of <see cref="Maybe"/> and returns a list of the <code>Just</code> values.
    /// </summary>
    /// <param name="list">List of <see cref="Maybe"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns>A list of the <code>Just</code> values.</returns>
    public static List<V> CatMaybes<V>(List<IMaybe<V>> list) => list == null
      ? new List<V>()
      : list.Where(IsJust)
        .Select(FromJust)
        .ToList();

    /// <summary>
    /// Extracts the value out of a <code>Just</code> and throws an error if its argument is a <code>Nothing</code>.
    /// </summary>
    /// <param name="maybe">The <see cref="Maybe"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns>The underlying value.</returns>
    public static V FromJust<V>(IMaybe<V> maybe) => maybe is Just<V> just
      ? just._value
      : throw new ArgumentException($"{nameof(maybe)} must not be null or Nothing");

    /// <summary>
    /// The curried implementation of <see cref="FromMaybe{T}(T, IMaybe{T})"/>.
    /// </summary>
    public static Func<IMaybe<V>, V> FromMaybe<V>(V defaultValue) => maybe => FromMaybe(defaultValue, maybe);

    /// <summary>
    /// Takes a <code>defaultValue</code> and a <see cref="Maybe"/> value. If the <see cref="Maybe"/> is
    /// <code>Nothing</code>, it returns the <code>defaultValue</code>; otherwise, it returns the underlying value of
    /// the <code>Just</code>.
    /// </summary>
    /// <param name="defaultValue">The value to use if the <code>maybeMap</code> is <code>null</code> or
    /// <code>Nothing</code>.</param>
    /// <param name="maybe">The <see cref="Maybe"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns>The underlying value for a <code>Just</code>; otherwise, the <code>defaultValue</code>.</returns>
    public static V FromMaybe<V>(V defaultValue, IMaybe<V> maybe) => maybe is Just<V> just
      ? just._value
      : defaultValue;

    /// <summary>
    /// Determines whether or not the <code>maybe</code> is a <code>Just</code>.
    /// </summary>
    /// <param name="maybe">The <see cref="Maybe"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns><code>true</code> for a <code>Just</code>; otherwise, <code>false</code>.</returns>
    public static bool IsJust<V>(IMaybe<V> maybe) => maybe.IsJust();

    /// <summary>
    /// Determines whether or not the <code>maybe</code> is a <code>Nothing</code>.
    /// </summary>
    /// <param name="maybe">The <see cref="Maybe"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns><code>true</code> for a <code>Nothing</code>; otherwise, <code>false</code>.</returns>
    public static bool IsNothing<V>(IMaybe<V> maybe) => maybe.IsNothing();

    /// <summary>
    /// Creates a <code>Just</code> of the value.
    /// </summary>
    /// <param name="value">A non-null value.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns>The <see cref="Maybe"/> of the <code>value</code>.</returns>
    public static IMaybe<V> Just<V>(V value) => value == null
      ? throw new ArgumentNullException($"{nameof(value)} must not be null")
      : new Just<V>(value);

    /// <summary>
    /// Returns <code>Nothing</code> for an empty <code>list</code> or <code>Just</code> of the first element in the
    /// <code>list</code>.
    /// </summary>
    /// <param name="list">The <code>list</code> of values.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns>A <code>Just</code> of the first non-null value; otherwise, <code>Nothing</code>.</returns>
    public static IMaybe<V> ListToMaybe<V>(List<V> list) => list == null
      ? Nothing<V>()
      : list.Where(IsNotNull).Select(Just).FirstOrDefault(IsJust) ?? Nothing<V>();

    /// <summary>
    /// Curried implementation of <see cref="MapMaybe{T, R}(Func{T, IMaybe{R}}, List{T})"/>.
    /// </summary>
    public static Func<List<V>, List<R>> MapMaybe<V, R>(Func<V, IMaybe<R>> morphism) =>
      list => MapMaybe(morphism, list);

    /// <summary>
    /// A version of <code>map</code> which can throw out elements. If the result of the function is
    /// <code>Nothing</code>, no element is added to the result list; otherwise, the value is added.
    /// </summary>
    /// <param name="morphism">The function that maps the value in the <code>list</code> to a <see cref="Maybe"/> of the
    /// result.</param>
    /// <param name="list">The <code>list</code> of values.</param>
    /// <typeparam name="V">The underlying type of the <code>list</code>.</typeparam>
    /// <typeparam name="R">The underlying type of the result <code>list</code>.</typeparam>
    /// <returns>A list of mapped <code>Just</code> values.</returns>
    /// <exception cref="ArgumentNullException">if the <code>morphism</code> is <code>null</code>.</exception>
    public static List<R> MapMaybe<V, R>(Func<V, IMaybe<R>> morphism, List<V> list) => morphism == null
      ? throw new ArgumentNullException($"{nameof(morphism)} must not be null")
      : (list ?? new List<V>())
        .Select(morphism)
        .Where(IsJust)
        .Select(FromJust)
        .ToList();

    /// <summary>
    /// Curried implementation of <see cref="MaybeMap{T, R}(R, Func{T, R}, IMaybe{T})"/>.
    /// </summary>
    public static Func<Func<V, R>, Func<IMaybe<V>, R>> MaybeMap<V, R>(R defaultValue) => morphism =>
      maybe => MaybeMap(defaultValue, morphism)(maybe);

    /// <summary>
    /// Partially curried implementation of <see cref="MaybeMap{T, R}(R, Func{T, R}, IMaybe{T})"/>.
    /// </summary>
    public static Func<IMaybe<V>, R> MaybeMap<V, R>(R defaultValue, Func<V, R> morphism) =>
      maybe => MaybeMap(defaultValue, morphism, maybe);

    /// <summary>
    /// If the <see cref="Maybe"/>} value is a <code>Nothing</code>, it returns the <code>defaultValue</code>;
    /// otherwise, applies the <code>morphism</code> to the underlying value in the <code>Just</code> and returns the
    /// result.
    /// </summary>
    /// <param name="defaultValue">The value to use if the <code>maybeMap</code> is <code>null</code> or
    /// <code>Nothing</code>.</param>
    /// <param name="morphism">The function to map the underlying value of the <code>maybeMap</code> to the same type as
    /// the return type.</param>
    /// <param name="maybe">The <see cref="Maybe"/>.</param>
    /// <typeparam name="V">The underlying type of the <code>maybeMap</code>.</typeparam>
    /// <typeparam name="R">The default and return type.</typeparam>
    /// <returns>The mapped value or default.</returns>
    /// <exception cref="ArgumentNullException">if the <code>morphism</code> is <code>null</code>.</exception>
    public static R MaybeMap<V, R>(R defaultValue, Func<V, R> morphism, IMaybe<V> maybe) => (
      morphism == null
        ? throw new ArgumentNullException($"{nameof(morphism)} must not be null")
        : ((maybe ?? Nothing<V>()).IsJust() ? morphism(FromJust(maybe)) : defaultValue)
    );

    /// <summary>
    /// Returns an empty list for <code>Nothing</code>; otherwise, a singleton list of the underlying value of the
    /// <code>Just</code>.
    /// </summary>
    /// <param name="maybe">The <see cref="Maybe"/>.</param>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns>A singleton list of the underlying value within the <code>maybeMap</code> for a <code>Just</code>;
    /// otherwise, an empty list for <code>Nothing</code>.</returns>
    public static List<V> MaybeToList<V>(IMaybe<V> maybe) => maybe == null
      ? new List<V>()
      : (maybe.IsJust() ? new List<V> { FromJust(maybe) } : new List<V>());

    /// <summary>
    /// Creates a nothing to represent <code>null</code> or a missing value.
    /// </summary>
    /// <typeparam name="V">The underlying type.</typeparam>
    /// <returns>A <code>Nothing</code>.</returns>
    public static IMaybe<V> Nothing<V>() => new Nothing<V>();
  }

  internal struct Just<A> : IMaybe<A>
  {
    internal readonly A _value;

    internal Just(A value) => _value = value;

    public override bool Equals(object obj) => obj is IMaybe<A> && Equals(obj as IMaybe<A>);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"Just<{typeof(A)}>({_value})";

    public bool Equals(IMaybe<A> other) => other is Just<A>
      && _value.Equals(((Just<A>) other)._value);

    /// <summary>
    /// Determines whether or not the <see cref="Maybe"/> is a <code>Just</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Just</code>; otherwise, <code>false</code>.</returns>
    public bool IsJust() => true;

    /// <summary>
    /// Determines whether or not the <see cref="Maybe"/> is a <code>Nothing</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Nothing</code>; otherwise, <code>false</code>.</returns>
    public bool IsNothing() => false;
  }

  internal struct Nothing<A> : IMaybe<A>
  {
    public override bool Equals(object obj) => obj is IMaybe<A> && Equals(obj as IMaybe<A>);
    public override int GetHashCode() => default(A).GetHashCode();
    public override string ToString() => $"Nothing<{typeof(A)}>()";

    public bool Equals(IMaybe<A> other) => other is Nothing<A>;

    /// <summary>
    /// Determines whether or not the <see cref="Maybe"/> is a <code>Just</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Just</code>; otherwise, <code>false</code>.</returns>
    public bool IsJust() => false;

    /// <summary>
    /// Determines whether or not the <see cref="Maybe"/> is a <code>Nothing</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Nothing</code>; otherwise, <code>false</code>.</returns>
    public bool IsNothing() => true;
  }
}
