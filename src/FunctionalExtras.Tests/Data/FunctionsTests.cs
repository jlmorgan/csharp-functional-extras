using static FunctionalExtras.Data.Functions;

using System;
using Xunit;

namespace FunctionalExtras.Tests.Data
{
  public static class FunctionsTests
  {
    private static readonly bool _testInputA = true;
    private static readonly Func<bool, int> _testAToB = value => value ? 1 : 0;
    private static readonly Func<int, string> _testBToC = value => value == 1 ? "one" : "zero";

    public class DescribeBind
    {
      [Fact]
      public void ShouldApplyTheValueToTheSequence()
      {
        Func<int, int> testAToB = value => value + 1;
        Func<int, int, int> testBAndAToC = (a, b) => a + b;
        int testValue = 10;
        int expectedResult = testValue + testValue + 1;
        int actualResult = Bind(testBAndAToC)(testAToB)(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeComposeCurried
    {
      [Fact]
      public void ShouldConvertTypeAToTypeC()
      {
        string expectedResult = _testBToC(_testAToB(_testInputA));
        string actualResult = Compose<bool, int, string>(_testBToC)(_testAToB)(_testInputA);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeCompose
    {
      [Fact]
      public void ShouldConvertTypeAToTypeC()
      {
        string expectedResult = _testBToC(_testAToB(_testInputA));
        string actualResult = Compose(_testBToC, _testAToB)(_testInputA);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeConst
    {
      [Fact]
      public void ShouldIgnoreInputValue()
      {
        Guid testValue = Guid.NewGuid();
        string testSecondValue = "test";
        Func<string, Guid> testFunction = Const<string, Guid>(testValue);
        Guid expectedResult = testValue;
        Guid actualResult = testFunction(testSecondValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeFlip
    {
      [Fact]
      public void ShouldFlipArgumentsForBiFunction()
      {
        int testA = 2;
        int testB = 4;
				int testFunction(int a, int b) => a - b;
				int expectedResult = testFunction(testA, testB);
        int actualResult = Flip<int, int, int>(testFunction)(testB, testA);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldFlipArgumentsForFunction()
      {
        int testA = 2;
        int testB = 4;
				Func<int, int> testFunction(int a) => b => a - b;
				int expectedResult = testFunction(testA)(testB);
        int actualResult = Flip<int, int, int>(testFunction)(testB)(testA);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeId
    {
      [Fact]
      public void ShouldReturnValue()
      {
        Guid testValue = Guid.NewGuid();
        Guid expectedResult = testValue;
        Guid actualResult = Id(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeCurriedPipe
    {
      [Fact]
      public void ShouldConvertTypeAToTypeC()
      {
        string expectedResult = _testBToC(_testAToB(_testInputA));
        string actualResult = Pipe<bool, int, string>(_testAToB)(_testBToC)(_testInputA);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribePipe
    {
      [Fact]
      public void ShouldConvertTypeAToTypeC()
      {
        string expectedResult = _testBToC(_testAToB(_testInputA));
        string actualResult = Pipe(_testAToB, _testBToC)(_testInputA);

        Assert.Equal(expectedResult, actualResult);
      }
    }
  }
}
