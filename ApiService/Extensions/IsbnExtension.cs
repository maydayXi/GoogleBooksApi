namespace ApiService.Extensions;

/// <summary>
/// Provides extension methods for International Standard Book Numbers (ISBN).
/// </summary>
public static class IsbnExtension
{
    /// <summary>
    /// Determines whether the specified string is a valid ISBN.
    /// </summary>
    /// <param name="isbn">The string to validate as an ISBN.</param>
    /// <returns>true if the string is a valid ISBN; otherwise, false.</returns>
    public static bool IsValidIsbn(this string isbn) => ISBN.TryParse(isbn, out var _);

    /// <summary>
    /// Returns the hyphenated representation of an ISBN if valid; otherwise, returns an empty string.
    /// </summary>
    /// <param name="isbn">The ISBN string to hyphenate.</param>
    /// <returns>A hyphenated ISBN string if parsing succeeds; otherwise, an empty string.</returns>
    public static string Hyphenate(this string isbn) => ISBN.TryParse(isbn, out var _isbn)
        ? $"{_isbn}"
        : string.Empty;

    /// <summary>
    /// Returns the normalized form of the specified ISBN string by removing spaces and hyphens if the input is a valid ISBN; otherwise, returns an empty string.
    /// </summary>
    /// <param name="isbn">The ISBN string to normalize.</param>
    /// <returns>The normalized ISBN string, or an empty string if the input is not a valid ISBN.</returns>
    public static string Normalized(this string isbn) => ISBN.TryParse(isbn, out var _)
        ? isbn.Trim().Replace("-", string.Empty).Replace(" ", string.Empty)
        : string.Empty;
}
