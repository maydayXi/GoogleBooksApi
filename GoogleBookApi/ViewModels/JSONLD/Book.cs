using System.Text.Json.Serialization;

namespace GoogleBookApi.ViewModels.JSONLD;

/// <summary>
/// Represents a Schema.org Book type.
/// </summary>
public record Book : SchemaBase
{
    [JsonIgnore]
    public override string Type => nameof(Book);

    [JsonPropertyName("@type")]
    public string SchemaType => Type;

    /// <summary>
    /// The name of the item.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
    
    /// <summary>
    /// The author of this content or rating.
    /// </summary>
    [JsonPropertyName("author")]
    public Person? Author { get; init; }
    
    /// <summary>
    /// The publisher of the article in question.
    /// </summary>
    [JsonPropertyName("publisher")]
    public Organization? Publisher { get; init; }
    
    /// <summary>
    /// The ISBN of the book.
    /// </summary>
    [JsonPropertyName("isbn")]
    public string? Isbn { get; init; }
    
    /// <summary>
    /// Date of first publication or broadcast.
    /// </summary>
    [JsonPropertyName("datePublished")]
    public string? DatePublished { get; init; }
    
    /// <summary>
    /// An image of the item. 
    /// </summary>
    /// <remarks>
    /// Can be a URL.
    /// </remarks>
    [JsonPropertyName("image")]
    public string? Image { get; init; }
    
    /// <summary>
    /// A description of the item.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }
}