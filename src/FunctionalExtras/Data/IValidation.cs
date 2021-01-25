namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Validation"/> type is a right-biased disjunction that represents two possibilities; either an
  /// <code>Invalid</code> of <code>a</code> or a <code>Valid</code> of <code>b</code>. By convention, the
  /// <see cref="Validation"/> is used to represent a value or invalid result of some function or process as an
  /// <code>Invalid</code> of the failure message or a <code>Valid</code> of the value.
  /// </summary>
  public interface IValidation<A, B>
  {
    /// <summary>
    /// Determines whether or not the instance is a <code>Invalid</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Invalid</code>; otherwise, <code>false</code>.</returns>
    bool IsInvalid();

    /// <summary>
    /// Determines whether or not the instance is a <code>Valid</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Valid</code>; otherwise, <code>false</code>.</returns>
    bool IsValid();
  }
}
