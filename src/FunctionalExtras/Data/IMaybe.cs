using System;

namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Maybe"/> type is a disjunction that wraps an arbitrary value. The <see cref="Maybe"/>
  /// <code>a</code> either contains a value of type <code>a</code> (read: <code>Just a</code>) or empty (read:
  /// <code>Nothing</code>). <see cref="Maybe"/> provides a way to deal with error or exceptional behavior.
  /// </summary>
  public interface IMaybe<A>
  {
    /// <summary>
    /// Tests the underlying value against the <code>predicate</code>, returning the <code>Just</code> of the value for
    /// <code>true</code>; otherwise, <code>Nothing</code>.
    /// </summary>
    /// <param name="predicate">The predicate with which to test the value.</param>
    /// <returns>The <code>Just</code> of the value for <code>true</code>; otherwise, <code>Nothing</code>.</returns>
    /// <exception cref="ArgumentNullException">If the <code>predicate</code> is <code>null</code>.</exception>
    IMaybe<A> Filter(Predicate<A> predicate);

    /// <summary>
    /// Maps the underlying value of a <see cref="Maybe"/> in a <code>null</code>-safe way.
    /// </summary>
    /// <typeparam name="B">The return type of the <code>morphism</code>.</typeparam>
    /// <param name="morphism">The morphism.</param>
    /// <returns>The mapped <see cref="Maybe"/>.</returns>
    IMaybe<B> FMap<B>(Func<A, B> morphism);

    /// <summary>
    /// Determines whether or not the <see cref="Maybe"/> is a <code>Just</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Just</code>; otherwise, <code>false</code>.</returns>
    bool IsJust();

    /// <summary>
    /// Determines whether or not the <see cref="Maybe"/> is a <code>Nothing</code>.
    /// </summary>
    /// <returns><code>true</code> for a <code>Nothing</code>; otherwise, <code>false</code>.</returns>
    bool IsNothing();
  }
}
