using System.Text.Json.Serialization;

namespace ApiService.Dtos.GoogleBooks;

/// <summary>
/// Google Book Volume Info Data Transfer Object (DTO) 
/// for encapsulating the relevant information about a book's volume retrieved from the Google Books API.
/// </summary>
public class GoogleBookVolumeInfoDto
{
    /// <summary>
    /// Title of the book, representing the main title or name of the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Book authors, representing the list of authors associated with the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; } = [];

    /// <summary>
    /// Publisher of the book, representing the name of the publisher associated with the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("publisher")]
    public required string Publisher { get; set; }

    /// <summary>
    /// Book published date, representing the date when the book was published as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("publishedDate")]
    public DateOnly PublishedDate { get; set; }

    /// <summary>
    /// Book description, representing a brief summary or overview of the book's content as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Google book industry identifiers, representing a list of industry identifiers (e.g., ISBN numbers) 
    /// associated with the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("industryIdentifiers")]
    public List<GoogleBookIndustryIdentifierDto> IndustryIdentifiers { get; set; } = [];

    /// <summary>
    /// Gets or sets the reading mode information for the book.
    /// </summary>
    [JsonPropertyName("readingModes")]
    public GoogleBookReadingMode ReadingMode { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of pages.
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// Gets or sets the print type associated with the item.
    /// </summary>
    [JsonPropertyName("printType")]
    public string? PrintType { get; set; }

    /// <summary>
    /// Gets or sets google book categories, representing the list of categories or genres 
    /// associated with the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; } = [];

    /// <summary>
    /// Google book maturity rating, representing the maturity rating (e.g., "MATURE", "NOT_MATURE")
    /// </summary>
    [JsonPropertyName("maturityRating")]
    public string? MaturityRating { get; set; }

    /// <summary>
    /// Google book image links, representing the various image links (e.g., thumbnail, small thumbnail)
    /// </summary>
    [JsonPropertyName("imageLinks")]
    public GoogleBookImageLinks ImageLinks { get; set; } = new();

    /// <summary>
    /// Google book language, representing the language code (e.g., "en", "fr") associated with the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Google book preview link, representing the URL to preview the book's content as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("previewLink")]
    public string? PreviewLink { get; set; }

    /// <summary>
    /// Google book info link, representing the URL to access more detailed information about the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("infoLink")]
    public string? InfoLink { get; set; }

    /// <summary>
    /// Google book canonical volume link, representing the URL to access the canonical volume information for the book as provided by the Google Books API.
    /// </summary>
    [JsonPropertyName("canonicalVolumeLink")]
    public string? CanonicalVolumeLink { get; set; }
}
