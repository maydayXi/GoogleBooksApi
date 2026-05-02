using ApiInfrastructure.Options;
using ApiService.Dtos;
using ApiService.Dtos.GoogleBooks;
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
public class GoogleBookService(IOptions<GoogleBooksOptions> options) : IGoogleBookService
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

    public async Task<BookSearchResponseDto?> FetchBookByIsbnAsync(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn)) return null;

        string requestUri = $"?q=isbn:{isbn}&key={_options.ApiKey}";

        using var response = await _httpClient.GetAsync(requestUri);
        if (!response.IsSuccessStatusCode) return null;

        GoogleBooksApiResponseDto googleBooksApiResponse = await response.Content.ReadFromJsonAsync<GoogleBooksApiResponseDto>(_jsonOptions)
            ?? throw new InvalidOperationException("Failed to deserialize Google Books API response.");

        if (googleBooksApiResponse is null || googleBooksApiResponse.TotalItems <= 0 ||
            googleBooksApiResponse.Items.Count <= 0)
            return null;

        GoogleBookItemDto? googleBook = googleBooksApiResponse.Items.FirstOrDefault();
        if (googleBook?.VolumeInfo is null) return null;

        return googleBook.ToBookSearchResponse();
    }

    public async Task<IEnumerable<BookSearchResponseDto>> FetchBooksByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return [];

        string requestUrl = $"?q=intitle:{title}&key={_options.ApiKey}";

        using var response = await _httpClient.GetAsync(requestUrl);
        if (!response.IsSuccessStatusCode) return [];

        GoogleBooksApiResponseDto googleBookApiResponse =
            await response.Content.ReadFromJsonAsync<GoogleBooksApiResponseDto>()
            ?? throw new InvalidOperationException("Failed to deserialize Google Books API response");

        if (googleBookApiResponse is null || googleBookApiResponse.TotalItems <= 0 ||
            googleBookApiResponse.Items.Count <= 0)
            return [];

        IEnumerable<BookSearchResponseDto> books = googleBookApiResponse.Items.Select(book =>
            book.ToBookSearchResponse());

        return books;
    }
}
