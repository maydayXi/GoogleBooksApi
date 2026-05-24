using ApiInfrastructure.Options;
using ApiService.Dtos;
using ApiService.Dtos.GoogleBooks;
using ApiService.Dtos.Response;
using ApiService.Enums;
using ApiService.Extensions;
using ApiService.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace ApiInfrastructure.ExternalServices;

/// <summary>
/// Google book service responsible for handling operations related to Google Books, 
/// such as fetching book data from the Google Books API, 
/// processing book information, and providing book-related functionalities to the application.
/// </summary>
public class GoogleBookService(IOptions<GoogleBooksOptions> options, ILogger<GoogleBookService> logger)
    : GoogleBooksServiceBase, IGoogleBookService
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
    /// Provides logging capabilities for the GoogleBookService.
    /// </summary>
    private readonly ILogger<GoogleBookService> _logger = logger;

    /// <summary>
    /// Masked URL used for logging purposes to avoid exposing sensitive information such as API keys in logs.
    /// </summary>
    private string _maskedUrl = string.Empty;

    /// <summary>
    /// Original content of the API response, used for logging and debugging purposes to capture the raw response from the Google Books API before any processing or deserialization occurs.
    /// </summary>
    private string _originalContent = string.Empty;

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

        const BookSearchCriteria criteria = BookSearchCriteria.ISBN;
        var requestUri = $"?q=isbn:{isbn}&key={_options.ApiKey}";
        _maskedUrl = $"{_options.BaseUrl}?q=isbn:{isbn}";

        try
        {
            using var response = await _httpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode) return BadGatewayResponse<BookSearchResponseDto>(response.StatusCode);

            var googleBooksApiResponse = await response.Content.ReadFromJsonAsync<GoogleBooksApiResponseDto>(_jsonOptions);

            if (googleBooksApiResponse is null || googleBooksApiResponse.TotalItems <= 0 ||
                googleBooksApiResponse.Items.Count <= 0)
                return NotFoundResponse<BookSearchResponseDto>(criteria, isbn);

            var googleBook = googleBooksApiResponse.Items.FirstOrDefault();
            
            return googleBook?.VolumeInfo is null
                ? NotFoundResponse<BookSearchResponseDto>(criteria, isbn) 
                : SuccessResponse(googleBook, 1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while processing Google Books API request.");
            return InternalServerErrorResponse<BookSearchResponseDto>(ex);
        }
    }

    public async Task<ApiResponse<IEnumerable<BookSearchResponseDto>>> FetchBooksByQueryAsync(BookQueryDto bookQueryDto)
    {
        if (string.IsNullOrWhiteSpace(bookQueryDto.Title))
            return BadRequestResponse<IEnumerable<BookSearchResponseDto>>(
                "Title cannot be empty. Please provide a valid book title.");

        if (bookQueryDto.StartIndex < 0) bookQueryDto.StartIndex = 0;
        if (!Enum.IsDefined(typeof(PageSize), bookQueryDto.MaxResults))
            bookQueryDto.MaxResults = (int)PageSize.Ten;

        const BookSearchCriteria criteria = BookSearchCriteria.Title;
        var requestUrl = $"?q=intitle:{bookQueryDto.Title}&startIndex={bookQueryDto.StartIndex}&maxResults={bookQueryDto.MaxResults}&key={_options.ApiKey}";
        _maskedUrl = $"{_options.BaseUrl}?q=intitle:{bookQueryDto.Title}";

        try
        {
            using var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return BadGatewayResponse<IEnumerable<BookSearchResponseDto>>(response.StatusCode);

            _originalContent = await response.Content.ReadAsStringAsync();
            var googleBookApiResponse = await response.Content.ReadFromJsonAsync<GoogleBooksApiResponseDto>();

            if (googleBookApiResponse is null || googleBookApiResponse.TotalItems <= 0 ||
                googleBookApiResponse.Items.Count <= 0)
                return NotFoundResponse<IEnumerable<BookSearchResponseDto>>(criteria, bookQueryDto.Title);

            return SuccessResponse(googleBookApiResponse.Items, googleBookApiResponse.TotalItems);
        }
        catch (JsonException jsonEx)
        {
            _logger.LogError(jsonEx,
                "Failed to deserialize Google Books API response.\nRequestUrl: {RequestUrl}\nJson Path: {JsonPath}\nLine Number: {LineNumber}\nByte Position in Line: {BytePositionInLine}\nOriginal Content: {OriginalContent}",
                _maskedUrl, jsonEx.Path, jsonEx.LineNumber, jsonEx.BytePositionInLine, _originalContent
            );
            return JsonParseExceptionResponse(jsonEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while processing Google Books API request.\nRequest Url: {RequestUrl}", _maskedUrl);
            return InternalServerErrorResponse<IEnumerable<BookSearchResponseDto>>(ex);
        }
    }
}