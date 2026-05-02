using System.Text.Json.Serialization;

namespace ApiService.Dtos.GoogleBooks;

/// <summary>
/// Represents a data transfer object for a Google Book item, 
/// encapsulating the relevant information about a book retrieved from the Google Books API.
/// </summary>
public class GoogleBookItemDto
{
    /// <summary>
    /// Gets or sets the kind or category associated with the object.
    /// </summary>
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    /// <summary>
    /// Google book item identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the entity tag (ETag) value used for concurrency checks.
    /// </summary>
    [JsonPropertyName("etag")]
    public string? Etag { get; set; }

    /// <summary>
    /// Gets or sets the URI that uniquely identifies this resource.
    /// </summary>
    [JsonPropertyName("selfLink")]
    public string? SelfLink { get; set; }

    /// <summary>
    /// Gets or sets detailed information about the book volume.
    /// </summary>
    [JsonPropertyName("volumeInfo")]
    public GoogleBookVolumeInfoDto? VolumeInfo { get; set; }
}
