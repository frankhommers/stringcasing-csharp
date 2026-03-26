using StringCasing;

namespace StringCasing.Tests;

public class ToTitleCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "The Quick Brown Fox")]
  [InlineData("theQuickBrownFox", "The Quick Brown Fox")]
  [InlineData("TheQuickBrownFox", "The Quick Brown Fox")]
  [InlineData("the-quick-brown-fox", "The Quick Brown Fox")]
  [InlineData("the_quick_brown_fox", "The Quick Brown Fox")]
  [InlineData("parseHTTPResponse", "Parse Http Response")]
  [InlineData("PERMISSIVE", "Permissive")]
  [InlineData("a", "A")]
  [InlineData("", "")]
  public void ConvertsToTitleCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToTitleCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToTitleCase());
  }
}