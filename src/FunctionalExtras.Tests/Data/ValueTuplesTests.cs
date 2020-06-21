using System;
using Xunit;

using static FunctionalExtras.Data.ValueTuples;

namespace FunctionalExtras.Tests.Data
{
  public static class ValueTuplesTests
  {
    private static readonly Guid _testFirstValue = Guid.NewGuid();
    private static readonly Guid _testSecondValue = Guid.NewGuid();

    public class DescribeOf
    {
      [Fact]
      public void ShouldReturnTuple()
      {
        (Guid, Guid) expectedResult = (_testFirstValue, _testSecondValue);
        (Guid, Guid) actualResult = Of<Guid, Guid>(_testFirstValue)(_testSecondValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class TupleMap
    {
      private static Func<int> _randomInt = () => new Random().Next(0, 9);

      [Fact]
      public void ShouldThrowAnExceptionForNullFirstMorphism()
      {
        Func<int, int> testFirstMorphism = null;
        Func<int, int> testSecondMorphism = value => value + 2;
        int testValue = _randomInt();

        Assert.Throws<ArgumentNullException>(() => TupleMap<int, int, int>(testFirstMorphism)
          (testSecondMorphism)
          (testValue)
        );
      }

      [Fact]
      public void ShouldThrowAnExceptionForNullSecondMorphism()
      {
        Func<int, int> testFirstMorphism = value => value + 1;
        Func<int, int> testSecondMorphism = null;
        int testValue = _randomInt();

        Assert.Throws<ArgumentNullException>(() => TupleMap<int, int, int>(testFirstMorphism)
          (testSecondMorphism)
          (testValue)
        );
      }

      [Fact]
      public void ShouldReturnTupleOfMappedValues()
      {
        Func<int, int> testFirstMorphism = value => value + 1;
        Func<int, int> testSecondMorphism = value => value + 2;
        int testValue = _randomInt();
        (int, int) expectedResult = (testValue + 1, testValue + 2);
        (int, int) actualResult = TupleMap<int, int, int>(testFirstMorphism)(testSecondMorphism)(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }
  }
}
