namespace FunctionalExtras.Data
{
  public interface IMaybe<A>
  {
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
