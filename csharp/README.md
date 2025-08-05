# CoffeeShop Test Design Kata – C#/.NET/xUnit

This directory contains the C# implementation of the CoffeeShop Test Design Kata, using xUnit for parameterized tests.

## Running the Tests

1. Ensure you have the .NET SDK installed (https://dotnet.microsoft.com/download).
2. Navigate to this directory in your terminal:
   ```sh
   cd csharp/src/SammanCoaching.ParameterizedTests.Tests
   ```
3. Run the tests with:
   ```sh
   dotnet test
   ```

## Example: Parameterized Test in xUnit

```csharp
using Xunit;

public class ExampleTests
{
    [Theory]
    [InlineData("foo")]
    [InlineData("bar")]
    public void TestWithParameters(string value)
    {
        Assert.NotNull(value);
    }
}
```

- Use `[Theory]` and `[InlineData]` for simple parameterized tests.
- Use `[MemberData]` or `[ClassData]` for more complex data sources.
- Prefer PascalCase for class and method names.
- Use `Assert.Equal`, `Assert.True`, etc. for assertions.

## Notes
- See the `concept/` directory for idiomatic xUnit parameterized test examples.
- The implementation may contain a bug (see the kata instructions for details).

---

For more information, see the root `README.md` and the [xUnit documentation](https://xunit.net/docs/getting-started/netcore/cmdline).
