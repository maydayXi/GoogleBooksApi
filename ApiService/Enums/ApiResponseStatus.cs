using System.ComponentModel;

namespace ApiService.Enums;

/// <summary>
/// API response status enumeration representing the various states of an API response
/// </summary>
/// <remarks>
/// Number rule
/// <list type="number">
///     <item>First digit: 1 for Books search API</item>
///     <item>digits 2-5: Status code <see cref="System.Net.HttpStatusCode"/></item>
///     <item>digits 6-7: Status sequence number</item>
/// </list>
/// </remarks>
public enum ApiResponseStatus
{
    /// <summary>
    /// Success
    /// </summary>
    [Description("Google books API success")]
    GoogleBooksApiSuccess = 120001,

    /// <summary>
    /// Invalid ISBN
    /// </summary>
    [Description("Invalid ISBN")]
    InvalidIsbn = 140001,

    /// <summary>
    /// Book not found
    /// </summary>
    [Description("Book not found")]
    BookNotFound = 140401,

    /// <summary>
    /// System error
    /// </summary>
    [Description("System error")]
    SystemError = 150001,

    /// <summary>
    /// Google Books API error
    /// </summary>
    [Description("Google books API error")]
    GoogleBooksApiError = 150201,

    /// <summary>
    /// Google Books API invalid response
    /// </summary>
    [Description("Google books API invalid response")]
    GoogleBooksApiInvalidResponse = 150202
}
