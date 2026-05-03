using ApiService.Dtos;
using ApiService.Dtos.GoogleBooks;
using ApiService.Dtos.Response;
using ApiService.Enums;
using ApiService.Extensions;
using System.Net;

namespace ApiInfrastructure.ExternalServices;

/// <summary>
/// Base class for Google Books services, providing factory methods for constructing
/// standardized <see cref="ApiResponse{T}"/> results for common HTTP outcomes.
/// </summary>
public class GoogleBooksServiceBase
{
    /// <summary>
    /// Creates a successful <see cref="ApiResponse{T}"/> containing the book data retrieved from the Google Books API.
    /// </summary>
    /// <param name="book">The <see cref="GoogleBookItemDto"/> returned by the Google Books API.</param>
    /// <returns>
    /// An <see cref="ApiResponse{T}"/> with <see cref="HttpStatusCode.OK"/>,
    /// <see cref="ApiResponseStatus.GoogleBooksApiSuccess"/> and the mapped <see cref="BookSearchResponseDto"/>.
    /// </returns>
    protected static ApiResponse<BookSearchResponseDto> SuccessResponse(GoogleBookItemDto book) => new()
    {
        IsSuccess = true,
        HttpStatusCode = (int)HttpStatusCode.OK,
        ApiResponseStatus = ApiResponseStatus.GoogleBooksApiSuccess,
        Message = "Book data retrieved successfully.",
        Data = book.ToBookSearchResponse(),
    };

    /// <summary>
    /// Creates a successful API response containing a collection of book search results mapped from Google Books API items.
    /// </summary>
    /// <param name="books">The collection of Google Books API items to map to the response.</param>
    /// <returns>An ApiResponse containing the mapped book search results and a success status.</returns>
    protected static ApiResponse<IEnumerable<BookSearchResponseDto>> SuccessResponse(IEnumerable<GoogleBookItemDto> books) => new()
    {
        IsSuccess = true,
        HttpStatusCode = (int)HttpStatusCode.OK,
        ApiResponseStatus = ApiResponseStatus.GoogleBooksApiSuccess,
        Message = "Books data retrieved successfully.",
        Data = books.Select(b => b.ToBookSearchResponse()),
    };

    /// <summary>
    /// Creates a bad request <see cref="ApiResponse{T}"/> indicating that the provided ISBN is invalid.
    /// </summary>
    /// <param name="message">The error message describing the reason for the bad request.</param>
    /// <returns>
    /// An <see cref="ApiResponse{T}"/> with <see cref="HttpStatusCode.BadRequest"/> and <see cref="ApiResponseStatus.InvalidIsbn"/>.
    /// </returns>
    protected static ApiResponse<T> BadRequestResponse<T>(string message) where T : class => new()
    {
        IsSuccess = false,
        HttpStatusCode = (int)HttpStatusCode.BadRequest,
        ApiResponseStatus = ApiResponseStatus.InvalidIsbn,
        Message = message,
        Data = null,
    };

    /// <summary>
    /// Creates a bad gateway <see cref="ApiResponse{T}"/> indicating that the Google Books API returned an unsuccessful HTTP status code.
    /// </summary>
    /// <param name="httpStatusCode">The <see cref="HttpStatusCode"/> returned by the Google Books API.</param>
    /// <returns>
    /// An <see cref="ApiResponse{T}"/> with the upstream <paramref name="httpStatusCode"/> and <see cref="ApiResponseStatus.GoogleBooksApiError"/>.
    /// </returns>
    protected static ApiResponse<T> BadGetwayResponse<T>(HttpStatusCode httpStatusCode) where T : class => new()
    {
        IsSuccess = false,
        HttpStatusCode = (int)httpStatusCode,
        ApiResponseStatus = ApiResponseStatus.GoogleBooksApiError,
        Message = $"Failed to fetch book data from Google Books API. Status code: {(int)httpStatusCode}",
        Data = null,
    };

    /// <summary>
    /// Creates a not found <see cref="ApiResponse{T}"/> indicating that no book was found for the specified ISBN.
    /// </summary>
    /// <param name="criteria">The search criteria used for the book search (e.g., ISBN or Title).</param>
    /// <param name="isbn">The ISBN that yielded no results from the Google Books API.</param>
    /// <returns>
    /// An <see cref="ApiResponse{T}"/> with <see cref="HttpStatusCode.NotFound"/> and <see cref="ApiResponseStatus.BookNotFound"/>.
    /// </returns>
    protected static ApiResponse<T> NotFoundResponse<T>(BookSearchCriteria criteria, string input) where T : class => new()
    {
        IsSuccess = false,
        HttpStatusCode = (int)HttpStatusCode.NotFound,
        ApiResponseStatus = ApiResponseStatus.BookNotFound,
        Message = $"No book found with {criteria}: {input}",
        Data = null,
    };

    /// <summary>
    /// Creates an internal server error <see cref="ApiResponse{T}"/> wrapping an unexpected exception.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> that caused the failure.</param>
    /// <returns>
    /// An <see cref="ApiResponse{T}"/> with <see cref="HttpStatusCode.InternalServerError"/> and <see cref="ApiResponseStatus.SystemError"/>.
    /// </returns>
    protected static ApiResponse<T> InternalServerErrorResponse<T>(Exception exception) where T : class => new()
    {
        IsSuccess = false,
        HttpStatusCode = (int)HttpStatusCode.InternalServerError,
        ApiResponseStatus = ApiResponseStatus.SystemError,
        Message = $"An error occurred while processing the request: {exception.Message}",
        Data = null,
    };
}
