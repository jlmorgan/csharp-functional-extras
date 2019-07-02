using System;

namespace FunctionalExtras
{
  public static class Objects
  {
    public static bool IsNull<T>(T instance) => instance == null;
    public static bool IsNotNull<T>(T instance) => !IsNull(instance);
    public static T RequireNonNull<T>(T instance, string message) => IsNull(instance)
      ? throw new ArgumentNullException(message)
      : instance;
  }
}
