using System;

namespace Extensions
{
  public static class ValueTupleExtensions
  {
    /// <summary>
    /// The first element of the <code>Tuple</code>.
    /// </summary>
    /// <param name="tuple">A <code>Tuple</code>.</param>
    /// <typeparam name="A">The <code>first</code> type parameter.</typeparam>
    /// <typeparam name="B">The <code>second</code> type parameter.</typeparam>
    /// <returns>The first element.</returns>
    public static A First<A, B>(this (A, B) tuple) => tuple.Item1;

    /// <summary>
    /// The second element of the <code>Tuple</code>.
    /// </summary>
    /// <param name="tuple">A <code>Tuple</code>.</param>
    /// <typeparam name="A">The <code>first</code> type parameter.</typeparam>
    /// <typeparam name="B">The <code>second</code> type parameter.</typeparam>
    /// <returns>The second element.</returns>
    public static B Second<A, B>(this (A, B) tuple) => tuple.Item2;

    /// <summary>
    /// Creates a new <code>Tuple</code> with the values swapped.
    /// </summary>
    /// <param name="tuple">A <code>Tuple</code>.</param>
    /// <typeparam name="A">The <code>first</code> type parameter.</typeparam>
    /// <typeparam name="B">The <code>second</code> type parameter.</typeparam>
    /// <returns>The swapped <code>Tuple</code>.</returns>
    public static (B, A) Swap<A, B>(this (A, B) tuple) => (tuple.Item2, tuple.Item1);
  }
}
