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
  }
}
