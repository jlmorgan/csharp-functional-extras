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
          IValidation<string, Guid> testFirst = Validation.Valid<string, Guid>(Guid.NewGuid());

          Assert.Throws<ArgumentNullException>(() => Validation.Concat(testSecond)(testFirst));
        }

        [Fact]
        public void ShouldThrowExceptionForNullFirst()
        {
          IValidation<string, Guid> testSecond = Validation.Valid<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> testFirst = null;

          Assert.Throws<ArgumentNullException>(() => Validation.Concat(testSecond)(testFirst));
        }

        [Fact]
        public void ShouldReturnFirstForBothValids()
        {
          IValidation<string, Guid> testSecond = Validation.Valid<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> testFirst = Validation.Valid<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> expectedResult = testFirst;
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnFirstForFirstInvalid()
        {
          IValidation<string, Guid> testSecond = Validation.Valid<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> testFirst = Validation.Invalid<string, Guid>(Guid.NewGuid().ToString());
          IValidation<string, Guid> expectedResult = testFirst;
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnSecondForSecondInvalid()
        {
          IValidation<string, Guid> testSecond = Validation.Invalid<string, Guid>(Guid.NewGuid().ToString());
          IValidation<string, Guid> testFirst = Validation.Valid<string, Guid>(Guid.NewGuid());
          IValidation<string, Guid> expectedResult = testSecond;
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnConcatenatedInvalids()
        {
          string testValue1 = Guid.NewGuid().ToString();
          string testValue2 = Guid.NewGuid().ToString();
          IValidation<string, Guid> testSecond = Validation.Invalid<string, Guid>(testValue2);
          IValidation<string, Guid> testFirst = Validation.Invalid<string, Guid>(testValue1);
          IValidation<string, Guid> expectedResult = Validation.Invalid<string, Guid>(new List<string>
          {
            testValue1,
            testValue2
          });
          IValidation<string, Guid> actualResult = Validation.Concat(testSecond)(testFirst);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromInvalid
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
          IEnumerable<string> actualResult = Validation.FromInvalid<string, Guid>(_testDefaultValues)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForValid()
        {
          IValidation<string, Guid> testValidation = Validation.Valid<string, Guid>(Guid.NewGuid());
          IEnumerable<string> expectedResult = _testDefaultValues;
          IEnumerable<string> actualResult = Validation.FromInvalid<string, Guid>(_testDefaultValues)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnInvalidValueForInvalid()
        {
          string testInvalidValue = Guid.NewGuid().ToString();
          IValidation<string, Guid> testValidation = Validation.Invalid<string, Guid>(testInvalidValue);
          IEnumerable<string> expectedResult = new List<string> { testInvalidValue };
          IEnumerable<string> actualResult = Validation.FromInvalid<string, Guid>(_testDefaultValues)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromValid
      {
        private static readonly Guid _testDefaultValue = Guid.NewGuid();

        [Fact]
        public void ShouldReturnDefaultValueForNullValidation()
        {
          ;
          IValidation<string, Guid> testValidation = null;
          Guid expectedResult = _testDefaultValue;
          Guid actualResult = Validation.FromValid<string, Guid>(_testDefaultValue)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForInvalid()
        {
          IValidation<string, Guid> testValidation = Validation.Invalid<string, Guid>(Guid.NewGuid().ToString());
          Guid expectedResult = _testDefaultValue;
          Guid actualResult = Validation.FromValid<string, Guid>(_testDefaultValue)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnValidValueForValid()
        {
          Guid testValidValue = Guid.NewGuid();
          IValidation<string, Guid> testValidation = Validation.Valid<string, Guid>(testValidValue);
          Guid expectedResult = testValidValue;
          Guid actualResult = Validation.FromValid<string, Guid>(_testDefaultValue)(testValidation);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeInvalids
      {
        [Fact]
        public void ShouldReturnEmptyEnumerableForNullEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = null;
          IEnumerable<string> expectedResult = Enumerable.Empty<string>();
          IEnumerable<string> actualResult = Validation.Invalids(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForEmptyEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = Enumerable.Empty<IValidation<string, Guid>>();
          IEnumerable<string> expectedResult = Enumerable.Empty<string>();
          IEnumerable<string> actualResult = Validation.Invalids(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForBlankEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>> { null };
          IEnumerable<string> expectedResult = Enumerable.Empty<string>();
          IEnumerable<string> actualResult = Validation.Invalids(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEnumerableOfFalureValuesForMixedEnumerable()
        {
          string testInvalidValue1 = Guid.NewGuid().ToString();
          string testInvalidValue2 = Guid.NewGuid().ToString();
          Guid testValidValue1 = Guid.NewGuid();
          Guid testValidValue2 = Guid.NewGuid();
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>>
          {
            Validation.Invalid<string, Guid>(testInvalidValue1),
            Validation.Invalid<string, Guid>(testInvalidValue2),
            Validation.Valid<string, Guid>(testValidValue1),
            Validation.Valid<string, Guid>(testValidValue2)
          };
          IEnumerable<string> expectedResult = new List<string>
          {
            testInvalidValue1,
            testInvalidValue2
          };
          IEnumerable<string> actualResult = Validation.Invalids(testEnumerable);

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

          // NOTE(jlmorgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
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

          // NOTE(jlmorgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
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

          // NOTE(jlmorgan): The regular Assert.Equal(expectedResult, actualResult) expects an array and not a
          // collection.
          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }

        [Fact]
        public void ShouldReturnEnumerablesForMixedEnumerable()
        {
          string testInvalidValue1 = Guid.NewGuid().ToString();
          string testInvalidValue2 = Guid.NewGuid().ToString();
          Guid testValidValue1 = Guid.NewGuid();
          Guid testValidValue2 = Guid.NewGuid();
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>>
          {
            Validation.Invalid<string, Guid>(testInvalidValue1),
            Validation.Invalid<string, Guid>(testInvalidValue2),
            Validation.Valid<string, Guid>(testValidValue1),
            Validation.Valid<string, Guid>(testValidValue2)
          };
          (IEnumerable<string>, IEnumerable<Guid>) expectedResult = (
            new List<string>
            {
              testInvalidValue1,
              testInvalidValue2
            },
            new List<Guid>
            {
              testValidValue1,
              testValidValue2
            }
          );
          (IEnumerable<string>, IEnumerable<Guid>) actualResult = Validation.PartitionValidations(testEnumerable);

          Assert.Equal(expectedResult.Item1, actualResult.Item1);
          Assert.Equal(expectedResult.Item2, actualResult.Item2);
        }
      }

      public class DescribeValids
      {
        [Fact]
        public void ShouldReturnEmptyEnumerableForNullEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = null;
          IEnumerable<Guid> expectedResult = Enumerable.Empty<Guid>();
          IEnumerable<Guid> actualResult = Validation.Valids(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForEmptyEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = Enumerable.Empty<IValidation<string, Guid>>();
          IEnumerable<Guid> expectedResult = Enumerable.Empty<Guid>();
          IEnumerable<Guid> actualResult = Validation.Valids(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyEnumerableForBlankEnumerable()
        {
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>> { null };
          IEnumerable<Guid> expectedResult = Enumerable.Empty<Guid>();
          IEnumerable<Guid> actualResult = Validation.Valids(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEnumerableOfValidValuesForMixedEnumerable()
        {
          string testInvalidValue1 = Guid.NewGuid().ToString();
          string testInvalidValue2 = Guid.NewGuid().ToString();
          Guid testValidValue1 = Guid.NewGuid();
          Guid testValidValue2 = Guid.NewGuid();
          IEnumerable<IValidation<string, Guid>> testEnumerable = new List<IValidation<string, Guid>>
          {
            Validation.Invalid<string, Guid>(testInvalidValue1),
            Validation.Invalid<string, Guid>(testInvalidValue2),
            Validation.Valid<string, Guid>(testValidValue1),
            Validation.Valid<string, Guid>(testValidValue2)
          };
          IEnumerable<Guid> expectedResult = new List<Guid>
          {
            testValidValue1,
            testValidValue2
          };
          IEnumerable<Guid> actualResult = Validation.Valids(testEnumerable);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeValidate
      {
        private static readonly string _testInvalidValue = Guid.NewGuid().ToString();
        private static readonly Guid _testValue = Guid.NewGuid();

        [Fact]
        public void ShouldThrowExceptionForNullPredicate()
        {
          Predicate<Guid> testPredicate = null;

          Assert.Throws<ArgumentNullException>(
            () => Validation.Validate<string, Guid>(testPredicate)(_testInvalidValue)(_testValue)
          );
        }

        [Fact]
        public void ShouldReturnInvalidForFalsePredicate()
        {
          Predicate<Guid> testPredicate = ignored => false;
          IValidation<string, Guid> expectedResult = Validation.Invalid<string, Guid>(_testInvalidValue);
          IValidation<string, Guid> actualResult = Validation.Validate(testPredicate, _testInvalidValue, _testValue);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnValidForTruePredicate()
        {
          Predicate<Guid> testPredicate = ignored => true;
          IValidation<string, Guid> expectedResult = Validation.Valid<string, Guid>(_testValue);
          IValidation<string, Guid> actualResult = Validation.Validate(testPredicate, _testInvalidValue, _testValue);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeValidationMap
      {
        private static readonly Func<IEnumerable<Exception>, string> _testInvalidMorphism = enumerable => enumerable
          .Select(exception => exception.Message)
          .Aggregate((left, right) => $"{left}, {right}");
        private static readonly Func<Guid, string> _testValidMorphism = guid => guid.ToString();

        [Fact]
        public void ShouldThrowExceptionForNullInvalidMorphism()
        {
          Func<IEnumerable<Exception>, string> testInvalidMorphism = null;
          IValidation<Exception, Guid> testValidation = Validation.Invalid<Exception, Guid>(new Exception());

          Assert.Throws<ArgumentNullException>(
            () => Validation.ValidationMap<Exception, Guid, string>(testInvalidMorphism)
              (_testValidMorphism)
              (testValidation)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullValidMorphism()
        {
          Func<Guid, string> testValidMorphism = null;
          IValidation<Exception, Guid> testValidation = Validation.Valid<Exception, Guid>(Guid.NewGuid());

          Assert.Throws<ArgumentNullException>(
            () => Validation.ValidationMap<Exception, Guid, string>(_testInvalidMorphism)
              (testValidMorphism)
              (testValidation)
          );
        }

        [Fact]
        public void ShouldThrowExceptionForNullValidation()
        {
          IValidation<Exception, Guid> testValidation = null;

          Assert.Throws<ArgumentNullException>(
            () => Validation.ValidationMap<Exception, Guid, string>(_testInvalidMorphism)
              (_testValidMorphism)
              (testValidation)
          );
        }

        [Fact]
        public void ShouldReturnMappedValueForInvalid()
        {
          IEnumerable<Exception> testInvalids = new List<Exception>
          {
            new Exception(Guid.NewGuid().ToString()),
            new Exception(Guid.NewGuid().ToString())
          };
          IValidation<Exception, Guid> testValidation = Validation.Invalid<Exception, Guid>(testInvalids);
          string expectedResult = _testInvalidMorphism(testInvalids);
          string actualResult = Validation.ValidationMap<Exception, Guid, string>(_testInvalidMorphism)
            (_testValidMorphism)
            (testValidation);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnMappedValueForValid()
        {
          Guid testValue = Guid.NewGuid();
          IValidation<Exception, Guid> testValidation = Validation.Valid<Exception, Guid>(testValue);
          string expectedResult = testValue.ToString();
          string actualResult = Validation.ValidationMap<Exception, Guid, string>(_testInvalidMorphism)
            (_testValidMorphism)
            (testValidation);

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public static class DescribeInvalid
    {
      private static readonly string _testInvalidValue = Guid.NewGuid().ToString();
      private static readonly IValidation<string, Guid> _testValidation = Validation.Invalid<string, Guid>(_testInvalidValue);

      public class DescribeConstructor
      {
        [Fact]
        public void ShouldCoalesceNullEnumerableToEmpty()
        {
          IEnumerable<string> testInvalidValues = null;
          IValidation<string, Guid> expectedResult = Validation.Invalid<string, Guid>(Enumerable.Empty<string>());
          IValidation<string, Guid> actualResult = Validation.Invalid<string, Guid>(testInvalidValues);

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
          IValidation<string, Guid> testOther = Validation.Invalid<string, Guid>(Guid.NewGuid().ToString());

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForValid()
        {
          IValidation<string, Guid> testOther = Validation.Valid<string, Guid>(Guid.NewGuid());

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
          IValidation<string, Guid> testOther = Validation.Invalid<string, Guid>(_testInvalidValue);

          Assert.True(_testValidation.Equals(testOther));
        }
      }

      public class DescribeGetHashCode
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          IValidation<string, Guid> testOther = Validation.Invalid<string, Guid>(Guid.NewGuid().ToString());

          Assert.NotEqual(_testValidation.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          // NOTE(jlmorgan): Differing lists of the same values generate differing hash codes.
          IEnumerable<string> testInvalidValues = new List<string> { _testInvalidValue };
          IValidation<string, Guid> testValidation = Validation.Invalid<string, Guid>(testInvalidValues);
          IValidation<string, Guid> testOther = Validation.Invalid<string, Guid>(testInvalidValues);

          Assert.Equal(testValidation.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsInvalid
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testValidation.IsInvalid());
        }
      }

      public class DescribeIsValid
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testValidation.IsValid());
        }
      }

      public class DescribeToString
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          IEnumerable<string> testInvalidValues = new List<string>
          {
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
          };
          IValidation<string, Guid> testValidation = Validation.Invalid<string, Guid>(testInvalidValues);
          string expectedResult = $"Invalid<{typeof(IEnumerable<string>)}> [{string.Join(",", testInvalidValues)}]";
          string actualResult = testValidation.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public static class DescribeValid
    {
      private static readonly Guid _testValidValue = Guid.NewGuid();
      private static readonly  IValidation<string, Guid> _testValidation = Validation.Valid<string, Guid>(_testValidValue);

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
          IValidation<string, Guid> testOther = Validation.Valid<string, Guid>(Guid.NewGuid());

          Assert.False(_testValidation.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForInvalid()
        {
          IValidation<string, Guid> testOther = Validation.Invalid<string, Guid>(Guid.NewGuid().ToString());

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
          IValidation<string, Guid> testOther = Validation.Valid<string, Guid>(_testValidValue);

          Assert.True(_testValidation.Equals(testOther));
        }
      }

      public class DescribeGetHashCode
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          IValidation<string, Guid> testOther = Validation.Valid<string, Guid>(Guid.NewGuid());

          Assert.NotEqual(_testValidation.GetHashCode(), testOther.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          IValidation<string, Guid> testOther = Validation.Valid<string, Guid>(_testValidValue);

          Assert.Equal(_testValidation.GetHashCode(), testOther.GetHashCode());
        }
      }

      public class DescribeIsInvalid
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testValidation.IsInvalid());
        }
      }

      public class DescribeIsValid
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testValidation.IsValid());
        }
      }

      public class DescribeToString
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Valid<{typeof(Guid)}> {_testValidValue}";
          string actualResult = _testValidation.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }
  }
}
