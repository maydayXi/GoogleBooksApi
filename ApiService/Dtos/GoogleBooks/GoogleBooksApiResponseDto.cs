using System.Text.Json.Serialization;

namespace ApiService.Dtos.GoogleBooks;

/// <summary>
/// Google Books API response Data Transfer Object (DTO) 
/// for encapsulating the structure of the response received from the Google Books API.
/// </summary>
public class GoogleBooksApiResponseDto
{
    /// <summary>
    /// Gets or sets the resource type identifier.
    /// </summary>
    [JsonPropertyName("kind")]
    public required string Kind { get; set; }
    /// <summary>
    /// Gets or sets the total number of items.
    /// </summary>
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }
    /// <summary>
    /// Google Books API response items, representing individual book entries.
    /// </summary>
    [JsonPropertyName("items")]
    public List<GoogleBookItemDto> Items { get; set; } = [];
}
