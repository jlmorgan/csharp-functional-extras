using static FunctionalExtras.Objects;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalExtras.Data
{
  public static class Lists
  {
    /// <summary>
    /// Curried implementation of <see cref="Append{A}(List{A}, List{A})"/>.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="second">The list to append.</param>
    /// <returns>A function that takes the first list and returns the appended list.</returns>
    public static Func<List<A>, List<A>> Append<A>(List<A> second) => first => Append(second, first);

    /// <summary>
    /// Appends two lists together. <code>null</code> lists are treated as empty lists.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="second">The list to append.</param>
    /// <param name="first">The list on which to append.</param>
    /// <returns>The appended list.</returns>
    public static List<A> Append<A>(List<A> second, List<A> first) => (List<A>) (first ?? Empty<A>())
      .Concat(second ?? Empty<A>()).ToList();

    /// <summary>
    /// Creates an empty list.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <returns>The empty list.</returns>
    public static List<A> Empty<A>() => new List<A>();

    /// <summary>
    /// Curried implementation of <see cref="FoldLeft{A, B}(Func{B, A, B}, B, List{A})"/>.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The resulting type.</typeparam>
    /// <param name="fold">Folding function.</param>
    /// <returns>
    /// A function that takes an initial value and a function that takes a list and returns the folded
    /// result.
    /// </returns>
    public static Func<B, Func<List<A>, B>> FoldLeft<A, B>(Func<B, A, B> fold) => initialValue
      => FoldLeft(fold, initialValue);

    /// <summary>
    /// Partially curried implementation of <see cref="FoldLeft{A, B}(Func{B, A, B}, B, List{A})"/>.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The resulting type.</typeparam>
    /// <param name="fold">Folding function.</param>
    /// <param name="initialValue">Initial value.</param>
    /// <returns>A function that takes a list and returns the folded result.</returns>
    public static Func<List<A>, B> FoldLeft<A, B>(Func<B, A, B> fold, B initialValue) => list
      => FoldLeft(fold, initialValue, list);

    /// <summary>
    /// Folds the list from last to head.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The resulting type.</typeparam>
    /// <param name="fold">Folding function.</param>
    /// <param name="initialValue">Initial value.</param>
    /// <param name="list">The list.</param>
    /// <returns>The result of the fold.</returns>
    /// <exception cref="ArgumentNullException">if the <code>fold</code> is <code>null</code>.</exception>
    public static B FoldLeft<A, B>(Func<B, A, B> fold, B initialValue, List<A> list)
    {
      RequireNonNull(fold, "fold must not be null");

      int index = (list ?? Empty<A>()).Count;
      B result = initialValue;

      while (index > 0)
      {
        index -= 1;

        result = fold(result, list[index]);
      }

      return result;
    }

    /// <summary>
    /// Curried implementation of <see cref="FoldRight{A, B}(Func{A, B, B}, B, List{A})"/>.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The resulting type.</typeparam>
    /// <param name="fold">Folding function.</param>
    /// <returns>
    /// A function that takes an initial value and a function that takes a list and returns the folded
    /// result.
    /// </returns>
    public static Func<B, Func<List<A>, B>> FoldRight<A, B>(Func<A, B, B> fold) => initialValue
      => FoldRight(fold, initialValue);

    /// <summary>
    /// Partially curried implementation of <see cref="FoldRight{A, B}(Func{A, B, B}, B, List{A})"/>.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The resulting type.</typeparam>
    /// <param name="fold">Folding function.</param>
    /// <param name="initialValue">Initial value.</param>
    /// <returns>A function that takes a list and returns the folded result.</returns>
    public static Func<List<A>, B> FoldRight<A, B>(Func<A, B, B> fold, B initialValue) => list
      => FoldRight(fold, initialValue, list);

    /// <summary>
    /// Fold the list from head to last.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The resulting type.</typeparam>
    /// <param name="fold">Folding function.</param>
    /// <param name="initialValue">Initial value.</param>
    /// <param name="list">The list.</param>
    /// <returns>The result of the fold.</returns>
    /// <exception cref="ArgumentNullException">if the <code>fold</code> is <code>null</code>.</exception>
    public static B FoldRight<A, B>(Func<A, B, B> fold, B initialValue, List<A> list)
    {
      RequireNonNull(fold, "fold must not be null");

      int length = (list ?? Empty<A>()).Count;
      int index = 0;
      B result = initialValue;

      while (index < length)
      {
        result = fold(list[index], result);

        index += 1;
      }

      return result;
    }

    /// <summary>
    /// Extracts the first element of a non-null, non-empty list.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns>The first element.</returns>
    /// <exception cref="ArgumentException">if the <code>list</code> is <code>null</code> or empty.</exception>
    public static A Head<A>(List<A> list) => RequireNonEmpty(list)[0];

    /// <summary>
    /// Extracts the elements of a non-null, non-empty list excluding the last element.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns>The initial part of the list.</returns>
    /// <exception cref="ArgumentException">if the <code>list</code> is <code>null</code> or empty.</exception>
    public static List<A> Init<A>(List<A> list) => RequireNonEmpty(list).Take(list.Count() - 1).ToList();

    /// <summary>
    /// Determines whether or not the list is <code>null</code> empty.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns><code>true</code> if the <code>list</code> is empty; otherwise, <code>false</code>.</returns>
    public static bool IsEmpty<A>(List<A> list) => IsNull(list) || list.Count() == 0;

    /// <summary>
    /// Determines whether or not the list is not empty.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns><code>true</code> if the <code>list</code> is not empty; otherwise, <code>false</code>.</returns>
    public static bool IsNotEmpty<A>(List<A> list) => !IsEmpty(list);

    /// <summary>
    /// Extracts the last element of a non-null, non-empty list.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns>The last element.</returns>
    /// <exception cref="ArgumentException">if the <code>list</code> is <code>null</code> or empty.</exception>
    public static A Last<A>(List<A> list) => RequireNonEmpty(list).Last();

    /// <summary>
    /// Gets the length of the <code>list</code>; otherwise, defaults to <code>0</code>.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns>The length of the list.</returns>
    public static int Length<A>(List<A> list) => IsNull(list) ? 0 : list.Count();

    /// <summary>
    /// Curried implementation of <see cref="Map{A, B}(Func{A, B}, List{A})"/>.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The mapped type.</typeparam>
    /// <param name="morphism">The mapping function.</param>
    /// <returns>A function that takes a list and returns the mapped list.</returns>
    public static Func<List<A>, List<B>> Map<A, B>(Func<A, B> morphism) => list => Map(morphism, list);

    /// <summary>
    /// Maps the values of the <code>list</code> into a new list.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <typeparam name="B">The mapped type.</typeparam>
    /// <param name="morphism">The mapping function.</param>
    /// <param name="list">The list.</param>
    /// <returns>The mapped list.</returns>
    public static List<B> Map<A, B>(Func<A, B> morphism, List<A> list) => (list ?? Empty<A>())
      .Select(RequireNonNull(morphism, "morphism must not be null"))
      .ToList();

    /// <summary>
    /// Throws an exception of the <code>list</code> is <code>null</code> or empty.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns>The list.</returns>
    public static List<A> RequireNonEmpty<A>(List<A> list) => IsEmpty(list)
      ? throw new ArgumentException("list must be a non-empty List")
      : list;

    /// <summary>
    /// All elements of the list but the first or an empty list.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns>The tail part of the list.</returns>
    /// <exception cref="ArgumentException">if the <code>list</code> is <code>null</code> or empty.</exception>
    public static List<A> Tail<A>(List<A> list) => RequireNonEmpty(list).Skip(1).ToList();

    /// <summary>
    /// Decomposes a list into its head and tail. Returns <see cref="Nothing{A}"/> if the <code>list</code> is empty;
    /// otherwise, a <see cref="Just{A}"/> of <code>(x, xs)</code> where <code>x</code> is the head and <code>xs</code>
    /// is the tail.
    /// </summary>
    /// <typeparam name="A">The underlying type.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns><see cref="Just{A}"/> of the heaed and tail; otherwise, <see cref="Nothing{A}"/>.</returns>
    public static IMaybe<(A, List<A>)> Uncons<A>(List<A> list) => Maybe.Of(list)
      .Filter(IsNotEmpty)
      .FMap(ValueTuples.TupleMap<List<A>, A, List<A>>(Head, Tail));
  }
}
