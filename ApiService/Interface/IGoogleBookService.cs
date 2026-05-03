using ApiService.Dtos;
using ApiService.Dtos.Response;

namespace ApiService.Interface;

/// <summary>
/// Defines operations for interacting with the Google Books API.
/// </summary>
public interface IGoogleBookService
{
    /// <summary>
    /// Retrieves book information by International Standard Book Number (ISBN).
    /// </summary>
    /// <param name="isbn">The ISBN of the book to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the book search response.</returns>
    Task<ApiResponse<BookSearchResponseDto>> FetchBookByIsbnAsync(string isbn);

    /// <summary>
    /// Asynchronously retrieves a collection of books matching the specified title.
    /// </summary>
    /// <param name="title">The title to search for.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a collection of matching books.
    /// </returns>
    Task<ApiResponse<IEnumerable<BookSearchResponseDto>>> FetchBooksByTitleAsync(string title);
}
