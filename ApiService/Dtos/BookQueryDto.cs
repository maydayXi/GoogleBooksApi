namespace ApiService.Dtos;

/// <summary>
/// Represents the query parameters used internally to request book data from an external API.
/// </summary>
public class BookQueryDto
{
    /// <summary>
    /// The title of the book to search for.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// The maximum number of results to return in a single response.
    /// </summary>
    public int MaxResults { get; set; }

    /// <summary>
    /// The zero-based index of the first result to return, used for pagination.
    /// </summary>
    public int StartIndex { get; set; }
}