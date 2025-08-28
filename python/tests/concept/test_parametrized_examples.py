"""Examples of pytest parametrization features for learning."""

import csv
import math
from pathlib import Path
from typing import Any

import pytest


class TestBasicParametrization:
    """Basic @pytest.mark.parametrize examples."""

    @pytest.mark.parametrize("value", ["foo", "bar", "###", "___", "   "])
    def test_with_string_values(self, value: str) -> None:
        assert isinstance(value, str)
        assert len(value) > 0

    @pytest.mark.parametrize("value", ["foo", "bar", "", None])
    def test_with_strings_including_empty_and_none(self, value: str | None) -> None:
        if value is not None:
            assert isinstance(value, str)

    @pytest.mark.parametrize("number", [1, 2, 3, 7, 8, 9, 0, -1, -2, -2147483648])
    def test_with_integers(self, number: int) -> None:
        assert isinstance(number, int)


class TestMultipleParameters:
    """Examples with multiple parameters."""

    @pytest.mark.parametrize(
        "input_str,expected",
        [
            ("test", "TEST"),
            ("tEst", "TEST"),
            ("Java", "JAVA"),
            ("Python", "PYTHON"),
        ],
    )
    def test_string_uppercase(self, input_str: str, expected: str) -> None:
        assert input_str.upper() == expected

    @pytest.mark.parametrize(
        "a,b,expected",
        [
            (2, 3, 5),
            (0, 0, 0),
            (-1, 1, 0),
            (100, 200, 300),
        ],
    )
    def test_addition(self, a: int, b: int, expected: int) -> None:
        assert a + b == expected


class TestParametrizeWithIds:
    """Using ids for better test names."""

    @pytest.mark.parametrize(
        "value,expected",
        [
            ("hello", 5),
            ("", 0),
            ("pytest", 6),
        ],
        ids=["normal_string", "empty_string", "framework_name"],
    )
    def test_string_length_with_ids(self, value: str, expected: int) -> None:
        assert len(value) == expected


class TestParametrizeCartesianProduct:
    """Combining multiple parametrize decorators."""

    @pytest.mark.parametrize("x", [1, 2, 3])
    @pytest.mark.parametrize("y", [10, 20])
    def test_cartesian_product(self, x: int, y: int) -> None:
        """This creates 6 test cases: (1,10), (1,20), (2,10), (2,20), (3,10), (3,20)"""
        assert x < y


class TestPytestParam:
    """Using pytest.param for advanced features."""

    @pytest.mark.parametrize(
        "test_input,expected",
        [
            pytest.param("3+5", 8, id="addition"),
            pytest.param("2*4", 8, id="multiplication"),
            pytest.param("6-2", 4, id="subtraction"),
            pytest.param("1/0", 0, marks=pytest.mark.xfail(raises=ZeroDivisionError)),
        ],
    )
    def test_eval_expressions(self, test_input: str, expected: int) -> None:
        assert eval(test_input) == expected

    @pytest.mark.parametrize(
        "value",
        [
            pytest.param(
                -1, marks=pytest.mark.skip(reason="negative numbers not supported")
            ),
            pytest.param(0),
            pytest.param(1),
            pytest.param(4),
            pytest.param(9),
        ],
    )
    def test_square_root(self, value: int) -> None:
        result = math.sqrt(value)
        assert result * result == value


class TestIndirectParametrization:
    """Using indirect=True for fixture parametrization."""

    @pytest.fixture
    def squared_number(self, request: pytest.FixtureRequest) -> int:
        """Fixture that squares the input parameter."""
        return int(request.param**2)

    @pytest.mark.parametrize(
        "squared_number,expected",
        [
            (2, 4),
            (3, 9),
            (4, 16),
            (5, 25),
        ],
        indirect=["squared_number"],
    )
    def test_with_fixture(self, squared_number: int, expected: int) -> None:
        assert squared_number == expected


