using StringCasing;

namespace StringCasing.Tests;

public class ToTitleSnakeCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "The_Quick_Brown_Fox")]
  [InlineData("theQuickBrownFox", "The_Quick_Brown_Fox")]
  [InlineData("TheQuickBrownFox", "The_Quick_Brown_Fox")]
  [InlineData("the-quick-brown-fox", "The_Quick_Brown_Fox")]
  [InlineData("the_quick_brown_fox", "The_Quick_Brown_Fox")]
  [InlineData("parseHTTPResponse", "Parse_Http_Response")]
  [InlineData("PERMISSIVE", "Permissive")]
  [InlineData("a", "A")]
  [InlineData("", "")]
  public void ConvertsToTitleSnakeCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToTitleSnakeCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToTitleSnakeCase());
  }
}