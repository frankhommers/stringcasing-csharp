using StringCasing;

namespace StringCasing.Tests;

public class ToDotNetCamelCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "theQuickBrownFox")]
  [InlineData("theQuickBrownFox", "theQuickBrownFox")]
  [InlineData("TheQuickBrownFox", "theQuickBrownFox")]
  [InlineData("the-quick-brown-fox", "theQuickBrownFox")]
  [InlineData("the_quick_brown_fox", "theQuickBrownFox")]
  [InlineData("THE_QUICK_BROWN_FOX", "theQuickBrownFox")]
  [InlineData("parseHTTPResponse", "parseHttpResponse")]
  [InlineData("parseIOStream", "parseIOStream")]
  [InlineData("getDBConnection", "getDbConnection")]
  [InlineData("IOStream", "ioStream")]
  [InlineData("DBConnection", "dbConnection")]
  [InlineData("UIElement", "uiElement")]
  [InlineData("getUserID", "getUserId")]
  [InlineData("isOK", "isOk")]
  [InlineData("calculatePI", "calculatePi")]
  [InlineData("PERMISSIVE", "permissive")]
  [InlineData("item2Count", "item2Count")]
  [InlineData("a", "a")]
  [InlineData("", "")]
  [InlineData("   ", "")]
  public void ConvertsToDotNetCamelCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToDotNetCamelCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToDotNetCamelCase());
  }
}