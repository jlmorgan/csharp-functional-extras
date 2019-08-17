using System;

namespace FunctionalExtras.Data
{
  public static class ValueTuples
  {
    /// <summary>
    /// Curried implementation of <see cref="ValueTuple{T1, T2}"/> constructor.
    /// </summary>
    /// <param name="first">The first element.</param>
    /// <typeparam name="A">The <code>first</code> type parameter.</typeparam>
    /// <typeparam name="B">The <code>second</code> type parameter.</typeparam>
    /// <returns>A <code>Tuple</code> of two values.</returns>
    public static Func<B, (A, B)> Of<A, B>(A first) => second => (first, second);
  }
}
