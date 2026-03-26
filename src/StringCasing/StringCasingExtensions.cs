using System.Buffers;

namespace StringCasing;

internal static class StringCasingExtensions
{
  private const int StackAllocThreshold = 256;

  public static string? ToPascalCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Title, WordCasing.Title, '\0');
  }

  public static string? ToCamelCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Lower, WordCasing.Title, '\0');
  }

  public static string? ToDotNetPascalCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.DotNetTitle, WordCasing.DotNetTitle, '\0');
  }

  public static string? ToDotNetCamelCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Lower, WordCasing.DotNetTitle, '\0');
  }

  public static string? ToKebabCase(this string? input)
  {
    return ConvertCase(input, '-', true, WordCasing.Lower, WordCasing.Lower, '\0');
  }

  public static string? ToSnakeCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Lower, WordCasing.Lower, '\0');
  }

  public static string? ToCobolCase(this string? input)
  {
    return ConvertCase(input, '-', true, WordCasing.Upper, WordCasing.Upper, '\0');
  }

  public static string? ToMacroCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Upper, WordCasing.Upper, '\0');
  }

  public static string? ToTrainCase(this string? input)
  {
    return ConvertCase(input, '-', true, WordCasing.Title, WordCasing.Title, '\0');
  }

  public static string? ToTitleCase(this string? input)
  {
    return ConvertCase(input, ' ', true, WordCasing.Title, WordCasing.Title, '\0');
  }

  public static string? ToTitleSnakeCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Title, WordCasing.Title, '\0');
  }

  public static string? ToPrivateSnakeCase(this string? input)
  {
    return ConvertCase(input, '_', true, WordCasing.Lower, WordCasing.Lower, '_');
  }

  public static string? ToUnderscoreCamelCase(this string? input)
  {
    return ConvertCase(input, '\0', false, WordCasing.Lower, WordCasing.Title, '_');
  }

  public static string? ToDotCase(this string? input)
  {
    return ConvertCase(input, '.', true, WordCasing.Lower, WordCasing.Lower, '\0');
  }

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
}