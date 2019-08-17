using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using FunctionalExtras.Data;

namespace FunctionalExtras.Tests.Data
{
  public static class ValidationTests
  {
    public static class DescribeStaticMethods
    {
      public class DescribeConcat
      {
        [Fact]
        public void ShouldThrowExceptionForNullSecond()
        {
          IValidation<string, Guid> testSecond = null;
          IValidation<string, Guid> testFirst = Validation.Success<string, Guid>(Guid.NewGuid());
          
          Assert.Throws<ArgumentNullException>(() => Validation.Concat(testSecond)(testFirst));
        }

        [Fact]
        public void ShouldThrowExceptionForNullFirst()
        {
          IValidation<string, Guid> testSecond = Validation.Success<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> testFirst = null;

          Assert.Throws<ArgumentNullException>(() => Validation.Concat(testSecond)(testFirst));
        }

        [Fact]
        public void ShouldReturnFirstForBothSuccesses()
        {
          IValidation<string, Guid> testSecond = Validation.Success<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> testFirst = Validation.Success<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> expectedResult = testFirst;
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnFirstForFirstFailure()
        {
          IValidation<string, Guid> testSecond = Validation.Success<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> testFirst = Validation.Failure<string, Guid>(Guid.NewGuid().ToString());
          IValidation<string, Guid> expectedResult = testFirst;
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnSecondForSecondFailure()
        {
          IValidation<string, Guid> testSecond = Validation.Failure<string, Guid>(Guid.NewGuid().ToString()); 
          IValidation<string, Guid> testFirst = Validation.Success<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> expectedResult = testSecond;
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnConcatenatedFailures()
        {
          string testValue1 = Guid.NewGuid().ToString();
          string testValue2 = Guid.NewGuid().ToString();
          IValidation<string, Guid> testSecond = Validation.Failure<string, Guid>(testValue2);
          IValidation<string, Guid> testFirst = Validation.Failure<string, Guid>(testValue1);
          IValidation<string, Guid> expectedResult = Validation.Failure<string, Guid>(new List<string>
          {
            testValue1,
            testValue2
          });
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFailures
      {
        [Fact]
        public void ShouldReturnEmptyEnumerableForNullEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = null;
          IEnumerable<string> expectedResult = Enumerable.Empty<string>();
          IEnumerable<string> actualResult = Validation.Failures(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForEmptyEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = Enumerable.Empty<IValidation<string, Guid>>();
          IEnumerable<string> expectedResult = Enumerable.Empty<string>();
          IEnumerable<string> actualResult = Validation.Failures(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForBlankEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>> { null };
          IEnumerable<string> expectedResult = Enumerable.Empty<string>();
          IEnumerable<string> actualResult = Validation.Failures(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEnumerableOfFalureValuesForMixedEnumerable()
        {
          string testFailureValue1 = Guid.NewGuid().ToString();
          string testFailureValue2 = Guid.NewGuid().ToString();
          Guid testSuccessValue1 = Guid.NewGuid();
          Guid testSuccessValue2 = Guid.NewGuid();
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>>
          {
            Validation.Failure<string, Guid>(testFailureValue1),
            Validation.Failure<string, Guid>(testFailureValue2),
            Validation.Success<string, Guid>(testSuccessValue1),
            Validation.Success<string, Guid>(testSuccessValue2)
          };
          IEnumerable<string> expectedResult = new List<string>
          {
            testFailureValue1,
            testFailureValue2
          };
          IEnumerable<string> actualResult = Validation.Failures(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromFailure
      {
        private static readonly IEnumerable<string> _testDefaultValues = new List<string>
        {
          Guid.NewGuid().ToString(),
          Guid.NewGuid().ToString()
        };

        [Fact]
        public void ShouldReturnDefaultValueForNullValidation()
        {;
          IValidation<string, Guid> testValidation = null;
          IEnumerable<string> expectedResult = _testDefaultValues;
          IEnumerable<string> actualResult = Validation.FromFailure<string, Guid>(_testDefaultValues)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForSuccess()
        {
          IValidation<string, Guid> testValidation = Validation.Success<string, Guid>(Guid.NewGuid());
          IEnumerable<string> expectedResult = _testDefaultValues;
          IEnumerable<string> actualResult = Validation.FromFailure<string, Guid>(_testDefaultValues)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnFailureValueForFailure()
        {
          string testFailureValue = Guid.NewGuid().ToString();
          IValidation<string, Guid> testValidation = Validation.Failure<string, Guid>(testFailureValue);
          IEnumerable<string> expectedResult = new List<string> { testFailureValue };
          IEnumerable<string> actualResult = Validation.FromFailure<string, Guid>(_testDefaultValues)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromSuccess
      {
        private static readonly Guid _testDefaultValue = Guid.NewGuid();

        [Fact]
        public void ShouldReturnDefaultValueForNullValidation()
        {
          ;
          IValidation<string, Guid> testValidation = null;
          Guid expectedResult = _testDefaultValue;
          Guid actualResult = Validation.FromSuccess<string, Guid>(_testDefaultValue)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForFailure()
        {
          IValidation<string, Guid> testValidation = Validation.Failure<string, Guid>(Guid.NewGuid().ToString());
          Guid expectedResult = _testDefaultValue;
          Guid actualResult = Validation.FromSuccess<string, Guid>(_testDefaultValue)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnSuccessValueForSuccess()
        {
          Guid testSuccessValue = Guid.NewGuid();
          IValidation<string, Guid> testValidation = Validation.Success<string, Guid>(testSuccessValue);
          Guid expectedResult = testSuccessValue;
          Guid actualResult = Validation.FromSuccess<string, Guid>(_testDefaultValue)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribePartitionValidations
      {
        [Fact]
        public void ShouldReturnEmptyEnumerablesForNullEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testList = null;
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<string>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Validation.PartitionValidations(testList);

          // NOTE(justin.morgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerablesForEmptyEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testList = Enumerable.Empty<IValidation<string, Guid>>();
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<string>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Validation.PartitionValidations(testList);

          // NOTE(justin.morgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerablesForBlankEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testList = new List<IValidation<string, Guid>> { null };
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            Enumerable.Empty<string>(),
            Enumerable.Empty<Guid>()
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Validation.PartitionValidations(testList);

          // NOTE(justin.morgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEnumerablesForMixedEnumerable()
        {
          string testFailureValue1 = Guid.NewGuid().ToString();
          string testFailureValue2 = Guid.NewGuid().ToString();
          Guid testSuccessValue1 = Guid.NewGuid();
          Guid testSuccessValue2 = Guid.NewGuid();
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>>
          {
            Validation.Failure<string, Guid>(testFailureValue1),
            Validation.Failure<string, Guid>(testFailureValue2),
            Validation.Success<string, Guid>(testSuccessValue1),
            Validation.Success<string, Guid>(testSuccessValue2)
          };
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            new List<string>
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
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Validation.PartitionValidations(testEnumerable);

          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }
      }

      public class DescribeSuccesses
      {
        [Fact]
        public void ShouldReturnEmptyEnumerableForNullEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = null;
          IEnumerable<Guid> expectedResult = Enumerable.Empty<Guid>();
          IEnumerable<Guid> actualResult = Validation.Successes(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForEmptyEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = Enumerable.Empty<IValidation<string, Guid>>();
          IEnumerable<Guid> expectedResult = Enumerable.Empty<Guid>();
          IEnumerable<Guid> actualResult = Validation.Successes(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForBlankEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>> { null };
          IEnumerable<Guid> expectedResult = Enumerable.Empty<Guid>();
          IEnumerable<Guid> actualResult = Validation.Successes(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEnumerableOfSuccessValuesForMixedEnumerable()
        {
          string testFailureValue1 = Guid.NewGuid().ToString();
          string testFailureValue2 = Guid.NewGuid().ToString();
          Guid testSuccessValue1 = Guid.NewGuid();
          Guid testSuccessValue2 = Guid.NewGuid();
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>>
          {
            Validation.Failure<string, Guid>(testFailureValue1),
            Validation.Failure<string, Guid>(testFailureValue2),
            Validation.Success<string, Guid>(testSuccessValue1),
            Validation.Success<string, Guid>(testSuccessValue2)
          };
          IEnumerable<Guid> expectedResult = new List<Guid>
          {
            testSuccessValue1,
            testSuccessValue2
          };
          IEnumerable<Guid> actualResult = Validation.Successes(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeValidate
      {
        private static readonly string _testFailureValue = Guid.NewGuid().ToString();
        private static readonly Guid _testValue = Guid.NewGuid();

        [Fact]
        public void ShouldThrowExceptionForNullPredicate()
        {
          Predicate<Guid> testPredicate = null;
          
          Assert.Throws<ArgumentNullException>(
            () => Validation.Validate<string, Guid>(testPredicate)(_testFailureValue)(_testValue)
          );
        }

        [Fact]
        public void ShouldReturnFailureForFalsePredicate()
        {
          Predicate<Guid> testPredicate = ignored => false;
          IValidation<string, Guid> expectedResult = Validation.Failure<string, Guid>(_testFailureValue);
          IValidation<string, Guid> actualResult = Validation.Validate(testPredicate, _testFailureValue, _testValue);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnSuccessForTruePredicate()
        {
          Predicate<Guid> testPredicate = ignored => true;
          IValidation<string, Guid> expectedResult = Validation.Success<string, Guid>(_testValue);
          IValidation<string, Guid> actualResult = Validation.Validate(testPredicate, _testFailureValue, _testValue);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeValidationMap
      {
        private static readonly Func<IEnumerable<Exception>, string> _testFailureMorphism = enumerable => enumerable
          .Select(exception => exception.Message)
          .Aggregate((left, right) => $"{left}, {right}");
        private static readonly Func<Guid, string> _testSuccessMorphism = guid => guid.ToString();

        [Fact]
        public void ShouldThrowExceptionForNullFailureMorphism()
        {
          Func<IEnumerable<Exception>, string> testFailureMorphism = null;
          IValidation<Exception, Guid> testValidation = Validation.Failure<Exception, Guid>(new Exception());

          Assert.Throws<ArgumentNullException>(
            () => Validation.ValidationMap<Exception, Guid, string>(testFailureMorphism)
              (_testSuccessMorphism)
              (testValidation)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullSuccessMorphism()
        {
          Func<Guid, string> testSuccessMorphism = null;
          IValidation<Exception, Guid> testValidation = Validation.Success<Exception, Guid>(Guid.NewGuid());

          Assert.Throws<ArgumentNullException>(
            () => Validation.ValidationMap<Exception, Guid, string>(_testFailureMorphism)
              (testSuccessMorphism)
              (testValidation)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullValidation()
        {
          IValidation<Exception, Guid> testValidation = null;

          Assert.Throws<ArgumentNullException>(
            () => Validation.ValidationMap<Exception, Guid, string>(_testFailureMorphism)
              (_testSuccessMorphism)
              (testValidation)
          );
        }

        [Fact]
        public void ShouldReturnMappedValueForFailure()
        {
          IEnumerable<Exception> testFailures = new List<Exception>
          {
            new Exception(Guid.NewGuid().ToString()),
            new Exception(Guid.NewGuid().ToString())
          };
          IValidation<Exception, Guid> testValidation = Validation.Failure<Exception, Guid>(testFailures);
          string expectedResult = _testFailureMorphism(testFailures);
          string actualResult = Validation.ValidationMap<Exception, Guid, string>(_testFailureMorphism)
            (_testSuccessMorphism)
            (testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnMappedValueForSuccess()
        {
          Guid testValue = Guid.NewGuid();
          IValidation<Exception, Guid> testValidation = Validation.Success<Exception, Guid>(testValue);
          string expectedResult = testValue.ToString();
          string actualResult = Validation.ValidationMap<Exception, Guid, string>(_testFailureMorphism)
            (_testSuccessMorphism)
            (testValidation);

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public static class DescribeFailure
    {
      private static readonly string _testFailureValue = Guid.NewGuid().ToString();
      private static readonly IValidation<string, Guid> _testValidation = Validation.Failure<string, Guid>(_testFailureValue);

      public class DescribeConstructor
      {
        [Fact]
        public void ShouldCoalesceNullEnumerableToEmpty()
        {
          IEnumerable<string> testFailureValues = null;
          IValidation<string, Guid> expectedResult = Validation.Failure<string, Guid>(Enumerable.Empty<string>());
          IValidation<string, Guid> actualResult = Validation.Failure<string, Guid>(testFailureValues);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeEquals
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          IValidation<string, Guid> testOther = null;

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingValues()
        {
          IValidation<string, Guid> testOther = Validation.Failure<string, Guid>(Guid.NewGuid().ToString());

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForSuccess()
        {
          IValidation<string, Guid> testOther = Validation.Success<string, Guid>(Guid.NewGuid());

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Testing for code coverage
          Assert.True(_testValidation.Equals(_testValidation));
          #pragma warning restore RECS0088
        }

        [Fact]
        public void ShouldReturnTrueForSameValue()
        {
          IValidation<string, Guid> testOther = Validation.Failure<string, Guid>(_testFailureValue);

          Assert.True(_testValidation.Equals(testOther));
        }
      }

      public class DescribeGetHashCode
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          IValidation<string, Guid> testOther = Validation.Failure<string, Guid>(Guid.NewGuid().ToString());

          Assert.NotEqual(_testValidation.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          // NOTE(jlmorgan): Differing lists of the same values generate differing hash codes.
          IEnumerable<string> testFailureValues = new List<string> { _testFailureValue };
          IValidation<string, Guid> testValidation = Validation.Failure<string, Guid>(testFailureValues);
          IValidation<string, Guid> testOther = Validation.Failure<string, Guid>(testFailureValues);

          Assert.Equal(testValidation.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsFailure
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testValidation.IsFailure());
        }
      }

      public class DescribeIsSuccess
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testValidation.IsSuccess());
        }
      }

      public class DescribeToString
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          IEnumerable<string> testFailureValues = new List<string>
          {
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
          };
          IValidation<string, Guid> testValidation = Validation.Failure<string, Guid>(testFailureValues);
          string expectedResult = $"Failure<{typeof(IEnumerable<string>)}> [{string.Join(",", testFailureValues)}]";
          string actualResult = testValidation.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public static class DescribeSuccess
    {
      private static readonly Guid _testSuccessValue = Guid.NewGuid();
      private static readonly  IValidation<string, Guid> _testValidation = Validation.Success<string, Guid>(_testSuccessValue);

      public class DescribeEquals
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          IValidation<string, Guid> testOther = null;

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingValues()
        {
          IValidation<string, Guid> testOther = Validation.Success<string, Guid>(Guid.NewGuid());

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForFailure()
        {
          IValidation<string, Guid> testOther = Validation.Failure<string, Guid>(Guid.NewGuid().ToString());

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Testing for code coverage
          Assert.True(_testValidation.Equals(_testValidation));
          #pragma warning restore RECS0088
        }

        [Fact]
        public void ShouldReturnTrueForSameValue()
        {
          IValidation<string, Guid> testOther = Validation.Success<string, Guid>(_testSuccessValue);

          Assert.True(_testValidation.Equals(testOther));
        }
      }

      public class DescribeGetHashCode
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          IValidation<string, Guid> testOther = Validation.Success<string, Guid>(Guid.NewGuid());

          Assert.NotEqual(_testValidation.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          IValidation<string, Guid> testOther = Validation.Success<string, Guid>(_testSuccessValue);

          Assert.Equal(_testValidation.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsFailure
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testValidation.IsFailure());
        }
      }

      public class DescribeIsSuccess
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testValidation.IsSuccess());
        }
      }

      public class DescribeToString
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Success<{typeof(Guid)}> {_testSuccessValue}";
          string actualResult = _testValidation.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }
  }
}
