using ApiService.Dtos;
using ApiService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoogleBookApi.Controllers;

/// <summary>
/// API controller for managing Google Books operations.
/// </summary>
/// <param name="googleBookService">The service responsible for handling Google Books operations.</param>
[ApiController, Route("api/[controller]")]
public class GoogleBooksController(IGoogleBookService googleBookService) : ControllerBase
{
    /// <summary>
    /// Provides access to Google Books service operations.
    /// </summary>
    private readonly IGoogleBookService _googleBookService = googleBookService;

    /// <summary>
    /// Retrieves book information by ISBN.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to retrieve.</param>
    /// <returns>An IActionResult containing the book information if found; otherwise, a NotFound or BadRequest result.</returns>
    [HttpGet($"{nameof(BookInfo)}/{{isbn}}")]
    public async Task<IActionResult> BookInfo(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn)) return BadRequest("ISBN cannot be null or empty.");

        BookSearchResponseDto? bookResponse = await _googleBookService.FetchBookByIsbnAsync(isbn);

        if (bookResponse is null) return NotFound($"No book found with ISBN: {isbn}");

        return Ok(bookResponse);
    }
}
