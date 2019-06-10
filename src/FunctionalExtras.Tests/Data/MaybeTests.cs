//using static FunctionalExtras.Data.Functions;

using System;
using System.Collections.Generic;
using Xunit;

using FunctionalExtras.Data;

namespace FunctionalExtras.Tests.Data
{
  public static class MaybeTests
  {
    public class DescribeStaticMethods
    {
      public class DescribeCatMaybes
      {
        [Fact]
        public void ShouldReturnEmptyListForNullList()
        {
          List<IMaybe<Guid>> testList = null;
          List<Guid> expectedResult = new List<Guid>();
          List<Guid> actualResult = Maybe.CatMaybes(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForEmptyList()
        {
          List<IMaybe<Guid>> testList = new List<IMaybe<Guid>>();
          List<Guid> expectedResult = new List<Guid>();
          List<Guid> actualResult = Maybe.CatMaybes(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnListOfNonNullValuesForMixedList()
        {
          Guid testValue1 = Guid.NewGuid();
          Guid testValue2 = Guid.NewGuid();
          List<IMaybe<Guid>> testList = new List<IMaybe<Guid>>
          {
            Maybe.Just(testValue1),
            Maybe.Nothing<Guid>(),
            Maybe.Just(testValue2),
            Maybe.Nothing<Guid>()
          };
          List<Guid> expectedResult = new List<Guid>
          {
            testValue1,
            testValue2
          };
          List<Guid> actualResult = Maybe.CatMaybes(testList);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromJust
      {
        [Fact]
        public void ShouldThrowExceptionForNull()
        {
          Assert.Throws<ArgumentException>(() => Maybe.FromJust<Guid>(null));
        }

        [Fact]
        public void ShouldThrowExceptionForNothing()
        {
          Assert.Throws<ArgumentException>(() => Maybe.FromJust(Maybe.Nothing<Guid>()));
        }

        [Fact]
        public void ShouldReturnValueForJust()
        {
          Guid testValue = Guid.NewGuid();
          IMaybe<Guid> testMaybe = Maybe.Just(testValue);
          Guid expectedResult = testValue;
          Guid actualResult = Maybe.FromJust(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeFromMaybe
      {
        public readonly Guid _testDefaultValue = Guid.NewGuid();

        [Fact]
        public void ShouldReturnDefaultValueForNull()
        {
          IMaybe<Guid> testMaybe = null;
          Guid expectedResult = _testDefaultValue;
          Guid actualResult = Maybe.FromMaybe(_testDefaultValue)(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForNothing()
        {
          IMaybe<Guid> testMaybe = Maybe.Nothing<Guid>();
          Guid expectedResult = _testDefaultValue;
          Guid actualResult = Maybe.FromMaybe(_testDefaultValue)(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldValueForJust()
        {
          Guid testValue = Guid.NewGuid();
          IMaybe<Guid> testMaybe = Maybe.Just(testValue);
          Guid expectedResult = testValue;
          Guid actualResult = Maybe.FromMaybe(_testDefaultValue)(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeIsJust
      {
        [Fact]
        public void ShouldReturnTrueForJust()
        {
          IMaybe<Guid> testMaybe = Maybe.Just(Guid.NewGuid());

          Assert.True(Maybe.IsJust(testMaybe));
        }

        [Fact]
        public void ShouldReturnFalseForNothing()
        {
          IMaybe<Guid> testMaybe = Maybe.Nothing<Guid>();

          Assert.False(Maybe.IsJust(testMaybe));
        }
      }

      public class DescribeIsNothing
      {
        [Fact]
        public void ShouldReturnFalseForJust()
        {
          IMaybe<Guid> testMaybe = Maybe.Just(Guid.NewGuid());

          Assert.False(Maybe.IsNothing(testMaybe));
        }

        [Fact]
        public void ShouldReturnTrueForNothing()
        {
          IMaybe<Guid> testMaybe = Maybe.Nothing<Guid>();

          Assert.True(Maybe.IsNothing(testMaybe));
        }
      }

      public class DescribeJust
      {
        [Fact]
        public void ShouldThrowExceptionForNull()
        {
          Assert.Throws<ArgumentNullException>(() => Maybe.Just<object>(null));
        }
      }

      public class DescibeListToMaybe
      {
        [Fact]
        public void ShouldReturnNothingForNullList()
        {
          List<Guid> testList = null;
          IMaybe<Guid> expectedResult = Maybe.Nothing<Guid>();
          IMaybe<Guid> actualResult = Maybe.ListToMaybe(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnNothingForEmptyList()
        {
          List<Guid> testList = new List<Guid>();
          IMaybe<Guid> expectedResult = Maybe.Nothing<Guid>();
          IMaybe<Guid> actualResult = Maybe.ListToMaybe(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnNothingForBlankList()
        {
          List<object> testList = new List<object> { null };
          IMaybe<object> expectedResult = Maybe.Nothing<object>();
          IMaybe<object> actualResult = Maybe.ListToMaybe(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnJustForFirstInList()
        {
          Guid testValue = Guid.NewGuid();
          List<Guid> testList = new List<Guid> { testValue, Guid.NewGuid() };
          IMaybe<Guid> expectedResult = Maybe.Just(testValue);
          IMaybe<Guid> actualResult = Maybe.ListToMaybe(testList);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeMapMaybe
      {
        private readonly Func<string, IMaybe<string>> _testMorphism = value => (value ?? "").Length > 0
          ? Maybe.Just(value)
          : Maybe.Nothing<string>();

        [Fact]
        public void ShouldThrowExceptionForNullMorphism()
        {
          Func<string, IMaybe<string>> testMorphism = null;
          List<string> testList = new List<string>();

          Assert.Throws<ArgumentNullException>(() => Maybe.MapMaybe(testMorphism)(testList));
        }

        [Fact]
        public void ShouldReturnEmptyListForNullList()
        {
          List<string> testList = null;
          List<string> expectedResult = new List<string>();
          List<string> actualResult = Maybe.MapMaybe(_testMorphism)(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForEmptyList()
        {
          List<string> testList = new List<string>();
          List<string> expectedResult = new List<string>();
          List<string> actualResult = Maybe.MapMaybe(_testMorphism)(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForBlankList()
        {
          List<string> testList = new List<string> { null };
          List<string> expectedResult = new List<string>();
          List<string> actualResult = Maybe.MapMaybe(_testMorphism)(testList);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnListOfMappedValuesForList()
        {
          List<string> testList = new List<string> { "", "a", "", "b" };
          List<string> expectedResult = new List<string> { "a", "b" };
          List<string> actualResult = Maybe.MapMaybe(_testMorphism)(testList);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeMaybeMap
      {
        private readonly string _testDefaultValue = Guid.NewGuid().ToString();
        private readonly Func<Guid, string> _testMorphism = guid => guid.ToString();

        [Fact]
        public void ShouldThrowExceptionForNullMorphism()
        {
          Func<Guid, string> testMorphism = null;
          IMaybe<Guid> testMaybe = Maybe.Just(Guid.NewGuid());

          Assert.Throws<ArgumentNullException>(
            () => Maybe.MaybeMap<Guid, string>(_testDefaultValue)(testMorphism)(testMaybe)
          );
        }

        [Fact]
        public void ShouldReturnDefaultValueForNullMaybe()
        {
          IMaybe<Guid> testMaybe = null;
          string expectedResult = _testDefaultValue;
          string actualResult = Maybe.MaybeMap<Guid, string>(_testDefaultValue)(_testMorphism)(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnDefaultValueForNothing()
        {
          Guid testValue = Guid.NewGuid();
          IMaybe<Guid> testMaybe = Maybe.Nothing<Guid>();
          string expectedResult = _testDefaultValue;
          string actualResult = Maybe.MaybeMap<Guid, string>(_testDefaultValue)(_testMorphism)(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnValueForJust()
        {
          Guid testValue = Guid.NewGuid();
          IMaybe<Guid> testMaybe = Maybe.Just(testValue);
          string expectedResult = testValue.ToString();
          string actualResult = Maybe.MaybeMap<Guid, string>(_testDefaultValue)(_testMorphism)(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }
      }

      public class DescribeMaybeToList
      {
        [Fact]
        public void ShouldReturnEmptyListForNullMaybe()
        {
          IMaybe<Guid> testMaybe = null;
          List<Guid> expectedResult = new List<Guid>();
          List<Guid> actualResult = Maybe.MaybeToList(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnEmptyListForNothing()
        {
          IMaybe<Guid> testMaybe = Maybe.Nothing<Guid>();
          List<Guid> expectedResult = new List<Guid>();
          List<Guid> actualResult = Maybe.MaybeToList(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ShouldReturnListOfValueForJust()
        {
          Guid testValue = Guid.NewGuid();
          IMaybe<Guid> testMaybe = Maybe.Just(testValue);
          List<Guid> expectedResult = new List<Guid> { testValue };
          List<Guid> actualResult = Maybe.MaybeToList(testMaybe);

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public class DescribeJust
    {
      private readonly Guid _testValue = Guid.NewGuid();
      private readonly IMaybe<Guid> _testMaybe;

      public DescribeJust() => _testMaybe = Maybe.Just(_testValue);

      public class DescribeEquals : DescribeJust
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          IMaybe<Guid> testOther = null;

          Assert.False(_testMaybe.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testMaybe.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForNothing()
        {
          IMaybe<Guid> testOther = Maybe.Nothing<Guid>();

          Assert.False(_testMaybe.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingValues()
        {
          IMaybe<Guid> testOther = Maybe.Just(Guid.NewGuid());

          Assert.False(_testMaybe.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Comparing equal expression for equality is usually useless
          Assert.True(_testMaybe.Equals(_testMaybe));
          #pragma warning restore RECS0088 // Comparing equal expression for equality is usually useless
        }

        [Fact]
        public void ShouldReturnTrueForSameValue()
        {
          IMaybe<Guid> testOther = Maybe.Just(_testValue);

          Assert.True(_testMaybe.Equals(testOther));
        }
      }

      public class DescribeHashCode : DescribeJust
      {
        [Fact]
        public void ShouldReturnDifferingHashCodeForDifferingValues()
        {
          IMaybe<Guid> testMaybe2 = Maybe.Just(Guid.NewGuid());

          Assert.NotEqual(_testMaybe.GetHashCode(), testMaybe2.GetHashCode());
        }

        [Fact]
        public void ShouldReturnSameHashCodeForSameValues()
        {
          IMaybe<Guid> testMaybe2 = Maybe.Just(_testValue);

          Assert.Equal(_testMaybe.GetHashCode(), testMaybe2.GetHashCode());
        }
      }

      public class DescribeIsJust : DescribeJust
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testMaybe.IsJust());
        }
      }

      public class DescribeIsNothing : DescribeJust
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testMaybe.IsNothing());
        }
      }

      public class DescribeToString: DescribeJust
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Just<{typeof(Guid)}>({_testValue})";
          string actualResult = _testMaybe.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }

    public class DescribeNothing
    {
      private readonly IMaybe<Guid> _testMaybe = Maybe.Nothing<Guid>();

      public class DescribeEquals : DescribeNothing
      {
        [Fact]
        public void ShouldReturnFalseForNull()
        {
          IMaybe<Guid> testOther = null;

          Assert.False(_testMaybe.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnFalseForDifferingType()
        {
          object testOther = new object();

          Assert.False(_testMaybe.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForNothing()
        {
          IMaybe<Guid> testOther = Maybe.Nothing<Guid>();

          Assert.True(_testMaybe.Equals(testOther));
        }

        [Fact]
        public void ShouldReturnTrueForSameInstance()
        {
          #pragma warning disable RECS0088 // Comparing equal expression for equality is usually useless
          Assert.True(_testMaybe.Equals(_testMaybe));
          #pragma warning restore RECS0088 // Comparing equal expression for equality is usually useless
        }
      }

      public class DescribeHashCode : DescribeNothing
      {
        [Fact]
        public void ShouldReturnSameHashCode()
        {
          IMaybe<Guid> testMaybe2 = Maybe.Nothing<Guid>();

          Assert.Equal(_testMaybe.GetHashCode(), testMaybe2.GetHashCode());
        }
      }

      public class DescribeIsJust : DescribeNothing
      {
        [Fact]
        public void ShouldReturnFalse()
        {
          Assert.False(_testMaybe.IsJust());
        }
      }

      public class DescribeIsNothing : DescribeNothing
      {
        [Fact]
        public void ShouldReturnTrue()
        {
          Assert.True(_testMaybe.IsNothing());
        }
      }

      public class DescribeToString : DescribeNothing
      {
        [Fact]
        public void ShouldReturnFormattedString()
        {
          string expectedResult = $"Nothing<{typeof(Guid)}>()";
          string actualResult = _testMaybe.ToString();

          Assert.Equal(expectedResult, actualResult);
        }
      }
    }
  }
}
