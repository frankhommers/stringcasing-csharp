using StringCasing;

namespace StringCasing.Tests;

public class ToKebabCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "the-quick-brown-fox")]
  [InlineData("theQuickBrownFox", "the-quick-brown-fox")]
  [InlineData("TheQuickBrownFox", "the-quick-brown-fox")]
  [InlineData("the-quick-brown-fox", "the-quick-brown-fox")]
  [InlineData("the_quick_brown_fox", "the-quick-brown-fox")]
  [InlineData("THE_QUICK_BROWN_FOX", "the-quick-brown-fox")]
  [InlineData("the.quick.brown.fox", "the-quick-brown-fox")]
  [InlineData("parseHTTPResponse", "parse-http-response")]
  [InlineData("item2Count", "item-2-count")]
  [InlineData("PERMISSIVE", "permissive")]
  [InlineData("a", "a")]
  [InlineData("", "")]
  [InlineData("   ", "")]
  public void ConvertsToKebabCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToKebabCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToKebabCase());
  }
}