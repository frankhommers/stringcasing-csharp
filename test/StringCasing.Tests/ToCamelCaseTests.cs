using StringCasing;

namespace StringCasing.Tests;

public class ToCamelCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "theQuickBrownFox")]
  [InlineData("theQuickBrownFox", "theQuickBrownFox")]
  [InlineData("TheQuickBrownFox", "theQuickBrownFox")]
  [InlineData("the-quick-brown-fox", "theQuickBrownFox")]
  [InlineData("the_quick_brown_fox", "theQuickBrownFox")]
  [InlineData("THE_QUICK_BROWN_FOX", "theQuickBrownFox")]
  [InlineData("the.quick.brown.fox", "theQuickBrownFox")]
  [InlineData("parseHTTPResponse", "parseHttpResponse")]
  [InlineData("item2Count", "item2Count")]
  [InlineData("PERMISSIVE", "permissive")]
  [InlineData("a", "a")]
  [InlineData("", "")]
  [InlineData("   ", "")]
  public void ConvertsToCamelCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToCamelCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToCamelCase());
  }
}