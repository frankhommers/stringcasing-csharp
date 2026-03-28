# StringCasing

High-performance string case conversion extensions for .NET. Source-only NuGet package — zero runtime dependencies.

## Installation

```bash
dotnet add package StringCasing
```

## Usage

```csharp
using StringCasing;

"parseHTTPResponse".ToPascalCase();           // ParseHttpResponse
"parseHTTPResponse".ToCamelCase();            // parseHttpResponse
"parseHTTPResponse".ToKebabCase();            // parse-http-response
"parseHTTPResponse".ToSnakeCase();            // parse_http_response
"parseHTTPResponse".ToMacroCase();            // PARSE_HTTP_RESPONSE
"parseHTTPResponse".ToCobolCase();            // PARSE-HTTP-RESPONSE
"parseHTTPResponse".ToTrainCase();            // Parse-Http-Response
"parseHTTPResponse".ToTitleCase();            // Parse Http Response
"parseHTTPResponse".ToTitleSnakeCase();       // Parse_Http_Response
"parseHTTPResponse".ToDotCase();              // parse.http.response
"parseHTTPResponse".ToFlatCase();             // parsehttpresponse
```

### Microsoft .NET naming convention

Two additional methods follow the [Microsoft capitalization conventions](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/capitalization-conventions), where two-letter acronyms stay fully uppercase:

```csharp
"parseIOStream".ToDotNetPascalCase();   // ParseIOStream  (IO stays uppercase)
"parseIOStream".ToDotNetCamelCase();    // parseIOStream
"getDBConnection".ToDotNetPascalCase(); // GetDBConnection (DB stays uppercase)
"parseHTTPResponse".ToDotNetPascalCase(); // ParseHttpResponse (HTTP is 4 letters, gets title-cased)
```

## Performance

Built for performance with zero unnecessary allocations:

- `ref struct WordSplitter` for zero-allocation word boundary detection
- `stackalloc char[256]` output buffer with `ArrayPool<char>` fallback
- `ReadOnlySpan<char>` slicing — no intermediate string arrays
- No LINQ, no StringBuilder, no Regex

## Source-only package

This package ships as source code, not a compiled assembly. The `.cs` files are compiled directly into your project. The extension class is `internal`, so it won't pollute your public API.

## License

This project is licensed under the [MIT License](LICENSE).
