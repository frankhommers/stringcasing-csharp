using StringCasing;

namespace StringCasing.Tests;

public class ToMacroCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "THE_QUICK_BROWN_FOX")]
  [InlineData("theQuickBrownFox", "THE_QUICK_BROWN_FOX")]
  [InlineData("TheQuickBrownFox", "THE_QUICK_BROWN_FOX")]
  [InlineData("the-quick-brown-fox", "THE_QUICK_BROWN_FOX")]
  [InlineData("the_quick_brown_fox", "THE_QUICK_BROWN_FOX")]
  [InlineData("parseHTTPResponse", "PARSE_HTTP_RESPONSE")]
  [InlineData("PERMISSIVE", "PERMISSIVE")]
  [InlineData("a", "A")]
  [InlineData("", "")]
  public void ConvertsToMacroCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToMacroCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToMacroCase());
  }
}