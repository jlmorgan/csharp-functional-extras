using static FunctionalExtras.Objects;

using System;
using System.Linq;
using System.Collections.Generic;

namespace FunctionalExtras.Data
{
  /// <summary>
  /// The <see cref="Validation"/> type is a right-biased disjunction that represents two possibilities; either a
  /// <code>Invalid</code> of <code>a</code> or a <code>Valid</code> of <code>b</code>. By convention, the
  /// <see cref="Validation"/> is used to represent a value or invalid result of some function or process as a
  /// <code>Invalid</code> of the invalid message or a <code>Valid</code> of the value.
  /// </summary>
  public static class Validation
  {
    /// <summary>
    /// Curried implementation of <see cref="Concat{I, V}(IValidation{I, V}, IValidation{I, V})"/>.
    /// </summary>
    public static Func<IValidation<I, V>, IValidation<I, V>> Concat<I, V>(IValidation<I, V> second) => first
      => Concat(second, first);

    /// <summary>
    /// Concatenates two <code>Invalid</code> values together, replacing a <code>Valid</code> with the
    /// <code>Invalid</code>; otherwise, take the first <code>Valid</code>.
    /// </summary>
    /// <param name="second">The second <see cref="Validation"/>.</param>
    /// <param name="first">The first <see cref="Validation"/>.</param>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <returns>The first <code>Valid</code> for two valids, the first <code>Invalid</code> for mixed; otherwise,
    /// a <code>Invalid</code> of the concatenation of the invalid values.</returns>
    /// <exception cref="ArgumentNullException">if either <see cref="Validation"/> is <code>null</code>.</exception>
    public static IValidation<I, V> Concat<I, V>(IValidation<I, V> second, IValidation<I, V> first)
    {
      RequireNonNull(second, "second validation must not be null");
      RequireNonNull(first, "first validation must not be null");

      IValidation<I, V> result = first;

      if (first.IsValid() && second.IsInvalid())
      {
        result = second;
      }
      else if (second.IsInvalid())
      {
        result = Invalid<I, V>(new List<IValidation<I, V>> { first, second }
          .SelectMany(FromInvalid<I, V>(Enumerable.Empty<I>()))
        );
      }

      return result;
    }

    /// <summary>
    /// Creates a <code>Invalid</code> from an arbitrary value.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A <code>Invalid</code> of the value.</returns>
    public static IValidation<I, V> Invalid<I, V>(I value) => new Invalid<I, V>(value);

    /// <summary>
    /// Creates a <code>Invalid</code> from an arbitrary collection of values.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="values">The values.</param>
    /// <returns>A <code>Invalid</code> of the values.</returns>
    public static IValidation<I, V> Invalid<I, V>(IEnumerable<I> values) => new Invalid<I, V>(values);

    /// <summary>
    /// Extracts from a collection of <see cref="Validation"/> all of the <code>Invalid</code> elements in extracted
    /// order. The underlying collections are concatenated together.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Validation"/></param>
    /// <returns>The enumerable of underlying <code>Invalid</code> values.</returns>
    public static IEnumerable<I> Invalids<I, V>(IEnumerable<IValidation<I, V>> enumerable) =>
      (enumerable ?? Enumerable.Empty<IValidation<I, V>>())
        .Where(IsNotNull)
        .Where(IsInvalid)
        .SelectMany(FromInvalid<I, V>(Enumerable.Empty<I>()));

    /// <summary>
    /// Curried implementation of <see cref="FromInvalid{F, S}(IEnumerable{F}, IValidation{F, S})"/>.
    /// </summary>
    public static Func<IValidation<I, V>, IEnumerable<I>> FromInvalid<I, V>(IEnumerable<I> defaultValues) => validation
      => FromInvalid(defaultValues, validation);

    /// <summary>
    /// Extracts the value(s) of a <code>Invalid</code>; otherwise, returns the <code>defaultValues</code>.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="defaultValues">Values used if the <code>validation</code> is not a <code>Invalid</code>.</param>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <returns>The underlying invalid value(s) or the defaults.</returns>
    public static IEnumerable<I> FromInvalid<I, V>(IEnumerable<I> defaultValues, IValidation<I, V> validation)
      => validation is Invalid<I, V> invalid
        ? invalid._values
        : defaultValues;

    /// <summary>
    /// Curried implementation of <see cref="FromValid{F, S}(S, IValidation{F, S})"/>.
    /// </summary>
    public static Func<IValidation<I, V>, V> FromValid<I, V>(V defaultValue) => validation
      => FromValid(defaultValue, validation);

