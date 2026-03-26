using StringCasing;

namespace StringCasing.Tests;

public class ToUnderscoreCamelCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "_theQuickBrownFox")]
  [InlineData("theQuickBrownFox", "_theQuickBrownFox")]
  [InlineData("TheQuickBrownFox", "_theQuickBrownFox")]
  [InlineData("the-quick-brown-fox", "_theQuickBrownFox")]
  [InlineData("the_quick_brown_fox", "_theQuickBrownFox")]
  [InlineData("parseHTTPResponse", "_parseHttpResponse")]
  [InlineData("PERMISSIVE", "_permissive")]
  [InlineData("a", "_a")]
  [InlineData("", "")]
  public void ConvertsToUnderscoreCamelCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToUnderscoreCamelCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToUnderscoreCamelCase());
  }
}