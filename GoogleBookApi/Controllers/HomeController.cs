using ApiService.Enums;
using ApiService.Extensions;
using ApiService.Interface;
using GoogleBookApi.Helper;
using GoogleBookApi.Models;
using GoogleBookApi.ViewModels;
using GoogleBookApi.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoogleBookApi.Controllers;

/// <summary>
/// Controller for handling requests related to the application's home and informational pages.
/// </summary>
/// <param name="logger">The logger used to record application events.</param>
/// <param name="googleBookService">The service responsible for handling Google Books operations.</param>
public class HomeController(ILogger<HomeController> logger, IGoogleBookService googleBookService,
    IJsonDataProvider<VersionItemVm> versionDataProvider, IJsonDataProvider<GuidelineItemVm> guidelineDataProvider)
    : Controller
{
    /// <summary>
    /// Provides logging capabilities for the HomeController.
    /// </summary>
    private readonly ILogger<HomeController> _logger = logger;

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
        var guidelineItems = _guidelineDataProvider.GetListDataFromJson(@"data\guideline.json");
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
                .Where(criteria => criteria == BookSearchCriteria.ISBN || criteria == BookSearchCriteria.Title)
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
        if (!isbn.IsValidIsbn()) return BadRequest("ISBN cannot be null or empty.");

        var bookResponse = await _googleBookService.FetchBookByIsbnAsync(isbn);

        if (bookResponse is null) return NotFound($"No book found with ISBN: {isbn}");

        (string isbn10, string isbn13) = WebHelper.GetIsbnByTypeFromBookIdentifier(bookResponse.BookIdentifier);
        string description = bookResponse.Description.Length > 150
            ? $"{bookResponse.Description[..150]}..."
            : bookResponse.Description;

        return PartialView(new BookVm
        {
            ImageLink = bookResponse.ImageLink,
            Title = bookResponse.Title,
            Author = bookResponse.Author,
            Publisher = bookResponse.Publisher,
            Description = description,
            Isbn10 = isbn10,
            Isbn13 = isbn13,
            PublishedDate = bookResponse.PublishedDate,
        });
    }

    /// <summary>
    /// Fetches book information based on the provided title and returns a partial view with the results.
    /// </summary>
    /// <param name="title">The title of the book to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the book information if found; otherwise, a NotFound or BadRequest result.</returns>
    [HttpGet($"/{nameof(_FetchBooksByTitle)}/{{title}}")]
    public async Task<IActionResult> _FetchBooksByTitle([FromRoute] string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return BadRequest("Title cannot be null or empty.");

        var bookResponses = await _googleBookService.FetchBooksByTitleAsync(title);

        if (bookResponses is null || !bookResponses.Any()) return NotFound($"No books found with title: {title}");

        string description;
        List<BookVm> bookVms = [.. bookResponses.Select(bookResponse =>
        {
            description = bookResponse.Description.Length > 150
                ? $"{bookResponse.Description[..150]}..."
                : bookResponse.Description;
            (string isbn10, string isbn13) = WebHelper.GetIsbnByTypeFromBookIdentifier(bookResponse.BookIdentifier);
            return new BookVm
            {
                ImageLink = bookResponse.ImageLink,
                Title = bookResponse.Title,
                Author = bookResponse.Author,
                Publisher = bookResponse.Publisher,
                Description = description,
                Isbn10 = isbn10,
                Isbn13 = isbn13,
                PublishedDate = bookResponse.PublishedDate,
            };
        })];

        return PartialView(bookVms);
    }

    /// <summary>
    /// Displays the version view.
    /// </summary>
    /// <returns>The view for the version information.</returns>
    public IActionResult Version()
    {
        var versions = _versionDataProvider.GetListDataFromJson(@"data\version.json");
        return View(versions);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