    /// <summary>
    /// Extracts the value of a <code>Valid</code>; otherwise, returns the <code>defaultValue</code>.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="defaultValue">Value used if the <code>validation</code> is not a <code>Valid</code>.</param>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <returns>The underlying valid value or the default.</returns>
    public static V FromValid<I, V>(V defaultValue, IValidation<I, V> validation)
      => validation is Valid<I, V> valid
        ? valid._value
        : defaultValue;

    /// <summary>
    /// Determines whether or not the instance is a <code>Invalid</code>.
    /// </summary>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <returns><code>true</code> for a <code>Invalid</code>; otherwise, <code>false</code>.</returns>
    public static bool IsInvalid<I, V>(IValidation<I, V> validation) => validation.IsInvalid();

    /// <summary>
    /// Determines whether or not the instance is a <code>Valid</code>.
    /// </summary>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <returns><code>true</code> for a <code>Valid</code>; otherwise, <code>false</code>.</returns>
    public static bool IsValid<I, V>(IValidation<I, V> validation) => validation.IsValid();

    /// <summary>
    /// Partitions a collection of <see cref="Validation"/> into two collections. All <code>Invalid</code> elements are
    /// extracted, in order, to the first position of the output. Similarly for the <code>Valid</code> elements in the
    /// second position.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Validation"/></param>
    /// <returns>A couple of a collection of the underlying <code>Invalid</code> values and a collection of the
    /// underlying <code>Valid</code> values.</returns>
    public static (IEnumerable<I>, IEnumerable<V>) PartitionValidations<I, V>(IEnumerable<IValidation<I, V>> enumerable)
      => (Invalids(enumerable), Valids(enumerable));

    /// <summary>
    /// Creates a <code>Valid</code> from an arbitrary value.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A <code>Valid</code> of the value.</returns>
    public static IValidation<I, V> Valid<I, V>(V value) => new Valid<I, V>(value);

    /// <summary>
    /// Extracts from a collection of <see cref="Validation"/> all of the <code>Valid</code> elements in extracted
    /// order.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="enumerable">The enumerable of <see cref="Validation"/></param>
    /// <returns>The enumerable of underlying <code>Valid</code> values.</returns>
    public static IEnumerable<V> Valids<I, V>(IEnumerable<IValidation<I, V>> enumerable)
       => (enumerable ?? Enumerable.Empty<IValidation<I, V>>())
        .Where(IsNotNull)
        .Where(IsValid)
        .Select(FromValid<I, V>(default));

    /// <summary>
    /// Curried implementation of <see cref="Validate{F, S}(Predicate{S}, F, S)"/>.
    /// </summary>
    public static Func<I, Func<V, IValidation<I, V>>> Validate<I, V>(Predicate<V> predicate) => invalidValue
      => value => Validate(predicate, invalidValue)(value);

    /// <summary>
    /// Partially curried implementation of <see cref="Validate{F, S}(Predicate{S}, F, S)"/>.
    /// </summary>
    public static Func<V, IValidation<I, V>> Validate<I, V>(Predicate<V> predicate, I invalidValue) => value
      => Validate(predicate, invalidValue, value);

    /// <summary>
    /// Validates a value <code>b</code> and a <code>Valid</code> of <code>b</code> if the
    /// <code>predicate</code> returns <code>true</code>; otherwise, a <code>Invalid</code> of <code>a</code>.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <param name="predicate">The predicate.</param>
    /// <param name="invalidValue">The invalid value.</param>
    /// <param name="value">The value to test.</param>
    /// <returns>A <code>Valid</code> of the <code>value</code> if the <code>predicate</code> returns
    /// <code>true</code>; otherwise, a <code>Invalid</code> of <code>invalidValue</code>.</returns>
    public static IValidation<I, V> Validate<I, V>(Predicate<V> predicate, I invalidValue, V value)
      => RequireNonNull(predicate, "predicate must not be null")(value)
        ? Valid<I, V>(value)
        : Invalid<I, V>(invalidValue);

    /// <summary>
    /// Curried implementation of
    /// <see cref="ValidationMap{I, V, C}(Func{IEnumerable{I}, C}, Func{V, C}, IValidation{I, V})"/>.
    /// </summary>
    public static Func<Func<V, C>, Func<IValidation<I, V>, C>> ValidationMap<I, V, C>(
      Func<IEnumerable<I>, C> invalidMorphism
    ) => validMorphism => ValidationMap(invalidMorphism, validMorphism);

