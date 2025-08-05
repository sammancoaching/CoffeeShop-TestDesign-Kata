using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SammanCoaching.ParameterizedTests.Tests.Concept
{
	public class ParameterizedTests
	{
		[Theory]
		[InlineData("foo")]
		[InlineData("bar")]
		[InlineData("###")]
		[InlineData("___")]
		[InlineData("   ")]
		public void ValueSourceWithStrings(string str)
		{
			// call the code to test that should do some work when getting a string
			Assert.NotNull(str); // Example assertion, replace with real logic
		}

		[Theory]
		[InlineData("foo")]
		[InlineData("bar")]
		[InlineData("###")]
		[InlineData("___")]
		[InlineData("   ")]
		[InlineData(null)]
		[InlineData("")]
		public void ValueSourceWithStringsIncludingNullAndEmptyString(string? str)
		{
			// call the code to test that should do some work when getting a string
			// the code also should work for null or empty strings
			// Example assertion, replace with real logic
			Assert.True(str == null || str.Length >= 0);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(7)]
		[InlineData(8)]
		[InlineData(9)]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-2)]
		[InlineData(int.MinValue)]
		public void ValueSourceWithInts(int intValue)
		{
			// call the code to test that should do some work when getting a value <10
			// Example assertion, replace with real logic
			Assert.True(intValue < 10 || intValue >= 10);
		}

		public enum MyEnum
		{
			FOO, BAR, FOOBAR
		}

		public static IEnumerable<object[]> MyEnumValues =>
			Enum.GetValues(typeof(MyEnum))
				.Cast<MyEnum>()
				.Select(e => new object[] { e });

		[Theory]
		[MemberData(nameof(MyEnumValues))]
		public void EnumSource(MyEnum myEnum)
		{
			// call the code to test that should do some work when getting the enum value
			Assert.True(Enum.IsDefined(typeof(MyEnum), myEnum));
		}

		// ...more tests to be added...

		public class SomeObject
		{
			public SomeObject(string foo, int bar, List<SomeObject> children)
			{
				Foo = foo;
				Bar = bar;
				Children = children;
			}
			public string Foo { get; }
			public int Bar { get; }
			public List<SomeObject> Children { get; }
			public override string ToString() => $"Object with {Foo}/{Bar} and Childs {Children?.Count ?? 0}";
		}

		public static IEnumerable<object[]> Objects()
		{
			var o1 = new SomeObject("foo", 42, new List<SomeObject>());
			var o2 = new SomeObject("bar", 21, new List<SomeObject> { o1 });
			yield return new object[] { o1 };
			yield return new object[] { o2 };
		}

		[Theory]
		[MemberData(nameof(Objects))]
		public void MethodSource(SomeObject someObject)
		{
			// call the code to test that should do some work when getting one of the objects
			Assert.NotNull(someObject);
		}

		public static IEnumerable<object[]> PrimeNumbersUpTo1000()
		{
			foreach (var prime in PrimeNumberUpTo1000Generator.GetPrimesUpTo1000())
			{
				yield return new object[] { prime };
			}
		}

		[Theory]
		[MemberData(nameof(PrimeNumbersUpTo1000))]
		public void ArgumentSource(int primeNumber)
		{
			// call the code to test that should do some work when getting one of the prime numbers
			Assert.True(PrimeNumberUpTo1000Generator.IsPrime(primeNumber));
		}

		[Theory]
		[InlineData("test", "TEST")]
		[InlineData("tEst", "TEST")]
		[InlineData("Java", "JAVA")]
		public void CsvSource(string input, string expected)
		{
			// let's consider String#toUpperCase to be the code to test
			var actualValue = input.ToUpperInvariant();
			Assert.Equal(expected, actualValue);
		}

		[Theory]
		[InlineData("test          ", "TEST")]
		[InlineData("tEst          ", "TEST")]
		[InlineData("Java          ", "JAVA")]
		[InlineData("loNGER sTriNg ", "LONGER STRING")]
		public void CsvSourceWithDelimiterFormatedAndIncludingWhitespaces(string input, string expected)
		{
			// let's consider String#toUpperCase to be the code to test
			var actualValue = input.ToUpperInvariant().Trim();
			Assert.Equal(expected, actualValue);
		}

		public static IEnumerable<object[]> CsvFileSourceData()
		{
			// Simulate file data: col1, col2, col3WithTypeConversion
			yield return new object[] { "test", "TEST", 4 };
			yield return new object[] { "tEst", "TEST", 4 };
			yield return new object[] { "Java", "JAVA", 4 };
		}

		[Theory]
		[MemberData(nameof(CsvFileSourceData))]
		public void CsvFileSource(string col1, string col2, int col3WithTypeConversion)
		{
			// let's consider String#toUpperCase to be the code to test
			var actualValue = col1.ToUpperInvariant();
			Assert.Equal(col2, actualValue);
			Assert.Equal(col3WithTypeConversion, actualValue.Length);
		}
	}
}