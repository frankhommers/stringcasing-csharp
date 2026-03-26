using StringCasing;

namespace StringCasing.Tests;

public class ToSnakeCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "the_quick_brown_fox")]
  [InlineData("theQuickBrownFox", "the_quick_brown_fox")]
  [InlineData("TheQuickBrownFox", "the_quick_brown_fox")]
  [InlineData("the-quick-brown-fox", "the_quick_brown_fox")]
  [InlineData("the_quick_brown_fox", "the_quick_brown_fox")]
  [InlineData("THE_QUICK_BROWN_FOX", "the_quick_brown_fox")]
  [InlineData("the.quick.brown.fox", "the_quick_brown_fox")]
  [InlineData("parseHTTPResponse", "parse_http_response")]
  [InlineData("item2Count", "item_2_count")]
  [InlineData("PERMISSIVE", "permissive")]
  [InlineData("a", "a")]
  [InlineData("", "")]
  [InlineData("   ", "")]
  public void ConvertsToSnakeCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToSnakeCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToSnakeCase());
  }
}