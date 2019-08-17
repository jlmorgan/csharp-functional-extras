namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Validation"/> type is a right-biased disjunction that represents two possibilities; either a
  /// <code>Failure</code> of <code>a</code> or a <code>Success</code> of <code>b</code>. By convention, the
  /// <see cref="Validation"/> is used to represent a value or failure result of some function or process as a
  /// <code>Failure</code> of the failure message or a <code>Success</code> of the value.
  /// </summary>
  public interface IValidation<A, B>
  {
    /// <summary>
    /// Determines whether or not the instance is a <code>Failure</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Failure</code>; otherwise, <code>false</code>.</returns>
    bool IsFailure();

    /// <summary>
    /// Determines whether or not the instance is a <code>Success</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Success</code>; otherwise, <code>false</code>.</returns>
    bool IsSuccess();
  }
}
