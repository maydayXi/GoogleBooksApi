using ApiService.Dtos;
using ApiService.Dtos.Response;
using ApiService.Enums;
using ApiService.Extensions;
using ApiService.Interface;
using GoogleBookApi.Extensions;
using GoogleBookApi.Helper;
using GoogleBookApi.Models;
using GoogleBookApi.ViewModels;
using GoogleBookApi.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using GoogleBookApi.ViewModels.JSONLD;

namespace GoogleBookApi.Controllers;

/// <summary>
/// Controller for handling requests related to the application's home and informational pages.
/// </summary>
/// <param name="googleBookService">The service responsible for handling Google Books operations.</param>
/// <param name="versionDataProvider">The data provider used to retrieve version history from a JSON source.</param>
/// <param name="guidelineDataProvider">The data provider used to retrieve guideline from a JSON source.</param>
public class HomeController(IGoogleBookService googleBookService, IJsonDataProvider<VersionItemVm> versionDataProvider, 
    IJsonDataProvider<GuidelineItemVm> guidelineDataProvider) : Controller
{

    /// <summary>
    /// Google Book service for fetching book information.
    /// </summary>
    private readonly IGoogleBookService _googleBookService = googleBookService;

    /// <summary>
    /// Version data provider for retrieving version information from JSON files.
    /// </summary>
    private readonly IJsonDataProvider<VersionItemVm> _versionDataProvider = versionDataProvider;

    /// <summary>
    /// Provides access to guideline item data using a JSON data provider.
    /// </summary>
    private readonly IJsonDataProvider<GuidelineItemVm> _guidelineDataProvider = guidelineDataProvider;

    /// <summary>
    /// Displays the home page of the application.
    /// </summary>
    /// <returns>
    /// A <see cref="IActionResult"/> that renders the home page view.
    /// </returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Renders the Guideline view.
    /// </summary>
    /// <returns>A <see cref="IActionResult"/> that renders the home page view.</returns>
    public IActionResult Guideline()
    {
        var guidelineItems = _guidelineDataProvider.GetListDataFromJson(WebHelper.GuidelineJsonFilePath);
        return View(guidelineItems);
    }

    /// <summary>
    /// Displays the book search view.
    /// </summary>
    /// <returns>A <see cref="IActionResult"/> that renders the book search page.</returns>
    public IActionResult BookSearch()
    {
        return View(new BookSearchVm
        {
            DropdownItems = [.. Enum.GetValues<BookSearchCriteria>()
                .Where(criteria => criteria is BookSearchCriteria.ISBN or BookSearchCriteria.Title)
                .Select(criteria => new DropdownItemVm
                {
                    Icon = WebHelper.GetDropdownItemIconByCriteria(criteria),
                    Criteria = criteria
                })
            ]
        });
    }

    /// <summary>
    /// Fetches book information based on the provided ISBN and returns a partial view with the results.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the book information if found; otherwise, a NotFound or BadRequest result.</returns>
    [HttpGet($"/{nameof(_FetchBookByIsbn)}/{{isbn}}")]
    public async Task<IActionResult> _FetchBookByIsbn([FromRoute] string isbn)
    {
        if (!isbn.IsValidIsbn()) return BadRequest($"Invalid ISBN: {isbn}.");

        ApiResponse<BookSearchResponseDto> bookResponse = await _googleBookService.FetchBookByIsbnAsync(isbn);

        if (!bookResponse.IsSuccess)
            return StatusCode(bookResponse.HttpStatusCode, bookResponse);

        var book = bookResponse.Data ?? new BookSearchResponseDto();
        return PartialView(book.ToBookViewModel());
    }

    /// <summary>
    /// Fetches book information based on the provided title and returns a partial view with the results.
    /// </summary>
    /// <param name="bookSearchRequest">The search request containing the title and pagination parameters.</param>
    /// <returns>An <see cref="IActionResult"/> containing the book information if found; otherwise, a NotFound or BadRequest result.</returns>
    [HttpGet($"/{nameof(_FetchBooksByTitle)}")]
    public async Task<IActionResult> _FetchBooksByTitle([FromQuery] BookSearchRequest bookSearchRequest)
    {
        if (string.IsNullOrWhiteSpace(bookSearchRequest.Title)) 
            return BadRequest("Title cannot be null or empty.");
        
        var booksResponse = await _googleBookService.FetchBooksByQueryAsync(
            bookSearchRequest.ToBookQuery());

        if (!booksResponse.IsSuccess)
            return StatusCode(booksResponse.HttpStatusCode, booksResponse);

        var books = booksResponse.Data ?? [];
        var bookVms = books.ToBooksViewModel();

        var hasBooks = booksResponse.TotalBooks > 0;
        
        var pagination = new PaginationVm
        {
            TotalPage = hasBooks 
                ? WebHelper.CalculatePages((int)bookSearchRequest.PageSize, booksResponse.TotalBooks)
                : 0,
            CurrentPage = hasBooks
                ? bookSearchRequest.Page
                : 0
        };

        List<BookVm> result = [..bookVms];

        var jsonLd = !hasBooks
            ? string.Empty
            : JsonSerializer.Serialize(new Dictionary<string, object>()
            {
                ["@context"] = "https://schema.org",
                ["@type"] = $"{nameof(ItemList)}",
                ["itemListElement"] = result.ToSchemaItemList().ItemListElement
            }, WebHelper.GetJsonSerializerOptions());
        
        return PartialView(new BookSearchResultVm
        {
            Books = result,
            Pagination = pagination,
            JsonLd = jsonLd
        });
    }

    /// <summary>
    /// Displays the version view.
    /// </summary>
    /// <returns>The view for the version information.</returns>
    public IActionResult Version()
    {
        var versions = _versionDataProvider.GetListDataFromJson(WebHelper.VersionJsonFilePath);
        return View(versions);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
