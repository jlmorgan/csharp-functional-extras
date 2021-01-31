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

    public class DescribeCurriedAp
    {
      [Fact]
      public void ShouldApplyTheValueToTheSequence()
      {
        int testValue = 10;
        Func<int, int> testAToB = value => value ^ 2;
        Func<int, int, int> testAAndBToC = (a, b) => a - b;
        int expectedResult = testValue - (testValue ^ 2);
        int actualResult = Ap(testAAndBToC)(testAToB)(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeAp
    {
      [Fact]
      public void ShouldApplyTheValueToTheSequence()
      {
        int testValue = 10;
        Func<int, int> testAToB = value => value ^ 2;
        Func<int, int, int> testAAndBToC = (a, b) => a - b;
        int expectedResult = testValue - (testValue ^ 2);
        int actualResult = Ap(testAAndBToC, testAToB)(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeCurriedBind
    {
      [Fact]
      public void ShouldApplyTheValueToTheSequence()
      {
        int testValue = 10;
        Func<int, int> testAToB = value => value ^ 2;
        Func<int, int, int> testBAndAToC = (b, a) => b - a;
        int expectedResult = (testValue ^ 2) - testValue;
        int actualResult = Bind(testBAndAToC)(testAToB)(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeBind
    {
      [Fact]
      public void ShouldApplyTheValueToTheSequence()
      {
        int testValue = 10;
        Func<int, int> testAToB = value => value ^ 2;
        Func<int, int, int> testBAndAToC = (b, a) => b - a;
        int expectedResult = (testValue ^ 2) - testValue;
        int actualResult = Bind(testBAndAToC, testAToB)(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeCurriedCompose
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

    public class DescribeCurriedFMap
    {
      [Fact]
      public void ShouldConvertTypeAToTypeC()
      {
        string expectedResult = _testBToC(_testAToB(_testInputA));
        string actualResult = FMap<bool, int, string>(_testBToC)(_testAToB)(_testInputA);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeFMap
    {
      [Fact]
      public void ShouldConvertTypeAToTypeC()
      {
        string expectedResult = _testBToC(_testAToB(_testInputA));
        string actualResult = FMap(_testBToC, _testAToB)(_testInputA);

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

    public class DescribeCurriedLiftA2
    {
      [Fact]
      public void ShouldApplyTheValueToTheSequence()
      {
        int testValue = 10;
        Func<int, int> testAToB = value => value ^ 2;
        Func<int, int> testAToC = value => value / 2;
        Func<int, int, int> testBAndCToD = (a, b) => a - b;
        int expectedResult = (testValue ^ 2) - (testValue / 2);
        int actualResult = LiftA2<int, int, int, int>(testBAndCToD)(testAToC)(testAToB)(testValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeLiftA2
    {
      [Fact]
      public void ShouldApplyTheValueToTheSequence()
      {
        int testValue = 10;
        Func<int, int> testAToB = value => value ^ 2;
        Func<int, int> testAToC = value => value / 2;
        Func<int, int, int> testBAndCToD = (a, b) => a - b;
        int expectedResult = (testValue ^ 2) - (testValue / 2);
        int actualResult = LiftA2(testBAndCToD, testAToC, testAToB)(testValue);

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

    public class DescribePure
    {
      [Fact]
      public void ShouldIgnoreInputValue()
      {
        Guid testValue = Guid.NewGuid();
        string testSecondValue = "test";
        Func<string, Guid> testFunction = Pure<string, Guid>(testValue);
        Guid expectedResult = testValue;
        Guid actualResult = testFunction(testSecondValue);

        Assert.Equal(expectedResult, actualResult);
      }
    }
  }
}
