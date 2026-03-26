using StringCasing;

namespace StringCasing.Tests;

public class ToPascalCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "TheQuickBrownFox")]
  [InlineData("theQuickBrownFox", "TheQuickBrownFox")]
  [InlineData("TheQuickBrownFox", "TheQuickBrownFox")]
  [InlineData("the-quick-brown-fox", "TheQuickBrownFox")]
  [InlineData("the_quick_brown_fox", "TheQuickBrownFox")]
  [InlineData("THE_QUICK_BROWN_FOX", "TheQuickBrownFox")]
  [InlineData("the.quick.brown.fox", "TheQuickBrownFox")]
  [InlineData("parseHTTPResponse", "ParseHttpResponse")]
  [InlineData("item2Count", "Item2Count")]
  [InlineData("PERMISSIVE", "Permissive")]
  [InlineData("a", "A")]
  [InlineData("", "")]
  [InlineData("   ", "")]
  public void ConvertsToPascalCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToPascalCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToPascalCase());
  }
}