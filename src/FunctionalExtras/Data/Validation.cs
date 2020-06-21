using static FunctionalExtras.Objects;

using System;
using System.Linq;
using System.Collections.Generic;

namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Validation"/> type is a right-biased disjunction that represents two possibilities; either a
  /// <code>Failure</code> of <code>a</code> or a <code>Success</code> of <code>b</code>. By convention, the
  /// <see cref="Validation"/> is used to represent a value or failure result of some function or process as a
  /// <code>Failure</code> of the failure message or a <code>Success</code> of the value.
  /// </summary>
  public static class Validation
  {
    /// <summary>
    /// Curried implementation of <see cref="Concat{F, S}(IValidation{F, S}, IValidation{F, S})"/>.
    /// </summary>
    public static Func<IValidation<F, S>, IValidation<F, S>> Concat<F, S>(IValidation<F, S> second) => first
      => Concat(second, first);

    /// <summary>
    /// Concatenates two <code>Failure</code> values together, replacing a <code>Success</code> with the
    /// <code>Failure</code>; otherwise, take the first <code>Success</code>.
    /// </summary>
    /// <param name="second">The second <see cref="Validation"/>.</param>
    /// <param name="first">The first <see cref="Validation"/>.</param>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying success type.</typeparam>
    /// <returns>The first <code>Success</code> for two successes, the first <code>Failure</code> for mixed; otherwise,
    /// a <code>Failure</code> of the concatenation of the failure values.</returns>
    /// <exception cref="ArgumentNullException">if either <see cref="Validation"/> is <code>null</code>.</exception>
    public static IValidation<F, S> Concat<F, S>(IValidation<F, S> second, IValidation<F, S> first)
    {
      RequireNonNull(second, "second validation must not be null");
      RequireNonNull(first, "first validation must not be null");

      IValidation<F, S> result = first;

      if (first.IsSuccess() && second.IsFailure())
      {
        result = second;
      }
      else if (second.IsFailure())
      {
        result = Failure<F, S>(new List<IValidation<F, S>> { first, second }
          .SelectMany(FromFailure<F, S>(Enumerable.Empty<F>()))
        );
      }

      return result;
    }

    /// <summary>
    /// Creates a <code>Failure</code> from an arbitrary value.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying success type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A <code>Failure</code> of the value.</returns>
    public static IValidation<F, S> Failure<F, S>(F value) => new Failure<F, S>(value);

    /// <summary>
    /// Creates a <code>Failure</code> from an arbitrary collection of values.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying success type.</typeparam>
    /// <param name="values">The values.</param>
    /// <returns>A <code>Failure</code> of the values.</returns>
    public static IValidation<F, S> Failure<F, S>(IEnumerable<F> values) => new Failure<F, S>(values);

    /// <summary>
    /// Extracts from a collection of <see cref="Validation"/> all of the <code>Failure</code> elements in extracted
    /// order. The underlying collections are concatenated together.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying success type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Validation"/></param>
    /// <returns>The enumerable of underlying <code>Failure</code> values.</returns>
    public static IEnumerable<F> Failures<F, S>(IEnumerable<IValidation<F, S>> enumerable) =>
      (enumerable ?? Enumerable.Empty<IValidation<F, S>>())
        .Where(IsNotNull)
        .Where(IsFailure)
        .SelectMany(FromFailure<F, S>(Enumerable.Empty<F>()));

    /// <summary>
    /// Curried implementation of <see cref="FromFailure{F, S}(IEnumerable{F}, IValidation{F, S})"/>.
    /// </summary>
    public static Func<IValidation<F, S>, IEnumerable<F>> FromFailure<F, S>(IEnumerable<F> defaultValues) => validation
      => FromFailure(defaultValues, validation);

    /// <summary>
    /// Extracts the value(s) of a <code>Failure</code>; otherwise, returns the <code>defaultValues</code>.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying success type.</typeparam>
    /// <param name="defaultValues">Values used if the <code>validation</code> is not a <code>Failure</code>.</param>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <returns>The underlying failure value(s) or the defaults.</returns>
    public static IEnumerable<F> FromFailure<F, S>(IEnumerable<F> defaultValues, IValidation<F, S> validation)
      => validation is Failure<F, S> failure
        ? failure._values
        : defaultValues;

    /// <summary>
    /// Curried implementation of <see cref="FromSuccess{F, S}(S, IValidation{F, S})"/>.
    /// </summary>
    public static Func<IValidation<F, S>, S> FromSuccess<F, S>(S defaultValue) => validation
      => FromSuccess(defaultValue, validation);

    /// <summary>
    /// Extracts the value of a <code>Success</code>; otherwise, returns the <code>defaultValue</code>.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying success type.</typeparam>
    /// <param name="defaultValue">Value used if the <code>validation</code> is not a <code>Success</code>.</param>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <returns>The underlying success value or the default.</returns>
    public static S FromSuccess<F, S>(S defaultValue, IValidation<F, S> validation)
      => validation is Success<F, S> success
        ? success._value
        : defaultValue;

    /// <summary>
    /// Determines whether or not the instance is a <code>Failure</code>.
    /// </summary>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying succes type.</typeparam>
    /// <returns><code>true</code> for a <code>Failure</code>; otherwise, <code>false</code>.</returns>
    public static bool IsFailure<F, S>(IValidation<F, S> validation) => validation.IsFailure();

    /// <summary>
    /// Determines whether or not the instance is a <code>Success</code>.
    /// </summary>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying succes type.</typeparam>
    /// <returns><code>true</code> for a <code>Success</code>; otherwise, <code>false</code>.</returns>
    public static bool IsSuccess<F, S>(IValidation<F, S> validation) => validation.IsSuccess();

    /// <summary>
    /// Partitions a collection of <see cref="Validation"/> into two collections. All <code>Failure</code> elements are
    /// extracted, in order, to the first position of the output. Similarly for the <code>Success</code> elements in the
    /// second position.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying succes type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Validation"/></param>
    /// <returns>A couple of a collection of the underlying <code>Failure</code> values and a collection of the
    /// underlying <code>Success</code> values.</returns>
    public static (IEnumerable<F>, IEnumerable<S>) PartitionValidations<F, S>(IEnumerable<IValidation<F, S>> enumerable)
      => (Failures(enumerable), Successes(enumerable));

    /// <summary>
    /// Creates a <code>Success</code> from an arbitrary value.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying succes type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A <code>Success</code> of the value.</returns>
    public static IValidation<F, S> Success<F, S>(S value) => new Success<F, S>(value);

    /// <summary>
    /// Extracts from a collection of <see cref="Validation"/> all of the <code>Success</code> elements in extracted
    /// order.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying succes type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Validation"/></param>
    /// <returns>The enumerable of underlying <code>Success</code> values.</returns>
    public static IEnumerable<S> Successes<F, S>(IEnumerable<IValidation<F, S>> enumerable)
       => (enumerable ?? Enumerable.Empty<IValidation<F, S>>())
        .Where(IsNotNull)
        .Where(IsSuccess)
        .Select(FromSuccess<F, S>(default));

    /// <summary>
    /// Curried implementation of <see cref="Validate{F, S}(Predicate{S}, F, S)"/>.
    /// </summary>
    public static Func<F, Func<S, IValidation<F, S>>> Validate<F, S>(Predicate<S> predicate) => failureValue
      => value => Validate(predicate, failureValue)(value);

    /// <summary>
    /// Partially curried implementation of <see cref="Validate{F, S}(Predicate{S}, F, S)"/>.
    /// </summary>
    public static Func<S, IValidation<F, S>> Validate<F, S>(Predicate<S> predicate, F failureValue) => value
      => Validate(predicate, failureValue, value);

    /// <summary>
    /// Validates a value <code>b</code> and a <code>Success</code> of <code>b</code> if the
    /// <code>predicate</code> returns <code>true</code>; otherwise, a <code>Failure</code> of <code>a</code>.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying succes type.</typeparam>
    /// <param name="predicate">The predicate.</param>
    /// <param name="failureValue">The failure value.</param>
    /// <param name="value">The value to test.</param>
    /// <returns>A <code>Success</code> of the <code>value</code> if the <code>predicate</code> returns
    /// <code>true</code>; otherwise, a <code>Failure</code> of <code>failureValue</code>.</returns>
    public static IValidation<F, S> Validate<F, S>(Predicate<S> predicate, F failureValue, S value)
      => RequireNonNull(predicate, "predicate must not be null")(value)
        ? Success<F, S>(value)
        : Failure<F, S>(failureValue);

    /// <summary>
    /// Curried implementation of
    /// <see cref="Validation.ValidationMap{F, S, C}(Func{IEnumerable{F}, C}, Func{S, C}, IValidation{F, S})"/>.
    /// </summary>
    public static Func<Func<S, C>, Func<IValidation<F, S>, C>> ValidationMap<F, S, C>(
      Func<IEnumerable<F>, C> failureMorphism
    ) => successMorphism => ValidationMap(failureMorphism, successMorphism);

    /// <summary>
    /// Partially curried implementation of
    /// <see cref="Validation.ValidationMap{F, S, C}(Func{IEnumerable{F}, C}, Func{S, C}, IValidation{F, S})"/>.
    /// </summary>
    public static Func<IValidation<F, S>, C> ValidationMap<F, S, C>(
      Func<IEnumerable<F>, C> failureMorphism,
      Func<S, C> successMorphism
    ) => validation => ValidationMap(failureMorphism, successMorphism, validation);

    /// <summary>
    /// Provides a catamorphism of the <code>validation</code> to a singular value. If the value is
    /// <code>Failure f</code>, apply the first function to <code>f</code>; otherwise, apply the second function to
    /// <code>s</code>.
    /// </summary>
    /// <typeparam name="F">The underlying failure type.</typeparam>
    /// <typeparam name="S">The underlying succes type.</typeparam>
    /// <typeparam name="C">The return type.</typeparam>
    /// <param name="failureMorphism">Maps the value of a <code>Failure f</code> to <code>c</code>.</param>
    /// <param name="successMorphism">Maps the value of a <code>Success s</code> to <code>c</code>.</param>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <returns>The result of the catamorphism of the <code>validation</code>.</returns>
    /// <exception cref="ArgumentNullException">if the <code>failureMorphism</code>, <code>successMorphism</code>, or
    /// <code>validation</code> is <code>null</code>.</exception>
    /// <see href="https://en.wikipedia.org/wiki/Catamorphism">Catamorphism</see>
    public static C ValidationMap<F, S, C>(
      Func<IEnumerable<F>, C> failureMorphism,
      Func<S, C> successMorphism,
      IValidation<F, S> validation
    ) => RequireNonNull(validation, $"{nameof(validation)} must not be null").IsSuccess()
      ? RequireNonNull(successMorphism, $"{nameof(successMorphism)} must not be null")(FromSuccess(default, validation))
      : RequireNonNull(failureMorphism, $"{nameof(failureMorphism)} must not be null")(FromFailure(
        Enumerable.Empty<F>(),
        validation
      ));
  }

  /// <summary>
  /// Encapsulates the falure value(s) of the disjunction.
  /// </summary>
  /// <typeparam name="A">The underlying failure type.</typeparam>
  /// <typeparam name="B">The underlying success type.</typeparam>
  internal struct Failure<A, B> : IValidation<A, B>
  {
    internal readonly IEnumerable<A> _values;

    internal Failure(A value) => _values = new List<A> { value };
    internal Failure(IEnumerable<A> values) => _values = values ?? Enumerable.Empty<A>();

    public override bool Equals(object obj) => obj is IValidation<A, B> && Equals(obj as IValidation<A, B>);
    public override int GetHashCode() => _values.GetHashCode();
    public override string ToString()
      => $"Failure<{typeof(IEnumerable<A>)}> [{string.Join(",", _values.Select(value => value.ToString()).ToList())}]";

    public bool Equals(IValidation<A, B> other) => other is Failure<A, B> failure && _values.SequenceEqual(failure._values);

    /// <summary>
    /// Determines whether or not the instance is a <code>Failure</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Failure</code>; otherwise, <code>false</code>.</returns>
    public bool IsFailure() => true;

    /// <summary>
    /// Determines whether or not the instance is a <code>Success</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Success</code>; otherwise, <code>false</code>.</returns>
    public bool IsSuccess() => false;
  }

  /// <summary>
  /// Encapsulates the success value of the disjunction.
  /// </summary>
  /// <typeparam name="A">The underlying failure type.</typeparam>
  /// <typeparam name="B">The underlying success type.</typeparam>
  internal struct Success<A, B> : IValidation<A, B>
  {
    internal readonly B _value;

    internal Success(B value) => _value = value;

    public override bool Equals(object obj) => obj is IValidation<A, B> && Equals(obj as IValidation<A, B>);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"Success<{typeof(B)}> {_value}";

    public bool Equals(IValidation<A, B> other) => other is Success<A, B> success && _value.Equals(success._value);

    /// <summary>
    /// Determines whether or not the instance is a <code>Failure</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Failure</code>; otherwise, <code>false</code>.</returns>
    public bool IsFailure() => false;

    /// <summary>
    /// Determines whether or not the instance is a <code>Success</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Success</code>; otherwise, <code>false</code>.</returns>
    public bool IsSuccess() => true;
  }
}
