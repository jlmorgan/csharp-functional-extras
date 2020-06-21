using static FunctionalExtras.Objects;

using System;

namespace FunctionalExtras.Data
{
  public static class ValueTuples
  {
    /// <summary>
    /// Curried implementation of <see cref="Of{A, B}"/>.
    /// </summary>
    /// <param name="first">The first element.</param>
    /// <typeparam name="A">The <code>first</code> type parameter.</typeparam>
    /// <typeparam name="B">The <code>second</code> type parameter.</typeparam>
    /// <returns>A function that takes the second value and returns the tuple.</returns>
    public static Func<B, (A, B)> Of<A, B>(A first) => second => Of(first, second);

    /// <summary>
    /// Static constructor for <see cref="ValueTuple{T1, T2}"/>.
    /// </summary>
    /// <param name="first">The first element.</param>
    /// <typeparam name="A">The <code>first</code> type parameter.</typeparam>
    /// <typeparam name="B">The <code>second</code> type parameter.</typeparam>
    /// <returns>A tuple of two values.</returns>
    public static (A, B) Of<A, B>(A first, B second) => (first, second);

    /// <summary>
    /// Curried implementation of <see cref="TupleMap{A, B, C}(Func{A, B}, Func{A, C}, A)"/>.
    /// </summary>
    /// <typeparam name="A">The value type.</typeparam>
    /// <typeparam name="B">The underlying first type for the returned <see cref="ValueTuple{T1, T2}"/>.</typeparam>
    /// <typeparam name="C">The underlying second type for the returned <see cref="ValueTuple{T1, T2}"/>.</typeparam>
    /// <param name="firstMorphism">The function to map the <code>value</code> into the first element.</param>
    /// <returns>
    /// A function that takes the <code>secondMorphism</code> and a function that takes the tuple and
    /// returns the mapped tuple.
    /// </returns>
    public static Func<Func<A, C>, Func<A, (B, C)>> TupleMap<A, B, C>(Func<A, B> firstMorphism)
      => secondMorphism => TupleMap(firstMorphism, secondMorphism);

    /// <summary>
    /// Partially curried implementation of <see cref="TupleMap{A, B, C}(Func{A, B}, Func{A, C}, A)"/>.
    /// </summary>
    /// <typeparam name="A">The value type.</typeparam>
    /// <typeparam name="B">The underlying first type for the returned <see cref="ValueTuple{T1, T2}"/>.</typeparam>
    /// <typeparam name="C">The underlying second type for the returned <see cref="ValueTuple{T1, T2}"/>.</typeparam>
    /// <param name="firstMorphism">The function to map the <code>value</code> into the first element.</param>
    /// <param name="secondMorphism">The function to map the <code>value</code> into the second element.</param>
    /// <returns>A function that takes the tuple and returns the mapped tuple.</returns>
    public static Func<A, (B, C)> TupleMap<A, B, C>(Func<A, B> firstMorphism, Func<A, C> secondMorphism)
      => value => TupleMap(firstMorphism, secondMorphism, value);

    /// <summary>
    /// Maps the <code>value</code> into the elements of the <see cref="ValueTuple{T1, T2}"/> after applying a morphism
    /// for each position.
    /// </summary>
    /// <typeparam name="A">The value type.</typeparam>
    /// <typeparam name="B">The underlying first type for the returned <see cref="ValueTuple{T1, T2}"/>.</typeparam>
    /// <typeparam name="C">The underlying second type for the returned <see cref="ValueTuple{T1, T2}"/>.</typeparam>
    /// <param name="firstMorphism">The function to map the <code>value</code> into the first element.</param>
    /// <param name="secondMorphism">The function to map the <code>value</code> into the second element.</param>
    /// <param name="value">The value.</param>
    /// <returns>The mapped tuple.</returns>
    public static (B, C) TupleMap<A, B, C>(Func<A, B> firstMorphism, Func<A, C> secondMorphism, A value)
      => (
        RequireNonNull(firstMorphism, "firstMorphism must not be null")(value),
        RequireNonNull(secondMorphism, "secondMorphism must not be null")(value)
      );
  }
}
