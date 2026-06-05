using GoogleBookApi.ViewModels.JSONLD;

namespace GoogleBookApi.ViewModels.Components;

/// <summary>
/// Represents the result of a book search operation.
/// </summary>
/// <remarks>
/// Including the matched books, pagination metadata, and structured data for SEO.
/// </remarks>
public class BookSearchResultVm
{
    /// <summary>
    /// The list of books returned by the search query.
    /// </summary>
    /// <remarks>
    /// Empty if no results were found.
    /// </remarks>
    public List<BookVm> Books { get; init; } = [];

    /// <summary>
    /// Pagination metadata for the book search result.
    /// </summary>
    public PaginationVm Pagination { get; init; } = new();

    /// <summary>
    /// Schema.org ItemList for structured data rendering.
    /// </summary>
    /// <remarks>
    /// <see see="Empty"/> if there are no results.
    /// </remarks>
    public string JsonLd { get; init; } = string.Empty;
}