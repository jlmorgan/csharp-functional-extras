using Extensions;
using System;
using Xunit;

namespace FunctionalExtras.Tests.Data
{
  public static class ValueTupleExtensionsTests
  {
    private static readonly Guid _testFirstValue = Guid.NewGuid();
    private static readonly Guid _testSecondValue = Guid.NewGuid();

    public class DescribeFirst
    {
      [Fact]
      public void ShouldReturnFirstElement()
      {
        (Guid, Guid) testTuple = (_testFirstValue, _testSecondValue);
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
        (Guid, Guid) testTuple = (_testFirstValue, _testSecondValue);
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
        (Guid, Guid) testTuple = (_testFirstValue, _testSecondValue);
        (Guid, Guid) expectedResult = (_testSecondValue, _testFirstValue);
        (Guid, Guid) actualResult = testTuple.Swap();

        Assert.Equal(expectedResult, actualResult);
      }
    }
  }
}
