using ApiService.Enums;

namespace ApiService.Dtos;

/// <summary>
/// Book Search Response Data Transfer Object (DTO) 
/// representing the structure of the response returned by the book search API,
/// </summary>
public class BookSearchResponseDto
{
    /// <summary>
    /// Title of the book
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Author of the book
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Publisher of the book
    /// </summary>
    public string Publisher { get; set; } = string.Empty;

    /// <summary>
    /// Date of the book's publication
    /// </summary>
    public DateOnly? PublishedDate { get; set; }

    /// <summary>
    /// Description of the book
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Image link for the book's thumbnail or cover image
    /// </summary>
    public string ImageLink { get; set; } = string.Empty;

    /// <summary>
    /// Book identifiers, such as ISBN-10 and ISBN-13, represented as a dictionary 
    /// where the key is the type of <see cref="BookIdentifierType"/>
    /// and the value is the corresponding identifier string.
    /// </summary>
    public Dictionary<BookIdentifierType, string> BookIdentifier { get; set; } = new()
    {
        [BookIdentifierType.ISBN_10] = string.Empty,
        [BookIdentifierType.ISBN_13] = string.Empty,
    };
}
