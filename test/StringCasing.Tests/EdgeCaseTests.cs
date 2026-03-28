using StringCasing;

namespace StringCasing.Tests;

public class EdgeCaseTests
{
  [Theory]
  [InlineData("a")]
  public void SingleCharLowercase_PascalCase(string input)
  {
    Assert.Equal("A", input.ToPascalCase());
  }

  [Theory]
  [InlineData("A")]
  public void SingleCharUppercase_CamelCase(string input)
  {
    Assert.Equal("a", input.ToCamelCase());
  }

  [Fact]
  public void LeadingTrailingSeparators_AreStripped()
  {
    Assert.Equal("the-quick", "--the-quick--".ToKebabCase());
  }

  [Fact]
  public void ConsecutiveSeparators_AreCollapsed()
  {
    Assert.Equal("the_quick_brown", "the___quick___brown".ToSnakeCase());
  }

  [Fact]
  public void MixedSeparators_AreHandled()
  {
    Assert.Equal("TheQuickBrownFox", "the-quick_brown.fox".ToPascalCase());
  }

  [Fact]
  public void NumbersAtBoundaries()
  {
    Assert.Equal("item-2-count-3-value", "item2Count3Value".ToKebabCase());
  }

  [Fact]
  public void AllDigits()
  {
    Assert.Equal("12345", "12345".ToPascalCase());
  }

  [Fact]
  public void MultipleAcronyms()
  {
    Assert.Equal("xml-to-json-parser", "XMLToJSONParser".ToKebabCase());
  }

  [Fact]
  public void AllUppercaseShortWord_TreatedAsSingleWord()
  {
    Assert.Equal("abc", "ABC".ToKebabCase());
  }

  [Fact]
  public void WhitespaceVariants()
  {
    Assert.Equal("the-quick", "the\t\nquick".ToKebabCase());
  }

  [Fact]
  public void PrivateSnakeCase_EmptyReturnsEmpty()
  {
    Assert.Equal("", "".ToPrivateSnakeCase());
  }

  [Fact]
  public void UnderscoreCamelCase_EmptyReturnsEmpty()
  {
    Assert.Equal("", "".ToUnderscoreCamelCase());
  }

  [Fact]
  public void LongString_UsesArrayPool()
  {
    // Generate a string longer than 256 chars (stackalloc threshold)
    string input = string.Join("-", Enumerable.Range(0, 50).Select(i => $"word{i}"));
    string? result = input.ToPascalCase();
    Assert.StartsWith("Word0", result);
    Assert.Contains("Word49", result);
  }

  [Fact]
  public void PreservesDigitSequences()
  {
    Assert.Equal("Get2NdItem", "get2ndItem".ToPascalCase());
  }

  [Fact]
  public void LeadingUnderscore_StrippedInNormalConversions()
  {
    Assert.Equal("private-field", "_privateField".ToKebabCase());
  }

  [Fact]
  public void PrivateSnakeCase_WithAlreadyPrefixed()
  {
    Assert.Equal("_private_field", "_privateField".ToPrivateSnakeCase());
  }

  [Fact]
  public void AllSupportedConversionsFromSameInput()
  {
    const string input = "parseHTTPResponse";

    Assert.Equal("ParseHttpResponse", input.ToPascalCase());
    Assert.Equal("parseHttpResponse", input.ToCamelCase());
    Assert.Equal("parse-http-response", input.ToKebabCase());
    Assert.Equal("PARSE-HTTP-RESPONSE", input.ToCobolCase());
    Assert.Equal("parse_http_response", input.ToSnakeCase());
    Assert.Equal("_parse_http_response", input.ToPrivateSnakeCase());
    Assert.Equal("Parse_Http_Response", input.ToTitleSnakeCase());
    Assert.Equal("PARSE_HTTP_RESPONSE", input.ToMacroCase());
    Assert.Equal("Parse-Http-Response", input.ToTrainCase());
    Assert.Equal("Parse Http Response", input.ToTitleCase());
    Assert.Equal("_parseHttpResponse", input.ToUnderscoreCamelCase());
    Assert.Equal("parse.http.response", input.ToDotCase());
    Assert.Equal("parsehttpresponse", input.ToFlatCase());
  }
}