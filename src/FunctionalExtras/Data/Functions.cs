using System;

namespace FunctionalExtras.Data
{
  public static class Functions
  {
    /// <summary>
    /// Curried implementation of <see cref="Compose{A, B, C}(Func{B, C}, Func{A, B})"/>.
    /// </summary>
    /// <param name="g">The second function.</param>
    /// <typeparam name="A">Input type to the first function (<code>f</code>) in the composition.</typeparam>
    /// <typeparam name="B">Output type of the first function (<code>f</code>) and input type to the second
    /// (<code>g</code>).</typeparam>
    /// <typeparam name="C">Output type of the second function (<code>g</code>).</typeparam>
    /// <returns>A function that maps a value of type <code>A</code> to type <code>C</code>.</returns>
    public static Func<Func<A, B>, Func<A, C>> Compose<A, B, C>(Func<B, C> g) => f => Compose(g, f);

    /// <summary>
    /// Composes two functions <code>g</code> after <code>f</code>.
    /// </summary>
    /// <param name="g">The second function.</param>
    /// <param name="f">The first function.</param>
    /// <typeparam name="A">Input type to the first function (<code>f</code>) in the composition.</typeparam>
    /// <typeparam name="B">Output type of the first function (<code>f</code>) and input type to the second
    /// (<code>g</code>).</typeparam>
    /// <typeparam name="C">Output type of the second function (<code>g</code>).</typeparam>
    /// <returns>A function that maps a value of type <code>A</code> to type <code>C</code>.</returns>
    public static Func<A, C> Compose<A, B, C>(Func<B, C> g, Func<A, B> f) => x => g(f(x));

    /// <summary>
    /// The <code>K</code> combinator. Creates a unary function that ignores the input value and returns the original
    /// value.
    /// </summary>
    /// <param name="a">The return value of the unary function.</param>
    /// <typeparam name="A">The constant type parameter.</typeparam>
    /// <typeparam name="B">The ignored type parameter.</typeparam>
    /// <returns>A unary function that takes a value of type <code>B</code> and returns the original value of
    /// type <code>A</code>.</returns>
    public static Func<B, A> Const<B, A>(A a) => b => a;

    /// <summary>
    /// Flips the argument order of the specified curried <code>function</code>.
    /// </summary>
    /// <param name="f">The curried function to flip.</param>
    /// <typeparam name="A">Type of the first argument.</typeparam>
    /// <typeparam name="B">Type of the second argument.</typeparam>
    /// <typeparam name="C">Return type of the function (<code>f</code>).</typeparam>
    /// <returns>The flipped <code>function</code>.</returns>
    public static Func<B, Func<A, C>> Flip<A, B, C>(Func<A, Func<B, C>> f) => b => a => f(a)(b);

    /// <summary>
    /// Flips the argument order of the specified bi-<code>function</code>.
    /// </summary>
    /// <param name="f">The bi-function to flip.</param>
    /// <typeparam name="A">Type of the first argument.</typeparam>
    /// <typeparam name="B">Type of the second argument.</typeparam>
    /// <typeparam name="C">Return type of the function (<code>f</code>).</typeparam>
    /// <returns>The flipped <code>bi-function</code>.</returns>
    public static Func<B, A, C> Flip<A, B, C>(Func<A, B, C> f) => (b, a) => f(a, b);

    /// <summary>
    /// The <code>I</code> combinator or identity morphism.
    /// </summary>
    /// <returns>The input value.</returns>
    /// <param name="a">The input value.</param>
    /// <typeparam name="A">Type of the argument.</typeparam>
    public static A Id<A>(A a) => a;

    /// <summary>
    /// Curried implementation of <see cref="Pipe{A, B, C}(Func{A, B}, Func{B, C})"/>.
    /// </summary>
    /// <param name="f">The first function.</param>
    /// <typeparam name="A">Input type to the first function (<code>f</code>) in the composition.</typeparam>
    /// <typeparam name="B">Output type of the first function (<code>f</code>) and input type to the second
    /// (<code>g</code>).</typeparam>
    /// <typeparam name="C">Output type of the second function (<code>g</code>).</typeparam>
    /// <returns>A function that maps a value of type <code>A</code> to type <code>C</code>.</returns>
    public static Func<Func<B, C>, Func<A, C>> Pipe<A, B, C>(Func<A, B> f) => g => Compose(g, f);

    /// <summary>
    /// Composes two functions <code>f</code> before <code>g</code>.
    /// </summary>
    /// <param name="f">The first function.</param>
    /// <param name="g">The second function.</param>
    /// <typeparam name="A">Input type to the first function (<code>f</code>) in the composition.</typeparam>
    /// <typeparam name="B">Output type of the first function (<code>f</code>) and input type to the second
    /// (<code>g</code>).</typeparam>
    /// <typeparam name="C">Output type of the second function (<code>g</code>).</typeparam>
    /// <returns>A function that maps a value of type <code>A</code> to type <code>C</code>.</returns>
    public static Func<A, C> Pipe<A, B, C>(Func<A, B> f, Func<B, C> g) => Compose(g, f);
  }
}
