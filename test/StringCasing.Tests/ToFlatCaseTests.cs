using StringCasing;

namespace StringCasing.Tests;

public class ToFlatCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "thequickbrownfox")]
  [InlineData("theQuickBrownFox", "thequickbrownfox")]
  [InlineData("TheQuickBrownFox", "thequickbrownfox")]
  [InlineData("the-quick-brown-fox", "thequickbrownfox")]
  [InlineData("the_quick_brown_fox", "thequickbrownfox")]
  [InlineData("parseHTTPResponse", "parsehttpresponse")]
  [InlineData("item2Count", "item2count")]
  [InlineData("PERMISSIVE", "permissive")]
  [InlineData("a", "a")]
  [InlineData("", "")]
  public void ConvertsToFlatCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToFlatCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToFlatCase());
  }
}