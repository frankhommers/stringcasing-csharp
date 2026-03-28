using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace StringCasing;

internal static class StringCasingExtensions
{
  private const int StackAllocThreshold = 256;

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToPascalCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Title, WordCasing.Title, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToCamelCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Lower, WordCasing.Title, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToDotNetPascalCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.DotNetTitle, WordCasing.DotNetTitle, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToDotNetCamelCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Lower, WordCasing.DotNetTitle, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToKebabCase(this string? input)
  {
    return ConvertCase(input, '-', true, WordCasing.Lower, WordCasing.Lower, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToSnakeCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Lower, WordCasing.Lower, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToCobolCase(this string? input)
  {
    return ConvertCase(input, '-', true, WordCasing.Upper, WordCasing.Upper, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToMacroCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Upper, WordCasing.Upper, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToTrainCase(this string? input)
  {
    return ConvertCase(input, '-', true, WordCasing.Title, WordCasing.Title, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToTitleCase(this string? input)
  {
    return ConvertCase(input, ' ', true, WordCasing.Title, WordCasing.Title, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToTitleSnakeCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Title, WordCasing.Title, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToPrivateSnakeCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Lower, WordCasing.Lower, '_');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToUnderscoreCamelCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Lower, WordCasing.Title, '_');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToDotCase(this string? input)
  {
    return ConvertCase(input, '.', true, WordCasing.Lower, WordCasing.Lower, '\0');
  }

  [return: NotNullIfNotNull(nameof(input))]
  public static string? ToFlatCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Lower, WordCasing.Lower, '\0');
  }

  private static string? ConvertCase(
    string? input,
    char separator,
    bool hasSeparator,
    WordCasing firstWordCasing,
    WordCasing restWordCasing,
    char prefix)
  {
    if (input is null)
    {
      return null;
    }

    if (input.Length == 0)
    {
      return string.Empty;
    }

    ReadOnlySpan<char> source = input.AsSpan();

    // Calculate maximum possible output length
    int maxLen = source.Length * 2 + 1; // worst case: separators between every char + prefix
    char[]? rented = null;
    Span<char> buffer = maxLen <= StackAllocThreshold
      ? stackalloc char[StackAllocThreshold]
      : rented = ArrayPool<char>.Shared.Rent(maxLen);

    try
    {
      int written = 0;

      // Write prefix if specified
      if (prefix != 0)
      {
        buffer[written++] = prefix;
      }

      WordSplitter splitter = new(source);
      bool isFirst = true;

      while (splitter.MoveNext())
      {
        ReadOnlySpan<char> word = splitter.Current;
        if (word.IsEmpty)
        {
          continue;
        }

        // Add separator between words
        if (!isFirst && hasSeparator)
        {
          buffer[written++] = separator;
        }

        WordCasing casing = isFirst ? firstWordCasing : restWordCasing;
        WriteWord(word, buffer, ref written, casing);

        isFirst = false;
      }

      if (written == 0)
      {
        return string.Empty;
      }

      return new string(buffer[..written]);
    }
    finally
    {
      if (rented is not null)
      {
        ArrayPool<char>.Shared.Return(rented);
      }
    }
  }

  private static void WriteWord(ReadOnlySpan<char> word, Span<char> buffer, ref int pos, WordCasing casing)
  {
    switch (casing)
    {
      case WordCasing.Lower:
        for (int i = 0; i < word.Length; i++)
        {
          buffer[pos++] = char.ToLowerInvariant(word[i]);
        }

        break;

      case WordCasing.Upper:
        for (int i = 0; i < word.Length; i++)
        {
          buffer[pos++] = char.ToUpperInvariant(word[i]);
        }

        break;

      case WordCasing.Title:
        buffer[pos++] = char.ToUpperInvariant(word[0]);
        for (int i = 1; i < word.Length; i++)
        {
          buffer[pos++] = char.ToLowerInvariant(word[i]);
        }

        break;

      case WordCasing.DotNetTitle:
        // MS convention: 2-letter all-uppercase acronyms stay uppercase
        if (word.Length == 2 && char.IsUpper(word[0]) && char.IsUpper(word[1]))
        {
          buffer[pos++] = word[0];
          buffer[pos++] = word[1];
        }
        else
        {
          buffer[pos++] = char.ToUpperInvariant(word[0]);
          for (int i = 1; i < word.Length; i++)
          {
            buffer[pos++] = char.ToLowerInvariant(word[i]);
          }
        }

        break;
    }
  }

  private enum WordCasing
  {
    Lower,
    Upper,
    Title,
    DotNetTitle,
  }

  /// <summary>
  /// A zero-allocation ref struct that splits a string into words by detecting
  /// case transitions, digit boundaries, and separator characters.
  /// </summary>
  private ref struct WordSplitter
  {
    private readonly ReadOnlySpan<char> _source;
    private int _position;
    private ReadOnlySpan<char> _current;

    public WordSplitter(ReadOnlySpan<char> source)
    {
      _source = source;
      _position = 0;
      _current = default;
    }

    public readonly ReadOnlySpan<char> Current => _current;

    public bool MoveNext()
    {
      // Skip separators
      while (_position < _source.Length && IsSeparator(_source[_position]))
      {
        _position++;
      }

      if (_position >= _source.Length)
      {
        _current = default;
        return false;
      }

      int start = _position;
      char first = _source[start];

      if (char.IsDigit(first))
      {
        // Consume all consecutive digits — digits are always their own word
        while (_position < _source.Length && char.IsDigit(_source[_position]))
        {
          _position++;
        }
      }
      else if (char.IsUpper(first))
      {
        _position++;

        if (_position < _source.Length && char.IsUpper(_source[_position]))
        {
          // Uppercase run (potential acronym)
          while (_position < _source.Length && char.IsUpper(_source[_position]))
          {
            // Look ahead: if next char is lowercase, this uppercase starts a new word
            if (_position + 1 < _source.Length && char.IsLower(_source[_position + 1]))
            {
              break;
            }

            _position++;
          }
        }
        else
        {
          // Single uppercase followed by lowercase (normal PascalCase word)
          while (_position < _source.Length && char.IsLower(_source[_position]))
          {
            _position++;
          }
        }
      }
      else
      {
        // Lowercase run
        _position++;
        while (_position < _source.Length && char.IsLower(_source[_position]))
        {
          _position++;
        }
      }

      _current = _source[start.._position];
      return true;
    }

    private static bool IsSeparator(char c)
    {
      return c is '-' or '_' or '.' or ' ' or '\t' or '\r' or '\n';
    }
  }
}
