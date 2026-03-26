using StringCasing;

namespace StringCasing.Tests;

public class ToPrivateSnakeCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "_the_quick_brown_fox")]
  [InlineData("theQuickBrownFox", "_the_quick_brown_fox")]
  [InlineData("TheQuickBrownFox", "_the_quick_brown_fox")]
  [InlineData("the-quick-brown-fox", "_the_quick_brown_fox")]
  [InlineData("the_quick_brown_fox", "_the_quick_brown_fox")]
  [InlineData("parseHTTPResponse", "_parse_http_response")]
  [InlineData("PERMISSIVE", "_permissive")]
  [InlineData("a", "_a")]
  [InlineData("", "")]
  public void ConvertsToPrivateSnakeCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToPrivateSnakeCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToPrivateSnakeCase());
  }
}