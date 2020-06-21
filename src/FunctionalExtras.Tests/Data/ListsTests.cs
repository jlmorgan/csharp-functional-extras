using static FunctionalExtras.Data.Lists;

using Extensions;
using FunctionalExtras.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunctionalExtras.Tests.Data
{
  public static class ListsTests
  {
    private static Func<int> _randomInt = () => new Random().Next(0, 9);

    public class DescribeAppend
    {
      [Fact]
      public void ShouldReturnSecondListForNullFirst()
      {
        List<Guid> testSecond = new List<Guid>
        {
          Guid.NewGuid()
        };
        List<Guid> testFirst = null;
        List<Guid> expectedResult = testSecond;
        List<Guid> actualResult = Append(testSecond)(testFirst);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnFirstListForNullSecond()
      {
        List<Guid> testSecond = null;
        List<Guid> testFirst = new List<Guid>
        {
          Guid.NewGuid()
        };
        List<Guid> expectedResult = testFirst;
        List<Guid> actualResult = Append(testSecond)(testFirst);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnEmptyListForEmptyLists()
      {
        List<Guid> testSecond = new List<Guid>();
        List<Guid> testFirst = new List<Guid>();
        List<Guid> expectedResult = new List<Guid>();
        List<Guid> actualResult = Append(testSecond)(testFirst);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnAppendedLists()
      {
        Guid testValue2 = Guid.NewGuid();
        Guid testValue1 = Guid.NewGuid();
        List<Guid> testSecond = new List<Guid>
        {
          testValue2
        };
        List<Guid> testFirst = new List<Guid>
        {
          testValue1
        };
        List<Guid> expectedResult = new List<Guid>
        {
          testValue1,
          testValue2
        };
        List<Guid> actualResult = Append(testSecond)(testFirst);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeEmpty
    {
      [Fact]
      public void ShouldReturnEmptyList()
      {
        List<Guid> expectedResult = new List<Guid>();
        List<Guid> actualResult = Empty<Guid>();

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeFoldLeft
    {
      private static Func<int, int, int> _testFold = (a, b) => a + b;
      private static int _testInitialValue = 0;

      [Fact]
      public void ShouldReturnInitialValueForNullList()
      {
        List<int> testList = null;
        int expectedResult = _testInitialValue;
        int actualResult = FoldLeft(_testFold)(_testInitialValue)(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnInitialValueForEmptyList()
      {
        List<int> testList = new List<int>();
        int expectedResult = _testInitialValue;
        int actualResult = FoldLeft(_testFold)(_testInitialValue)(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnAccumulatedValueForList()
      {
        int testValue1 = _randomInt();
        int testValue2 = _randomInt();
        int testValue3 = _randomInt();
        List<int> testList = new List<int>
        {
          testValue1,
          testValue2,
          testValue3
        };
        int expectedResult = _testInitialValue + testValue1 + testValue2 + testValue3;
        int actualResult = FoldLeft(_testFold)(_testInitialValue)(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeFoldRight
    {
      private static Func<int, int, int> _testFold = (a, b) => a + b;
      private static int _testInitialValue = 0;

      [Fact]
      public void ShouldReturnInitialValueForNullList()
      {
        List<int> testList = null;
        int expectedResult = _testInitialValue;
        int actualResult = FoldRight(_testFold)(_testInitialValue)(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnInitialValueForEmptyList()
      {
        List<int> testList = new List<int>();
        int expectedResult = _testInitialValue;
        int actualResult = FoldRight(_testFold)(_testInitialValue)(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnAccumulatedValueForList()
      {
        
        int testValue1 = _randomInt();
        int testValue2 = _randomInt();
        int testValue3 = _randomInt();
        List<int> testList = new List<int>
        {
          testValue1,
          testValue2,
          testValue3
        };
        int expectedResult = _testInitialValue + testValue1 + testValue2 + testValue3;
        int actualResult = FoldRight(_testFold)(_testInitialValue)(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeHead
    {
      [Fact]
      public void ShouldThrowExceptionForNullList()
      {
        List<object> testList = null;

        Assert.Throws<ArgumentException>(() => Head(testList));
      }

      [Fact]
      public void ShouldThrowExceptionForEmptyList()
      {
        List<object> testList = new List<object>();

        Assert.Throws<ArgumentException>(() => Head(testList));
      }

      [Fact]
      public void ShouldReturnFirstElementForNonEmptyList()
      {
        int testValue1 = _randomInt();
        int testValue2 = _randomInt();
        int testValue3 = _randomInt();
        List<int> testList = new List<int>
        {
          testValue1,
          testValue2,
          testValue3
        };
        int expectedResult = testValue1;
        int actualResult = Head(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeInit
    {
      [Fact]
      public void ShouldThrowExceptionForNullList()
      {
        List<object> testList = null;

        Assert.Throws<ArgumentException>(() => Init(testList));
      }

      [Fact]
      public void ShouldThrowExceptionForEmptyList()
      {
        List<object> testList = new List<object>();

        Assert.Throws<ArgumentException>(() => Init(testList));
      }

      [Fact]
      public void ShouldReturnAllElementsExcludingLastForNonEmptyList()
      {
        int testValue1 = _randomInt();
        int testValue2 = _randomInt();
        int testValue3 = _randomInt();
        List<int> testList = new List<int>
        {
          testValue1,
          testValue2,
          testValue3
        };
        List<int> expectedResult = new List<int>
        {
          testValue1,
          testValue2
        };
        List<int> actualResult = Init(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeIsEmpty
    {
      [Fact]
      public void ShouldReturnTrueForNullList()
      {
        List<object> testList = null;
        bool actualResult = IsEmpty(testList);

        Assert.True(actualResult);
      }

      [Fact]
      public void ShouldReturnTrueForEmptyList()
      {
        List<object> testList = new List<object>();
        bool actualResult = IsEmpty(testList);

        Assert.True(actualResult);
      }

      [Fact]
      public void ShouldReturnFalseForBlankList()
      {
        List<object> testList = new List<object>
        {
          null,
          null
        };
        bool actualResult = IsEmpty(testList);

        Assert.False(actualResult);
      }

      [Fact]
      public void ShouldReturnFalseForNonEmptyList()
      {
        List<Guid> testList = new List<Guid>
        {
          Guid.NewGuid(),
          Guid.NewGuid(),
          Guid.NewGuid()
        };
        bool actualResult = IsEmpty(testList);

        Assert.False(actualResult);
      }
    }

    public class DescribeIsNotEmpty
    {
      [Fact]
      public void ShouldReturnFalseForNullList()
      {
        List<object> testList = null;
        bool actualResult = IsNotEmpty(testList);

        Assert.False(actualResult);
      }

      [Fact]
      public void ShouldReturnFalseForEmptyList()
      {
        List<object> testList = new List<object>();
        bool actualResult = IsNotEmpty(testList);

        Assert.False(actualResult);
      }

      [Fact]
      public void ShouldReturnTrueForBlankList()
      {
        List<object> testList = new List<object>
        {
          null,
          null
        };
        bool actualResult = IsNotEmpty(testList);

        Assert.True(actualResult);
      }

      [Fact]
      public void ShouldReturnTrueForNonEmptyList()
      {
        List<Guid> testList = new List<Guid>
        {
          Guid.NewGuid(),
          Guid.NewGuid(),
          Guid.NewGuid()
        };
        bool actualResult = IsNotEmpty(testList);

        Assert.True(actualResult);
      }
    }

    public class DescribeLast
    {
      [Fact]
      public void ShouldThrowExceptionForNullList()
      {
        List<object> testList = null;

        Assert.Throws<ArgumentException>(() => Last(testList));
      }

      [Fact]
      public void ShouldThrowExceptionForEmptyList()
      {
        List<object> testList = new List<object>();

        Assert.Throws<ArgumentException>(() => Last(testList));
      }

      [Fact]
      public void ShouldReturnLastElementForNonEmptyList()
      {
        int testValue1 = _randomInt();
        int testValue2 = _randomInt();
        int testValue3 = _randomInt();
        List<int> testList = new List<int>
        {
          testValue1,
          testValue2,
          testValue3
        };
        int expectedResult = testValue3;
        int actualResult = Last(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeLength
    {
      [Fact]
      public void ShouldReturn0ForNullList()
      {
        List<object> testList = null;
        int expectedResult = 0;
        int actualResult = Length(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturn0ForEmptyList()
      {
        List<object> testList = new List<object>();
        int expectedResult = 0;
        int actualResult = Length(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnLengthOfList()
      {
        int testLength = _randomInt();
        List<object> testList = Enumerable.Repeat<object>(null, testLength).ToList();
        int expectedResult = testLength;
        int actualResult = Length(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeMap
    {
      [Fact]
      public void ShouldThrowExceptionForNullMorphism()
      {
        List<Guid> testList = new List<Guid>();
        Func<Guid, string> testMorphim = null;

        Assert.Throws<ArgumentNullException>(() => Map(testMorphim)(testList));
      }

      [Fact]
      public void ShouldReturnEmptyListForNullList()
      {
        List<Guid> testList = null;
        Func<Guid, string> testMorphim = guid => guid.ToString();
        List<string> expectedResult = new List<string>();
        List<string> actualResult = Map(testMorphim)(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnMappedList()
      {
        int testValue1 = _randomInt();
        int testValue2 = _randomInt();
        int testValue3 = _randomInt();
        Func<int, int> testMorphism = value => value + 1;
        List<int> testList = new List<int>
        {
          testValue1,
          testValue2,
          testValue3
        };
        List<int> expectedResult = new List<int>
        {
          testValue1 + 1,
          testValue2 + 1,
          testValue3 + 1
        };
        List<int> actualResult = Map(testMorphism)(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeTail
    {
      [Fact]
      public void ShouldThrowExceptionForNullList()
      {
        List<object> testList = null;

        Assert.Throws<ArgumentException>(() => Tail(testList));
      }

      [Fact]
      public void ShouldThrowExceptionForEmptyList()
      {
        List<object> testList = new List<object>();

        Assert.Throws<ArgumentException>(() => Tail(testList));
      }

      [Fact]
      public void ShouldReturnAllElementsExcludingFirstForNonEmptyList()
      {
        int testValue1 = _randomInt();
        int testValue2 = _randomInt();
        int testValue3 = _randomInt();
        List<int> testList = new List<int>
        {
          testValue1,
          testValue2,
          testValue3
        };
        List<int> expectedResult = new List<int>
        {
          testValue2,
          testValue3
        };
        List<int> actualResult = Tail(testList);

        Assert.Equal(expectedResult, actualResult);
      }
    }

    public class DescribeUncons
    {
      [Fact]
      public void ShouldNothingForNullList()
      {
        List<Guid> testList = null;
        IMaybe<(Guid, List<Guid>)> expectedResult = Maybe.Nothing<(Guid, List<Guid>)>();
        IMaybe<(Guid, List<Guid>)> actualResult = Uncons(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldNothingForEmptyList()
      {
        List<Guid> testList = new List<Guid>();
        IMaybe<(Guid, List<Guid>)> expectedResult = Maybe.Nothing<(Guid, List<Guid>)>();
        IMaybe<(Guid, List<Guid>)> actualResult = Uncons(testList);

        Assert.Equal(expectedResult, actualResult);
      }

      [Fact]
      public void ShouldReturnJustOfTupleOfHeadAndTailOfList()
      {
        Guid testValue1 = Guid.NewGuid();
        Guid testValue2 = Guid.NewGuid();
        Guid testValue3 = Guid.NewGuid();
        List<Guid> testList = new List<Guid>
        {
          testValue1,
          testValue2,
          testValue3
        };
        IMaybe<(Guid, List<Guid>)> expectedResult = Maybe.Just<(Guid, List<Guid>)>((
          testValue1,
          new List<Guid>
          {
            testValue2,
            testValue3
          }
        ));
        IMaybe<(Guid, List<Guid>)> actualResult = Uncons(testList);

        // Doing multiple asserts since Lists do not handle equality by sequence by default.
        Assert.Equal(Maybe.FromJust(expectedResult).First(), Maybe.FromJust(actualResult).First());
        Assert.Equal(Maybe.FromJust(expectedResult).Second(), Maybe.FromJust(actualResult).Second());
      }
    }
  }
}
