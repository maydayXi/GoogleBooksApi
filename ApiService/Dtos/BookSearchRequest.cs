namespace ApiService.Dtos;

/// <summary>
/// Represents the request parameters for searching books.
/// </summary>
public class BookSearchRequest : BookSearchBase
{
    /// <summary>
    /// The title of the book to search for. Defaults to an empty string.
    /// </summary>
    public string Title { get; set; } = string.Empty;
}