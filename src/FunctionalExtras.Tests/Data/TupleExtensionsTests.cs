using Extensions;
using System;
using Xunit;

namespace FunctionalExtras.Tests.Data
{
  public static class TupleExtensionsTests
  {
    private static readonly Guid _testFirstValue = Guid.NewGuid();
    private static readonly Guid _testSecondValue = Guid.NewGuid();

    public class DescribeFirst
    {
      [Fact]
      public void ShouldReturnFirstElement()
      {
        Tuple<Guid, Guid> testTuple = Tuple.Create(_testFirstValue, _testSecondValue);
        Guid expectedResult = _testFirstValue;
        Guid actualResult = testTuple.First();

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeSecond
    {
      [Fact]
      public void ShouldReturnSecondElement()
      {
        Tuple<Guid, Guid> testTuple = Tuple.Create(_testFirstValue, _testSecondValue);
        Guid expectedResult = _testSecondValue;
        Guid actualResult = testTuple.Second();

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeSwap
    {
      [Fact]
      public void ShouldReturnSwappedTuple()
      {
        Tuple<Guid, Guid> testTuple = Tuple.Create(_testFirstValue, _testSecondValue);
        Tuple<Guid, Guid> expectedResult = Tuple.Create(_testSecondValue, _testFirstValue);
        Tuple<Guid, Guid> actualResult = testTuple.Swap();

        Assert.Equal(expectedResult, actualResult);
      }
    }
  }
}
