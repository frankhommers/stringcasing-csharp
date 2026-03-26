using StringCasing;

namespace StringCasing.Tests;

public class ToCobolCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "THE-QUICK-BROWN-FOX")]
  [InlineData("theQuickBrownFox", "THE-QUICK-BROWN-FOX")]
  [InlineData("TheQuickBrownFox", "THE-QUICK-BROWN-FOX")]
  [InlineData("the-quick-brown-fox", "THE-QUICK-BROWN-FOX")]
  [InlineData("the_quick_brown_fox", "THE-QUICK-BROWN-FOX")]
  [InlineData("parseHTTPResponse", "PARSE-HTTP-RESPONSE")]
  [InlineData("PERMISSIVE", "PERMISSIVE")]
  [InlineData("a", "A")]
  [InlineData("", "")]
  public void ConvertsToCobolCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToCobolCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToCobolCase());
  }
}