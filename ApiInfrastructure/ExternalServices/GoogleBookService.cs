using ApiInfrastructure.Options;
using ApiService.Dtos;
using ApiService.Dtos.GoogleBooks;
using ApiService.Dtos.Response;
using ApiService.Enums;
using ApiService.Extensions;
using ApiService.Interface;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace ApiInfrastructure.ExternalServices;

/// <summary>
/// Google book service responsible for handling operations related to Google Books, 
/// such as fetching book data from the Google Books API, 
/// processing book information, and providing book-related functionalities to the application.
/// </summary>
public class GoogleBookService(IOptions<GoogleBooksOptions> options) : GoogleBooksServiceBase, IGoogleBookService
{
    /// <summary>
    /// Provides HTTP functionality for sending requests and receiving responses.
    /// </summary>
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(options.Value.BaseUrl)
    };

    /// <summary>
    /// Provides the configured Google Books options.
    /// </summary>
    private readonly GoogleBooksOptions _options = options.Value;

    /// <summary>
    /// Provides JSON serialization options with case-insensitive property name matching.
    /// </summary>
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<ApiResponse<BookSearchResponseDto>> FetchBookByIsbnAsync(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn) || !isbn.IsValidIsbn())
            return BadRequestResponse<BookSearchResponseDto>("Invalid ISBN provided. Please provide a valid ISBN-10 or ISBN-13.");

        BookSearchCriteria criteria = BookSearchCriteria.ISBN;
        string requestUri = $"?q=isbn:{isbn}&key={_options.ApiKey}";

        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode) return BadGetwayResponse<BookSearchResponseDto>(response.StatusCode);

            GoogleBooksApiResponseDto? googleBooksApiResponse = await response.Content.ReadFromJsonAsync<GoogleBooksApiResponseDto>(_jsonOptions);

            if (googleBooksApiResponse is null || googleBooksApiResponse.TotalItems <= 0 ||
                googleBooksApiResponse.Items.Count <= 0)
                return NotFoundResponse<BookSearchResponseDto>(criteria, isbn);

            GoogleBookItemDto? googleBook = googleBooksApiResponse.Items.FirstOrDefault();
            if (googleBook?.VolumeInfo is null)
                return NotFoundResponse<BookSearchResponseDto>(criteria, isbn);

            return SuccessResponse(googleBook);
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse<BookSearchResponseDto>(ex);
        }
    }

    public async Task<ApiResponse<IEnumerable<BookSearchResponseDto>>> FetchBooksByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return BadRequestResponse<IEnumerable<BookSearchResponseDto>>(
                "Title cannot be empty. Please provide a valid book title.");

        BookSearchCriteria criteria = BookSearchCriteria.Title;
        string requestUrl = $"?q=intitle:{title}&key={_options.ApiKey}";

        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return BadGetwayResponse<IEnumerable<BookSearchResponseDto>>(response.StatusCode);

            GoogleBooksApiResponseDto? googleBookApiResponse = await response.Content.ReadFromJsonAsync<GoogleBooksApiResponseDto>();

            if (googleBookApiResponse is null || googleBookApiResponse.TotalItems <= 0 ||
                googleBookApiResponse.Items.Count <= 0)
                return NotFoundResponse<IEnumerable<BookSearchResponseDto>>(criteria, title);

            return SuccessResponse(googleBookApiResponse.Items);
        }
        catch (Exception ex)
        {
            return InternalServerErrorResponse<IEnumerable<BookSearchResponseDto>>(ex);
        }
    }
}