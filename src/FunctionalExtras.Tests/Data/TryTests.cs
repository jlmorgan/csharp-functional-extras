using System;
using System.Collections.Generic;
using Xunit;

using FunctionalExtras.Data;
using System.Linq;

namespace FunctionalExtras.Tests.Data
{
  public static class TryTests
  {
    public class DescribeStaticMethods
    {
      public class DescribeAttempt
      {
        [Fact]
        public void ShouldReturnFailureForThrownException()
        {
          Exception testException = new Exception(Guid.NewGuid().ToString());
          ITry<Guid> expectedResult = Try.Failure<Guid>(testException);
          ITry<Guid> actualResult = Try.Attempt<Guid>(() => throw testException);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnSuccessForValue()
        {
          Guid testValue = Guid.NewGuid();
          ITry<Guid> expectedResult = Try.Success(testValue);
          ITry<Guid> actualResult = Try.Attempt(() => testValue);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromFailure
      {
        private static readonly Exception _testDefaultValue = new ArgumentException(Guid.NewGuid().ToString());

        [Fact]
        public void ShouldReturnDefaultValueForNull()
        {
          ITry<Guid> testTry = null;
          Exception expectedResult = _testDefaultValue;
          Exception actualResult = Try.FromFailure<Guid>(_testDefaultValue)(testTry);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForSuccess()
        {
          ITry<Guid> testTry = Try.Success(Guid.NewGuid());
          Exception expectedResult = _testDefaultValue;
          Exception actualResult = Try.FromFailure<Guid>(_testDefaultValue)(testTry);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnValueForFailure()
        {
          Exception testFailureValue = new ArgumentException(Guid.NewGuid().ToString());
          ITry<Guid> testTry = Try.Failure<Guid>(testFailureValue);
          Exception expectedResult = testFailureValue;
          Exception actualResult = Try.FromFailure<Guid>(_testDefaultValue)(testTry);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromSuccess
      {
        private static readonly string _testDefaultValue = Guid.NewGuid().ToString();

        [Fact]
        public void ShouldReturnDefaultValueForNull()
        {
          ITry<string> testTry = null;
          string expectedResult = _testDefaultValue;
          string actualResult = Try.FromSuccess(_testDefaultValue)(testTry);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForFailure()
        {
          ITry<string> testTry = Try.Failure<string>(null);
          string expectedResult = _testDefaultValue;
          string actualResult = Try.FromSuccess(_testDefaultValue)(testTry);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnValueForSuccess()
        {
          string testSuccessValue = Guid.NewGuid().ToString();
          ITry<string> testTry = Try.Success(testSuccessValue);
          string expectedResult = testSuccessValue;
          string actualResult = Try.FromSuccess(_testDefaultValue)(testTry);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFailures
      {
        [Fact]
        public void ShouldReturnEmptyListForNullList()
        {
          IEnumerable<ITry<Guid>> testList = null;
          IEnumerable<Exception> expectedResult = new List<Exception>();
          IEnumerable<Exception> actualResult = Try.Failures(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForEmptyList()
        {
          IEnumerable<ITry<Guid>> testList = new List<ITry<Guid>>();
          IEnumerable<Exception> expectedResult = new List<Exception>();
          IEnumerable<Exception> actualResult = Try.Failures(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForBlankList()
        {
          IEnumerable<ITry<Guid>> testList = new List<ITry<Guid>> { null };
          IEnumerable<Exception> expectedResult = new List<Exception>();
          IEnumerable<Exception> actualResult = Try.Failures(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnListOfFailureValuesForMixedList()
        {
          Exception testFailureValue1 = new Exception(Guid.NewGuid().ToString());
          Exception testFailureValue2 = new Exception(Guid.NewGuid().ToString());
          Guid testSuccessValue1 = Guid.NewGuid();
          Guid testSuccessValue2 = Guid.NewGuid();
          IEnumerable<ITry<Guid>> testList = new List<ITry<Guid>>
          {
            Try.Failure<Guid>(testFailureValue1),
            Try.Failure<Guid>(testFailureValue2),
            Try.Success(testSuccessValue1),
            Try.Success(testSuccessValue2)
          };
          IEnumerable<Exception> expectedResult = new List<Exception>
          {
            testFailureValue1,
            testFailureValue2
          };
          IEnumerable<Exception> actualResult = Try.Failures(testList);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribePartitionTries
      {
        [Fact]
        public void ShouldReturnEmptyListsForNullList()
        {
          IEnumerable<ITry<Guid>> testEnumerable = null;
          (IEnumerable<Exception>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<Exception>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<Exception>, IEnumerable<Guid>) actualResult = Try.PartitionTries(testEnumerable);

          // NOTE(jlmorgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEmptyListsForEmptyList()
        {
          IEnumerable<ITry<Guid>> testEnumerable = new List<ITry<Guid>>();
          (IEnumerable<Exception>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<Exception>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<Exception>, IEnumerable<Guid>) actualResult = Try.PartitionTries(testEnumerable);

          // NOTE(jlmorgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEmptyListsForBlankList()
        {
          IEnumerable<ITry<Guid>> testEnumerable = new List<ITry<Guid>> { null };
          (IEnumerable<Exception>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<Exception>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<Exception>, IEnumerable<Guid>) actualResult = Try.PartitionTries(testEnumerable);

          // NOTE(jlmorgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnPartitionedListsForMixedList()
        {
          Exception testFailureValue1 = new Exception(Guid.NewGuid().ToString());
          Exception testFailureValue2 = new Exception(Guid.NewGuid().ToString());
          Guid testSuccessValue1 = Guid.NewGuid();
          Guid testSuccessValue2 = Guid.NewGuid();
          IEnumerable<ITry<Guid>> testEnumerable = new List<ITry<Guid>>
          {
            Try.Failure<Guid>(testFailureValue1),
            Try.Failure<Guid>(testFailureValue2),
            Try.Success(testSuccessValue1),
            Try.Success(testSuccessValue2)
          };
          (IEnumerable<Exception>, IEnumerable<Guid>) expectedResult = (
            new List<Exception>
            {
              testFailureValue1,
              testFailureValue2
            },
            new List<Guid>
            {
              testSuccessValue1,
              testSuccessValue2
            }
          );
          (IEnumerable<Exception>, IEnumerable<Guid>) actualResult = Try.PartitionTries(testEnumerable);

          // NOTE(jlmorgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }
      }

      public class DescribeSuccesses
      {
        [Fact]
        public void ShouldReturnEmptyListForNullList()
        {
          IEnumerable<ITry<Guid>> testList = null;
          IEnumerable<Guid> expectedResult = new List<Guid>();
          IEnumerable<Guid> actualResult = Try.Successes(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForEmptyList()
        {
          IEnumerable<ITry<Guid>> testList = new List<ITry<Guid>>();
          IEnumerable<Guid> expectedResult = new List<Guid>();
          IEnumerable<Guid> actualResult = Try.Successes(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForBlankList()
        {
          IEnumerable<ITry<Guid>> testList = new List<ITry<Guid>> { null };
          IEnumerable<Guid> expectedResult = new List<Guid>();
          IEnumerable<Guid> actualResult = Try.Successes(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnListOfSuccessValuesForMixedList()
        {
          Exception testFailureValue1 = new Exception(Guid.NewGuid().ToString());
          Exception testFailureValue2 = new Exception(Guid.NewGuid().ToString());
          Guid testSuccessValue1 = Guid.NewGuid();
          Guid testSuccessValue2 = Guid.NewGuid();
          IEnumerable<ITry<Guid>> testList = new List<ITry<Guid>>
          {
            Try.Failure<Guid>(testFailureValue1),
            Try.Failure<Guid>(testFailureValue2),
            Try.Success(testSuccessValue1),
            Try.Success(testSuccessValue2)
          };
          IEnumerable<Guid> expectedResult = new List<Guid>
          {
            testSuccessValue1,
            testSuccessValue2
          };
          IEnumerable<Guid> actualResult = Try.Successes(testList);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeTryMap
      {
        private static readonly Func<Exception, string> _testFailureMorphism = exception => exception.Message;
        private static readonly Func<Guid, string> _testSuccessMorphism = guid => guid.ToString();

        [Fact]
        public void ShouldThrowExceptionForNullFailureMorphism()
        {
          Func<Exception, string> testFailureMorphism = null;
          ITry<Guid> testTry = Try.Failure<Guid>(new ArgumentException());

          Assert.Throws<ArgumentNullException>(() => Try.TryMap<Guid, string>(testFailureMorphism)
            (_testSuccessMorphism)
            (testTry)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullSuccessMorphism()
        {
          Func<Guid, string> testSuccessMorphism = null;
          ITry<Guid> testTry = Try.Success(Guid.NewGuid());

          Assert.Throws<ArgumentNullException>(() => Try.TryMap<Guid, string>(_testFailureMorphism)
            (testSuccessMorphism)
            (testTry)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullSuccessTry()
        {
          ITry<Guid> testTry = null;

          Assert.Throws<ArgumentNullException>(() => Try.TryMap<Guid, string>(_testFailureMorphism)
            (_testSuccessMorphism)
            (testTry)
          );
        }

        [Fact]
        public void ShouldReturnMappedValueForFailure()
        {
          Exception testFailureValue = new ArgumentException(Guid.NewGuid().ToString());
          ITry<Guid> testTry = Try.Failure<Guid>(testFailureValue);
          string expectedResult = testFailureValue.Message;
          string actualResult = Try.TryMap(_testFailureMorphism, _testSuccessMorphism, testTry);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnMappedValueForSuccess()
        {
          Guid testSuccessValue = Guid.NewGuid();
          ITry<Guid> testTry = Try.Success(testSuccessValue);
          string expectedResult = testSuccessValue.ToString();
          string actualResult = Try.TryMap(_testFailureMorphism, _testSuccessMorphism, testTry);

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public class DescribeFailure
    {
      private readonly Exception _testFailureValue = new Exception(Guid.NewGuid().ToString());
      private readonly ITry<Guid> _testTry;

      DescribeFailure() => _testTry = Try.Failure<Guid>(_testFailureValue);

      public class DescribeEquals : DescribeFailure
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          ITry<Guid> testOther = null;

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingValues()
        {
          ITry<Guid> testOther = Try.Failure<Guid>(new Exception(Guid.NewGuid().ToString()));

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForSuccess()
        {
          ITry<Guid> testOther = Try.Success(Guid.NewGuid());

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Testing for code coverage
          Assert.True(_testTry.Equals(_testTry));
          #pragma warning restore RECS0088
        }

        [Fact]
        public void ShouldReturnTrueForSameValue()
        {
          ITry<Guid> testOther = Try.Failure<Guid>(_testFailureValue);

          Assert.True(_testTry.Equals(testOther));
        }
      }

      public class DescribeHashCode : DescribeFailure
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          ITry<Guid> testOther = Try.Failure<Guid>(new Exception(Guid.NewGuid().ToString()));

          Assert.NotEqual(_testTry.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          ITry<Guid> testOther = Try.Failure<Guid>(_testFailureValue);

          Assert.Equal(_testTry.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsFailure : DescribeFailure
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testTry.IsFailure());
        }
      }

      public class DescribeIsSuccess : DescribeFailure
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testTry.IsSuccess());
        }
      }

      public class DescribeToString : DescribeFailure
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Failure<{typeof(Guid)}> {_testFailureValue}";
          string actualResult = _testTry.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public class DescribeSuccess
    {
      private readonly Guid _testSuccessValue = Guid.NewGuid();
      private readonly ITry<Guid> _testTry;

      DescribeSuccess() => _testTry = Try.Success(_testSuccessValue);

      public class DescribeEquals : DescribeSuccess
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          ITry<Guid> testOther = null;

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingValues()
        {
          ITry<Guid> testOther = Try.Success(Guid.NewGuid());

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForFailure()
        {
          ITry<Guid> testOther = Try.Failure<Guid>(new Exception(Guid.NewGuid().ToString()));

          Assert.False(_testTry.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Testing for code coverage
          Assert.True(_testTry.Equals(_testTry));
          #pragma warning restore RECS0088
        }

        [Fact]
        public void ShouldReturnTrueForSameValue()
        {
          ITry<Guid> testOther = Try.Success(_testSuccessValue);

          Assert.True(_testTry.Equals(testOther));
        }
      }

      public class DescribeHashCode : DescribeSuccess
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          ITry<Guid> testOther = Try.Success(Guid.NewGuid());

          Assert.NotEqual(_testTry.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          ITry<Guid> testOther = Try.Success(_testSuccessValue);

          Assert.Equal(_testTry.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsFailure : DescribeSuccess
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testTry.IsFailure());
        }
      }

      public class DescribeIsSuccess : DescribeSuccess
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testTry.IsSuccess());
        }
      }

      public class DescribeToString : DescribeSuccess
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Success<{typeof(Guid)}> {_testSuccessValue}";
          string actualResult = _testTry.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }
  }
}
