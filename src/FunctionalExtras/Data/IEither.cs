namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Either"/> type is a right-biased disjunction that represents two possibilities: either a
  /// <code>Left</code> of <code>a</code> or a <code>Right</code> of <code>b</code>. By convention, the
  /// <see cref="Either"/> is used to represent a value or an error result of some function or process as a
  /// <code>Left</code> of the error or a <code>Right</code> of the value.
  /// </summary>
  public interface IEither<A, B>
  {
    /// <summary>
    /// Determines whether or not the instance is a <code>Left</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Left</code>; otherwise, <code>false</code>.</returns>
    bool IsLeft();

    /// <summary>
    /// Determines whether or not the instance is a <code>Right</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Right</code>; otherwise, <code>false</code>.</returns>
    bool IsRight();
  }
}
