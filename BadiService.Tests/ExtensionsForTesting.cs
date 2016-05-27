using System;
using NUnit.Framework;

namespace BadiService.Tests
{
  public static class ExtensionsForTesting
  {
    /// <summary>
    ///   Assert that the value is as expected
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    public static void ShouldBe<T>(this T actual, T expected)
    {
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///   Compare just the day, ignoring any time
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    public static void ShouldBeSameDayAs(this DateTime actual, DateTime expected)
    {
      Assert.AreEqual(expected.ToShortDateString(), actual.ToShortDateString());
    }
  }
}