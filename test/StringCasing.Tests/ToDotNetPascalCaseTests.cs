using StringCasing;

namespace StringCasing.Tests;

public class ToDotNetPascalCaseTests
{
  [Theory]
  [InlineData("The Quick Brown Fox", "TheQuickBrownFox")]
  [InlineData("theQuickBrownFox", "TheQuickBrownFox")]
  [InlineData("TheQuickBrownFox", "TheQuickBrownFox")]
  [InlineData("the-quick-brown-fox", "TheQuickBrownFox")]
  [InlineData("the_quick_brown_fox", "TheQuickBrownFox")]
  [InlineData("THE_QUICK_BROWN_FOX", "TheQuickBrownFox")]
  [InlineData("parseHTTPResponse", "ParseHttpResponse")]
  [InlineData("parseIOStream", "ParseIOStream")]
  [InlineData("getDBConnection", "GetDbConnection")]
  [InlineData("getUIElement", "GetUIElement")]
  [InlineData("IOStream", "IOStream")]
  [InlineData("DBConnection", "DbConnection")]
  [InlineData("getUserID", "GetUserId")]
  [InlineData("isOK", "IsOk")]
  [InlineData("calculatePI", "CalculatePi")]
  [InlineData("PERMISSIVE", "Permissive")]
  [InlineData("SIG", "Sig")]
  [InlineData("item2Count", "Item2Count")]
  [InlineData("a", "A")]
  [InlineData("", "")]
  [InlineData("   ", "")]
  public void ConvertsToDotNetPascalCase(string input, string expected)
  {
    Assert.Equal(expected, input.ToDotNetPascalCase());
  }

  [Fact]
  public void HandlesNull()
  {
    Assert.Null(((string?) null).ToDotNetPascalCase());
  }
}