

using System;

namespace Between
{
  /// <summary>
  /// Defines the boundary inclusion mode for range comparisons.
  /// </summary>
  public enum BetweenBounds
  {
    /// <summary>
    /// Both lower and upper bounds are included in the range (>=, <=).
    /// This is the default behavior.
    /// </summary>
    Inclusive = 0,

    /// <summary>
    /// Both lower and upper bounds are excluded from the range (>, <).
    /// </summary>
    Exclusive = 1,

    /// <summary>
    /// Only the lower bound is excluded (>, <=).
    /// </summary>
    ExcludeLower = 2,

    /// <summary>
    /// Only the upper bound is excluded (>=, <).
    /// </summary>
    ExcludeUpper = 3
  }

  /// <summary>
  /// Provides extension methods to check if a value is between two bounds.
  /// <para>
  /// This is <b>syntactic sugar</b> that makes range comparisons more readable and expressive,
  /// inspired by SQL's BETWEEN operator. It compiles to the same efficient code as manual comparisons.
  /// </para>
  /// </summary>
  /// <remarks>
  /// Example: Instead of <c>value >= min &amp;&amp; value &lt;= max</c>, 
  /// you can write <c>value.Between(min, max)</c> for clearer intent.
  /// </remarks>
  public static class BetweenExtensions
  {
    /// <summary>
    /// Determines if a value is between two specified bounds.
    /// <para>
    /// This is <b>syntactic sugar</b> for <c>value.CompareTo(min) &gt;= 0 &amp;&amp; value.CompareTo(max) &lt;= 0</c>
    /// (or variations based on the bounds parameter). It provides a more readable and expressive syntax.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of values to compare. Must implement IComparable&lt;T&gt;.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="min">The lower bound.</param>
    /// <param name="max">The upper bound.</param>
    /// <param name="bounds">
    /// Specifies how the bounds are compared:
    /// - <see cref="BetweenBounds.Inclusive"/>: Includes both bounds (>=, <=).
    /// - <see cref="BetweenBounds.Exclusive"/>: Excludes both bounds (>, &lt;).
    /// - <see cref="BetweenBounds.ExcludeLower"/>: Excludes lower bound only (>, <=).
    /// - <see cref="BetweenBounds.ExcludeUpper"/>: Excludes upper bound only (>=, &lt;).
    /// </param>
    /// <returns>
    /// <c>true</c> if the value is within the bounds according to the specified mode;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="value"/>, <paramref name="min"/>, or <paramref name="max"/> is <c>null</c>.
    /// </exception>
    /// <example>
    /// <code>
    /// int number = 5;
    /// bool result = number.Between(1, 10, BetweenBounds.Inclusive); // true
    /// bool result2 = number.Between(5, 10, BetweenBounds.Exclusive); // false
    /// bool result3 = number.Between(5, 10, BetweenBounds.ExcludeLower); // false
    /// bool result4 = number.Between(5, 10, BetweenBounds.ExcludeUpper); // true
    /// </code>
    /// </example>
    public static bool Between<T>(this T value, T min, T max, BetweenBounds bounds=BetweenBounds.Inclusive) where T : IComparable<T>
    {
      if (value == null) throw new ArgumentNullException(nameof(value));
      if (min == null) throw new ArgumentNullException(nameof(min));
      if (max == null) throw new ArgumentNullException(nameof(max));

      int minComparison = value.CompareTo(min);
      int maxComparison = value.CompareTo(max);

      switch (bounds)
      {
        case BetweenBounds.Inclusive:
          return minComparison >= 0 && maxComparison <= 0;

        case BetweenBounds.Exclusive:
          return minComparison > 0 && maxComparison < 0;

        case BetweenBounds.ExcludeLower:
          return minComparison > 0 && maxComparison <= 0;

        case BetweenBounds.ExcludeUpper:
          return minComparison >= 0 && maxComparison < 0;

        default:
          return minComparison >= 0 && maxComparison <= 0;
      }
    }

  
  }
}

