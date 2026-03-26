namespace StringCasing;

/// <summary>
/// A zero-allocation ref struct that splits a string into words by detecting
/// case transitions, digit boundaries, and separator characters.
/// </summary>
internal ref struct WordSplitter
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
      // Consume all consecutive digits
      while (_position < _source.Length && char.IsDigit(_source[_position]))
      {
        _position++;
      }

      // Consume a short lowercase suffix (e.g., "nd" in "2nd", "st" in "1st")
      // that is followed by an uppercase letter, separator, digit, or end of input.
      // This keeps ordinal-like sequences as a single word.
      int suffixStart = _position;
      while (_position < _source.Length && char.IsLower(_source[_position])
                                        && _position - suffixStart < 2)
      {
        _position++;
      }

      // If the next char is still lowercase, the suffix is part of a longer word
      // — revert and let the lowercase be its own word.
      if (_position < _source.Length && char.IsLower(_source[_position]))
      {
        _position = suffixStart;
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