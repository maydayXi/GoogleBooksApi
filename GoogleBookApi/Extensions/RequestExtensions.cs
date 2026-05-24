using ApiService.Dtos;

namespace GoogleBookApi.Extensions;

/// <summary>
/// Provides extension methods for converting search request DTOs into query DTOs.
/// </summary>
public static class RequestExtensions
{
    /// <summary>
    /// Converts a <see cref="bookSearch"/> into a <see cref="BookQueryDto"/>,
    /// mapping pagination parameters to the appropriate <c>StartIndex</c> for external API consumption.
    /// </summary>
    /// <param name="bookSearch">The book search request containing the title and pagination parameters.</param>
    /// <returns>A <see cref="BookQueryDto"/> with the title, page size, and calculated start index.</returns>
    public static BookQueryDto ToBookQuery(this BookSearchRequest bookSearch)
    {
        var startIndex = (bookSearch.Page - 1) * (int)bookSearch.PageSize;

        return new BookQueryDto
        {
            Title = bookSearch.Title,
            MaxResults = (int)bookSearch.PageSize,
            StartIndex = startIndex
        };
    }
}