using StringCasing;

namespace StringCasing.Tests;

public class ToDotCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "the.quick.brown.fox")]
  [InlineData("theQuickBrownFox", "the.quick.brown.fox")]
  [InlineData("TheQuickBrownFox", "the.quick.brown.fox")]
  [InlineData("the-quick-brown-fox", "the.quick.brown.fox")]
  [InlineData("the_quick_brown_fox", "the.quick.brown.fox")]
  [InlineData("parseHTTPResponse", "parse.http.response")]
  [InlineData("PERMISSIVE", "permissive")]
  [InlineData("a", "a")]
  [InlineData("", "")]
  public void ConvertsToDotCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToDotCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToDotCase());
  }
}