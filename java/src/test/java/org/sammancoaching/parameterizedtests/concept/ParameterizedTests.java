package org.sammancoaching.parameterizedtests.concept;

import static java.util.Collections.emptyList;
import static org.assertj.core.api.Assertions.assertThat;
import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.List;

import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ArgumentsSource;
import org.junit.jupiter.params.provider.CsvFileSource;
import org.junit.jupiter.params.provider.CsvSource;
import org.junit.jupiter.params.provider.EnumSource;
import org.junit.jupiter.params.provider.FieldSource;
import org.junit.jupiter.params.provider.MethodSource;
import org.junit.jupiter.params.provider.NullAndEmptySource;
import org.junit.jupiter.params.provider.ValueSource;

class ParameterizedTests {

	@ParameterizedTest
	@ValueSource(strings = { "foo", "bar", "###", "___", "   " })
	void valueSourceWithStrings(String string) {
		// call the code to test that should do some work when getting a string
	}

	@ParameterizedTest
	@ValueSource(strings = { "foo", "bar", "###", "___", "   " })
	@NullAndEmptySource
	void valueSourceWithStringsIncludingNullAndEmptyString(String string) {
		// call the code to test that should do some work when getting a string
		// the code also should work for null or empty strings
	}

	@ParameterizedTest
	@ValueSource(ints = { 1, 2, 3, 7, 8, 9, 0, -1, -2, Integer.MIN_VALUE })
	void valueSourceWithInts(int intValue) {
		// call the code to test that should do some work when getting a value <10
	}

	private enum MyEnum {
		FOO, BAR, FOOBAR;
	}

	@ParameterizedTest
	@EnumSource
	void enumSource(MyEnum myEnum) {
		// call the code to test that should do some work when getting the enum value
	}

	private static class SomeObject {

		private final String foo;
		private final int bar;

		private SomeObject(String foo, int bar) {
			this.foo = foo;
			this.bar = bar;
		}

		// additional code can be placed here

		@Override
		public String toString() {
			return String.format("SomeObject [foo=%s, bar=%d]", foo, bar);
		}

	}

	// Array, Collection or Stream
	private static List<SomeObject> objects() {
		return List.of( //
				new SomeObject("A", 42), //
				new SomeObject("B", 21) //
		);
	}

	@ParameterizedTest
	@MethodSource("objects")
	void methodSource(SomeObject someObject) {
		// call the code to test that should do some work when getting one of the
		// objects
	}

	// Array, Collection or Stream
	static List<SomeObject> objects = List.of( //
			new SomeObject("A", 42), //
			new SomeObject("B", 21) //
	);

	@ParameterizedTest
	@FieldSource("objects")
	void fieldSource(SomeObject someObject) {
		// call the code to test that should do some work when getting one of the
		// objects
	}

	@ParameterizedTest
	@ArgumentsSource(PrimeNumberUpTo1000Generator.class)
	void argumentSource(int primeNumber) {
		// call the code to test that should do some work when getting one of the
		// prime numbers
	}

	@ParameterizedTest
	@CsvSource({ "test,TEST", "tEst,TEST", "Java,JAVA" })
	void csvSource(String input, String expected) {
		// let's consider String#toUpperCase to be the code to test
		String actualValue = input.toUpperCase();
		assertEquals(expected, actualValue);
	}

	@ParameterizedTest
	@CsvSource(value = { //
			"test          | TEST", //
			"tEst          | TEST", //
			"Java          | JAVA", //
			"loNGER sTriNg | LONGER STRING" //
	}, delimiter = '|')
	void csvSourceWithDelimiterFormatedAndIncludingWhitespaces(String input, String expected) {
		// let's consider String#toUpperCase to be the code to test
		String actualValue = input.toUpperCase();
		assertEquals(expected, actualValue);
	}

	@CsvSource(textBlock = """
			test          | TEST
			tEst          | TEST
			Java          | JAVA
			loNGER sTriNg | LONGER STRING
			""", delimiter = '|')
	void csvSourceWithDelimiterUsingTextblock(String input, String expected) {
		// let's consider String#toUpperCase to be the code to test
		String actualValue = input.toUpperCase();
		assertEquals(expected, actualValue);
	}

	@ParameterizedTest
	// can use files and/or resources
	@CsvFileSource(resources = "/example.csv", delimiter = ';', numLinesToSkip = 1)
	void csvFileSource(String col1, String col2, int col3WithTypeConversion) {
		// let's consider String#toUpperCase to be the code to test
		String actualValue = col1.toUpperCase();
		assertThat(actualValue).isEqualTo(col2).hasSize(col3WithTypeConversion);
	}

}
