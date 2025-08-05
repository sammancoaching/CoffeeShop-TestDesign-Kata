CoffeeShop Test Design Kata
===========================


## Learning to write Parameterized Tests

This code base is designed to be used with this learning hour on [Parameterized Tests](https://sammancoaching.org/learning_hours/test_design/parameterized_tests.html)

In the learning hour description, there's the ```do```section that requests you to create some tests with a lot of duplication where one of the tests should be failing. So that's the code base where the ```do``` is already done, and you can focus on parameterized tests.

Note: There's a bug in the implementation, (marked with the comment "Bug!"), so we have a failing test here (see learning hour description for more information on why that is).

In each language directory there's a ```concept``` sub-package/directory that show-cases a test-framework's features for writing parameterized tests.
- [JUnit 5](https://junit.org/junit5/) for Java
- [pytest](https://docs.pytest.org/en/stable/) for Python
- [Swift Testing](https://developer.apple.com/documentation/testing) for Swift
- [bun](https://bun.sh/docs/cli/test) for typescript
- [xUnit Theory](https://xunit.net/docs/getting-started/v3/getting-started#write-your-first-theory) for C#

## Notes for contributors

We would welcome pull requests with additional languages. Please include the bug that causes failing tests. If possible please also include a 'concept' example for your favourite test framework.

## Acknowledgements

This code katas was created at Atruvia and originally published on github as part of a collection of [samman coaching katas](https://github.com/atruvia/samman-coaching-katas). 