    /// <summary>
    /// Partially curried implementation of
    /// <see cref="ValidationMap{I, V, C}(Func{IEnumerable{I}, C}, Func{V, C}, IValidation{I, V})"/>.
    /// </summary>
    public static Func<IValidation<I, V>, C> ValidationMap<I, V, C>(
      Func<IEnumerable<I>, C> invalidMorphism,
      Func<V, C> validMorphism
    ) => validation => ValidationMap(invalidMorphism, validMorphism, validation);

    /// <summary>
    /// Provides a catamorphism of the <code>validation</code> to a singular value. If the value is
    /// <code>Invalid f</code>, apply the first function to <code>f</code>; otherwise, apply the second function to
    /// <code>s</code>.
    /// </summary>
    /// <typeparam name="I">The underlying invalid type.</typeparam>
    /// <typeparam name="V">The underlying valid type.</typeparam>
    /// <typeparam name="C">The return type.</typeparam>
    /// <param name="invalidMorphism">Maps the value of a <code>Invalid f</code> to <code>c</code>.</param>
    /// <param name="validMorphism">Maps the value of a <code>Valid s</code> to <code>c</code>.</param>
    /// <param name="validation">The <see cref="Validation"/>.</param>
    /// <returns>The result of the catamorphism of the <code>validation</code>.</returns>
    /// <exception cref="ArgumentNullException">if the <code>invalidMorphism</code>, <code>validMorphism</code>, or
    /// <code>validation</code> is <code>null</code>.</exception>
    /// <see href="https://en.wikipedia.org/wiki/Catamorphism">Catamorphism</see>
    public static C ValidationMap<I, V, C>(
      Func<IEnumerable<I>, C> invalidMorphism,
      Func<V, C> validMorphism,
      IValidation<I, V> validation
    ) => RequireNonNull(validation, $"{nameof(validation)} must not be null").IsValid()
      ? RequireNonNull(validMorphism, $"{nameof(validMorphism)} must not be null")(FromValid(default, validation))
      : RequireNonNull(invalidMorphism, $"{nameof(invalidMorphism)} must not be null")(FromInvalid(
        Enumerable.Empty<I>(),
        validation
      ));
  }

  /// <summary>
  /// Encapsulates the falure value(s) of the disjunction.
  /// </summary>
  /// <typeparam name="A">The underlying invalid type.</typeparam>
  /// <typeparam name="B">The underlying valid type.</typeparam>
  internal struct Invalid<A, B> : IValidation<A, B>
  {
    internal readonly IEnumerable<A> _values;

    internal Invalid(A value) => _values = new List<A> { value };
    internal Invalid(IEnumerable<A> values) => _values = values ?? Enumerable.Empty<A>();

    public override bool Equals(object obj) => obj is IValidation<A, B> && Equals(obj as IValidation<A, B>);
    public override int GetHashCode() => _values.GetHashCode();
    public override string ToString()
      => $"Invalid<{typeof(IEnumerable<A>)}> [{string.Join(",", _values.Select(value => value.ToString()).ToList())}]";

    public bool Equals(IValidation<A, B> other) => other is Invalid<A, B> invalid && _values.SequenceEqual(invalid._values);

    /// <summary>
    /// Determines whether or not the instance is a <code>Invalid</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Invalid</code>; otherwise, <code>false</code>.</returns>
    public bool IsInvalid() => true;

    /// <summary>
    /// Determines whether or not the instance is a <code>Valid</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Valid</code>; otherwise, <code>false</code>.</returns>
    public bool IsValid() => false;
  }

  /// <summary>
  /// Encapsulates the valid value of the disjunction.
  /// </summary>
  /// <typeparam name="A">The underlying invalid type.</typeparam>
  /// <typeparam name="B">The underlying valid type.</typeparam>
  internal struct Valid<A, B> : IValidation<A, B>
  {
    internal readonly B _value;

    internal Valid(B value) => _value = value;

    public override bool Equals(object obj) => obj is IValidation<A, B> && Equals(obj as IValidation<A, B>);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"Valid<{typeof(B)}> {_value}";

    public bool Equals(IValidation<A, B> other) => other is Valid<A, B> valid && _value.Equals(valid._value);

    /// <summary>
    /// Determines whether or not the instance is a <code>Invalid</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Invalid</code>; otherwise, <code>false</code>.</returns>
    public bool IsInvalid() => false;

    /// <summary>
    /// Determines whether or not the instance is a <code>Valid</code>.
    /// </summary>
    /// <returns><code>true</code> if <code>Valid</code>; otherwise, <code>false</code>.</returns>
    public bool IsValid() => true;
  }
}