def generate_prime_numbers(max_value: int = 100) -> list[int]:
    """Generate prime numbers up to max_value."""

    def is_prime(n: int) -> bool:
        if n < 2:
            return False
        for i in range(2, int(n**0.5) + 1):
            if n % i == 0:
                return False
        return True

    return [n for n in range(2, max_value) if is_prime(n)]


class TestParametrizeWithFunction:
    """Using functions to generate test data."""

    @pytest.mark.parametrize("prime", generate_prime_numbers(50))
    def test_prime_numbers(self, prime: int) -> None:
        # Test that dividing by anything except 1 and itself has remainder
        for divisor in range(2, prime):
            assert prime % divisor != 0


class TestParametrizeFromFile:
    """Reading test data from files."""

    @staticmethod
    def load_csv_data() -> list[tuple[str, str, int]]:
        """Load test data from CSV file."""
        csv_path = Path(__file__).parent / "test_data.csv"

        with open(csv_path) as f:
            reader = csv.reader(f)
            next(reader)  # Skip header
            return [(row[0], row[1], int(row[2])) for row in reader]

    @pytest.mark.parametrize("input_str,expected_upper,expected_len", load_csv_data())
    def test_from_csv_data(
        self, input_str: str, expected_upper: str, expected_len: int
    ) -> None:
        assert input_str.upper() == expected_upper
        assert len(input_str) == expected_len


class TestParametrizeClasses:
    """Parametrizing entire test classes."""

    @pytest.mark.parametrize("value", [1, 2, 3])
    class TestNumberOperations:
        """All methods in this class will be parametrized."""

        def test_is_positive(self, value: int) -> None:
            assert value > 0

        def test_square(self, value: int) -> None:
            assert value * value > 0

        def test_double(self, value: int) -> None:
            assert value * 2 > value


class TestParametrizePython310:
    """Examples using Python 3.10+ features."""

    @pytest.mark.parametrize(
        "test_input,expected",
        [
            (1, "one"),
            (2, "two"),
            (3, "three"),
            (99, "other"),
        ],
    )
    def test_match_statement(self, test_input: int, expected: str) -> None:
        match test_input:
            case 1:
                result = "one"
            case 2:
                result = "two"
            case 3:
                result = "three"
            case _:
                result = "other"
        assert result == expected


class TestParametrizeSubtests:
    """Using parametrize with subtests style."""

    def test_multiple_assertions_parametrized(self) -> None:
        """Alternative to parametrize using simple loops - not recommended but shown for completeness."""
        test_cases = [
            ("hello", 5),
            ("world", 5),
            ("pytest", 6),
        ]

        for text, expected_length in test_cases:
            # Each assertion is independent
            assert len(text) == expected_length, f"Failed for {text}"


class TestComplexParametrization:
    """Advanced parametrization examples."""

    @pytest.mark.parametrize(
        "config",
        [
            {"name": "test1", "value": 10, "enabled": True},
            {"name": "test2", "value": 20, "enabled": False},
            {"name": "test3", "value": 30, "enabled": True},
        ],
    )
    def test_with_dict_params(self, config: dict[str, Any]) -> None:
        assert isinstance(config["name"], str)
        assert isinstance(config["value"], int)
        assert isinstance(config["enabled"], bool)

        if config["enabled"]:
            assert config["value"] > 0


def pytest_generate_tests(metafunc: pytest.Metafunc) -> None:
    """Dynamic parametrization using pytest_generate_tests hook."""
    if "dynamic_value" in metafunc.fixturenames:
        # Generate different values based on some condition
        if metafunc.config.getoption("verbose") > 0:
            values = [1, 2, 3, 4, 5]
        else:
            values = [1, 2, 3]
        metafunc.parametrize("dynamic_value", values)


class TestDynamicParametrization:
    """Tests using dynamic parametrization."""

    def test_dynamic_values(self, dynamic_value: int) -> None:
        """This test will have different parameters based on pytest invocation."""
        assert dynamic_value > 0
        assert dynamic_value <= 5
