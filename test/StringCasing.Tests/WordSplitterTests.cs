using StringCasing;

namespace StringCasing.Tests;

public class WordSplitterTests
{
  private static List<string> SplitWords(string? input)
  {
    List<string> words = new();
    if (input is null)
    {
      return words;
    }

    WordSplitter splitter = new(input.AsSpan());
    while (splitter.MoveNext())
    {
      words.Add(splitter.Current.ToString());
    }

    return words;
  }

  [Fact]
  public void SplitsPlainEnglish()
  {
    List<string> words = SplitWords("The Quick Brown Fox");
    Assert.Equal(["The", "Quick", "Brown", "Fox"], words);
  }

  [Fact]
  public void SplitsCamelCase()
  {
    List<string> words = SplitWords("theQuickBrownFox");
    Assert.Equal(["the", "Quick", "Brown", "Fox"], words);
  }

  [Fact]
  public void SplitsPascalCase()
  {
    List<string> words = SplitWords("TheQuickBrownFox");
    Assert.Equal(["The", "Quick", "Brown", "Fox"], words);
  }

  [Fact]
  public void SplitsKebabCase()
  {
    List<string> words = SplitWords("the-quick-brown-fox");
    Assert.Equal(["the", "quick", "brown", "fox"], words);
  }

  [Fact]
  public void SplitsSnakeCase()
  {
    List<string> words = SplitWords("the_quick_brown_fox");
    Assert.Equal(["the", "quick", "brown", "fox"], words);
  }

  [Fact]
  public void SplitsMacroCase()
  {
    List<string> words = SplitWords("THE_QUICK_BROWN_FOX");
    Assert.Equal(["THE", "QUICK", "BROWN", "FOX"], words);
  }

  [Fact]
  public void SplitsDotCase()
  {
    List<string> words = SplitWords("the.quick.brown.fox");
    Assert.Equal(["the", "quick", "brown", "fox"], words);
  }

  [Fact]
  public void SplitsAcronymBeforeLowercase()
  {
    List<string> words = SplitWords("parseHTTPResponse");
    Assert.Equal(["parse", "HTTP", "Response"], words);
  }

  [Fact]
  public void SplitsAcronymAtEnd()
  {
    List<string> words = SplitWords("useHTTP");
    Assert.Equal(["use", "HTTP"], words);
  }

  [Fact]
  public void SplitsDigitBoundaries()
  {
    List<string> words = SplitWords("item2Count");
    Assert.Equal(["item", "2", "Count"], words);
  }

  [Fact]
  public void SplitsTrailingDigits()
  {
    List<string> words = SplitWords("example321");
    Assert.Equal(["example", "321"], words);
  }

  [Fact]
  public void SplitsLeadingDigits()
  {
    List<string> words = SplitWords("123abc");
    Assert.Equal(["123", "abc"], words);
  }

  [Fact]
  public void HandlesEmptyString()
  {
    List<string> words = SplitWords("");
    Assert.Empty(words);
  }

  [Fact]
  public void HandlesNull()
  {
    List<string> words = SplitWords(null);
    Assert.Empty(words);
  }

  [Fact]
  public void HandlesWhitespaceOnly()
  {
    List<string> words = SplitWords("   ");
    Assert.Empty(words);
  }

  [Fact]
  public void HandlesSingleChar()
  {
    List<string> words = SplitWords("a");
    Assert.Equal(["a"], words);
  }

  [Fact]
  public void HandlesAllCapsWord()
  {
    List<string> words = SplitWords("PERMISSIVE");
    Assert.Equal(["PERMISSIVE"], words);
  }

  [Fact]
  public void HandlesConsecutiveSeparators()
  {
    List<string> words = SplitWords("the--quick__brown");
    Assert.Equal(["the", "quick", "brown"], words);
  }

  [Fact]
  public void HandlesLeadingUnderscore()
  {
    List<string> words = SplitWords("_privateField");
    Assert.Equal(["private", "Field"], words);
  }

  [Fact]
  public void HandlesMixedSeparators()
  {
    List<string> words = SplitWords("the-quick_brown.fox jumps");
    Assert.Equal(["the", "quick", "brown", "fox", "jumps"], words);
  }

  [Fact]
  public void HandlesMultipleAcronyms()
  {
    List<string> words = SplitWords("XMLToJSONParser");
    Assert.Equal(["XML", "To", "JSON", "Parser"], words);
  }

  [Fact]
  public void HandlesSingleLetterWords()
  {
    List<string> words = SplitWords("aBC");
    Assert.Equal(["a", "BC"], words);
  }
}