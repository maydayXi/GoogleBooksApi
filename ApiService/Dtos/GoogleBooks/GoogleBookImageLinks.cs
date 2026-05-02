using System.Text.Json.Serialization;

namespace ApiService.Dtos.GoogleBooks;

/// <summary>
/// Google Book Image Link Data Transfer Object (DTO) for encapsulating the relevant information 
/// about a book's image links
/// </summary>
public class GoogleBookImageLinks
{
    /// <summary>
    /// Gets or sets the URL of the book small thumbnail image.
    /// </summary>
    [JsonPropertyName("smallThumbnail")]
    public string? SmallThumbnail { get; set; }

    /// <summary>
    /// Gets or sets the URL of the book thumbnail image.
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public string? Thumbnail { get; set; }
}
