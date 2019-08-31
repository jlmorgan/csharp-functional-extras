using System;
using System.Collections.Generic;
using Xunit;

using FunctionalExtras.Data;
using System.Linq;

namespace FunctionalExtras.Tests.Data
{
  public static class EitherTests
  {
    public class DescribeStaticMethods
    {
      public class DescribeEitherMap
      {
        private static readonly Func<ArgumentException, string> _testLeftMorphism = exception => exception.Message;
        private static readonly Func<Guid, string> _testRightMorphism = guid => guid.ToString();

        [Fact]
        public void ShouldThrowExceptionForNullLeftMorphism()
        {
          Func<ArgumentException, string> testLeftMorphism = null;
          IEither<ArgumentException, Guid> testEither = Either.Left<ArgumentException, Guid>(new ArgumentException());

          Assert.Throws<ArgumentNullException>(() => Either.EitherMap<ArgumentException, Guid, string>(testLeftMorphism)
            (_testRightMorphism)
            (testEither)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullRightMorphism()
        {
          Func<Guid, string> testRightMorphism = null;
          IEither<ArgumentException, Guid> testEither = Either.Right<ArgumentException, Guid>(Guid.NewGuid());

          Assert.Throws<ArgumentNullException>(() => Either.EitherMap<ArgumentException, Guid, string>(_testLeftMorphism)
            (testRightMorphism)
            (testEither)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullRightEither()
        {
          IEither<ArgumentException, Guid> testEither = null;

          Assert.Throws<ArgumentNullException>(() => Either.EitherMap<ArgumentException, Guid, string>(_testLeftMorphism)
            (_testRightMorphism)
            (testEither)
          );
        }

        [Fact]
        public void ShouldReturnMappedValueForLeft()
        {
          ArgumentException testLeftValue = new ArgumentException(Guid.NewGuid().ToString());
          IEither<ArgumentException, Guid> testEither = Either.Left<ArgumentException, Guid>(testLeftValue);
          string expectedResult = testLeftValue.Message;
          string actualResult = Either.EitherMap(_testLeftMorphism, _testRightMorphism, testEither);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnMappedValueForRight()
        {
          Guid testRightValue = Guid.NewGuid();
          IEither<ArgumentException, Guid> testEither = Either.Right<ArgumentException, Guid>(testRightValue);
          string expectedResult = testRightValue.ToString();
          string actualResult = Either.EitherMap(_testLeftMorphism, _testRightMorphism, testEither);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromLeft
      {
        private static readonly ArgumentException _testDefaultValue = new ArgumentException(Guid.NewGuid().ToString());

        [Fact]
        public void ShouldReturnDefaultValueForNull()
        {
          IEither<ArgumentException, Guid> testEither = null;
          ArgumentException expectedResult = _testDefaultValue;
          ArgumentException actualResult = Either.FromLeft<ArgumentException, Guid>(_testDefaultValue)(testEither);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForRight()
        {
          IEither<ArgumentException, Guid> testEither = Either.Right<ArgumentException, Guid>(Guid.NewGuid());
          ArgumentException expectedResult = _testDefaultValue;
          ArgumentException actualResult = Either.FromLeft<ArgumentException, Guid>(_testDefaultValue)(testEither);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnValueForLeft()
        {
          ArgumentException testLeftValue = new ArgumentException(Guid.NewGuid().ToString());
          IEither<ArgumentException, Guid> testEither = Either.Left<ArgumentException, Guid>(testLeftValue);
          ArgumentException expectedResult = testLeftValue;
          ArgumentException actualResult = Either.FromLeft<ArgumentException, Guid>(_testDefaultValue)(testEither);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromRight
      {
        private static readonly string _testDefaultValue = Guid.NewGuid().ToString();

        [Fact]
        public void ShouldReturnDefaultValueForNull()
        {
          IEither<ArgumentException, string> testEither = null;
          string expectedResult = _testDefaultValue;
          string actualResult = Either.FromRight<ArgumentException, string>(_testDefaultValue)(testEither);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForLeft()
        {
          IEither<ArgumentException, string> testEither = Either.Left<ArgumentException, string>(null);
          string expectedResult = _testDefaultValue;
          string actualResult = Either.FromRight<ArgumentException, string>(_testDefaultValue)(testEither);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnValueForRight()
        {
          string testRightValue = Guid.NewGuid().ToString();
          IEither<ArgumentException, string> testEither = Either.Right<ArgumentException, string>(testRightValue);
          string expectedResult = testRightValue;
          string actualResult = Either.FromRight<ArgumentException, string>(_testDefaultValue)(testEither);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeLefts
      {
        [Fact]
        public void ShouldReturnEmptyListForNullList()
        {
          IEnumerable<IEither<string, Guid>> testList = null;
          IEnumerable<string> expectedResult = new List<string>();
          IEnumerable<string> actualResult = Either.Lefts(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForEmptyList()
        {
          IEnumerable<IEither<string, Guid>> testList = new List<IEither<string, Guid>>();
          IEnumerable<string> expectedResult = new List<string>();
          IEnumerable<string> actualResult = Either.Lefts(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForBlankList()
        {
          IEnumerable<IEither<string, Guid>> testList = new List<IEither<string, Guid>> { null };
          IEnumerable<string> expectedResult = new List<string>();
          IEnumerable<string> actualResult = Either.Lefts(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnListOfLeftValuesForMixedList()
        {
          string testLeftValue1 = Guid.NewGuid().ToString();
          string testLeftValue2 = Guid.NewGuid().ToString();
          Guid testRightValue1 = Guid.NewGuid();
          Guid testRightValue2 = Guid.NewGuid();
          IEnumerable<IEither<string, Guid>> testList = new List<IEither<string, Guid>>
          {
            Either.Left<string, Guid>(testLeftValue1),
            Either.Left<string, Guid>(testLeftValue2),
            Either.Right<string, Guid>(testRightValue1),
            Either.Right<string, Guid>(testRightValue2)
          };
          IEnumerable<string> expectedResult = new List<string>
          {
            testLeftValue1,
            testLeftValue2
          };
          IEnumerable<string> actualResult = Either.Lefts(testList);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribePartitionEithers
      {
        [Fact]
        public void ShouldReturnEmptyListsForNullList()
        {
          IEnumerable<IEither<string, Guid>> testEnumerable = null;
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<string>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Either.PartitionEithers(testEnumerable);

          // NOTE(justin.morgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEmptyListsForEmptyList()
        {
          IEnumerable<IEither<string, Guid>> testEnumerable = new List<IEither<string, Guid>>();
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<string>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Either.PartitionEithers(testEnumerable);

          // NOTE(justin.morgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEmptyListsForBlankList()
        {
          IEnumerable<IEither<string, Guid>> testEnumerable = new List<IEither<string, Guid>> { null };
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<string>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Either.PartitionEithers(testEnumerable);

          // NOTE(justin.morgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnPartitionedListsForMixedList()
        {
          string testLeftValue1 = Guid.NewGuid().ToString();
          string testLeftValue2 = Guid.NewGuid().ToString();
          Guid testRightValue1 = Guid.NewGuid();
          Guid testRightValue2 = Guid.NewGuid();
          IEnumerable<IEither<string, Guid>> testEnumerable = new List<IEither<string, Guid>>
          {
            Either.Left<string, Guid>(testLeftValue1),
            Either.Left<string, Guid>(testLeftValue2),
            Either.Right<string, Guid>(testRightValue1),
            Either.Right<string, Guid>(testRightValue2)
          };
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            new List<string>
            {
              testLeftValue1,
              testLeftValue2
            },
            new List<Guid>
            {
              testRightValue1,
              testRightValue2
            }
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Either.PartitionEithers(testEnumerable);

          // NOTE(justin.morgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }
      }

      public class DescribeRights
      {
        [Fact]
        public void ShouldReturnEmptyListForNullList()
        {
          IEnumerable<IEither<string, Guid>> testList = null;
          IEnumerable<Guid> expectedResult = new List<Guid>();
          IEnumerable<Guid> actualResult = Either.Rights(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForEmptyList()
        {
          IEnumerable<IEither<string, Guid>> testList = new List<IEither<string, Guid>>();
          IEnumerable<Guid> expectedResult = new List<Guid>();
          IEnumerable<Guid> actualResult = Either.Rights(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForBlankList()
        {
          IEnumerable<IEither<string, Guid>> testList = new List<IEither<string, Guid>> { null };
          IEnumerable<Guid> expectedResult = new List<Guid>();
          IEnumerable<Guid> actualResult = Either.Rights(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnListOfRightValuesForMixedList()
        {
          string testLeftValue1 = Guid.NewGuid().ToString();
          string testLeftValue2 = Guid.NewGuid().ToString();
          Guid testRightValue1 = Guid.NewGuid();
          Guid testRightValue2 = Guid.NewGuid();
          IEnumerable<IEither<string, Guid>> testList = new List<IEither<string, Guid>>
          {
            Either.Left<string, Guid>(testLeftValue1),
            Either.Left<string, Guid>(testLeftValue2),
            Either.Right<string, Guid>(testRightValue1),
            Either.Right<string, Guid>(testRightValue2)
          };
          IEnumerable<Guid> expectedResult = new List<Guid>
          {
            testRightValue1,
            testRightValue2
          };
          IEnumerable<Guid> actualResult = Either.Rights(testList);

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public class DescribeLeft
    {
      private readonly string _testLeftValue = Guid.NewGuid().ToString();
      private readonly IEither<string, Guid> _testEither;

      DescribeLeft() => _testEither = Either.Left<string, Guid>(_testLeftValue);

      public class DescribeEquals : DescribeLeft
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          IEither<string, Guid> testOther = null;

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingValues()
        {
          IEither<string, Guid> testOther = Either.Left<string, Guid>(Guid.NewGuid().ToString());

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForRight()
        {
          IEither<string, Guid> testOther = Either.Right<string, Guid>(Guid.NewGuid());

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Testing for code coverage
          Assert.True(_testEither.Equals(_testEither));
          #pragma warning restore RECS0088
        }

        [Fact]
        public void ShouldReturnTrueForSameValue()
        {
          IEither<string, Guid> testOther = Either.Left<string, Guid>(_testLeftValue);

          Assert.True(_testEither.Equals(testOther));
        }
      }

      public class DescribeHashCode : DescribeLeft
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          IEither<string, Guid> testOther = Either.Left<string, Guid>(Guid.NewGuid().ToString());

          Assert.NotEqual(_testEither.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          IEither<string, Guid> testOther = Either.Left<string, Guid>(_testLeftValue);

          Assert.Equal(_testEither.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsLeft : DescribeLeft
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testEither.IsLeft());
        }
      }

      public class DescribeIsRight : DescribeLeft
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testEither.IsRight());
        }
      }

      public class DescribeToString : DescribeLeft
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Left<{typeof(string)}> {_testLeftValue}";
          string actualResult = _testEither.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public class DescribeRight
    {
      private readonly Guid _testRightValue = Guid.NewGuid();
      private readonly IEither<string, Guid> _testEither;

      DescribeRight() => _testEither = Either.Right<string, Guid>(_testRightValue);

      public class DescribeEquals : DescribeRight
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          IEither<string, Guid> testOther = null;

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingValues()
        {
          IEither<string, Guid> testOther = Either.Right<string, Guid>(Guid.NewGuid());

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForLeft()
        {
          IEither<string, Guid> testOther = Either.Left<string, Guid>(Guid.NewGuid().ToString());

          Assert.False(_testEither.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Testing for code coverage
          Assert.True(_testEither.Equals(_testEither));
          #pragma warning restore RECS0088
        }

        [Fact]
        public void ShouldReturnTrueForSameValue()
        {
          IEither<string, Guid> testOther = Either.Right<string, Guid>(_testRightValue);

          Assert.True(_testEither.Equals(testOther));
        }
      }

      public class DescribeHashCode : DescribeRight
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          IEither<string, Guid> testOther = Either.Right<string, Guid>(Guid.NewGuid());

          Assert.NotEqual(_testEither.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          IEither<string, Guid> testOther = Either.Right<string, Guid>(_testRightValue);

          Assert.Equal(_testEither.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsLeft : DescribeRight
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testEither.IsLeft());
        }
      }

      public class DescribeIsRight : DescribeRight
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testEither.IsRight());
        }
      }

      public class DescribeToString : DescribeRight
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Right<{typeof(Guid)}> {_testRightValue}";
          string actualResult = _testEither.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }
  }
}
