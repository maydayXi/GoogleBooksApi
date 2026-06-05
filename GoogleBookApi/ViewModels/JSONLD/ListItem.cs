using System.Text.Json.Serialization;

namespace GoogleBookApi.ViewModels.JSONLD;

/// <summary>
/// Represents a Schema.org ListItem type, used inside an ItemList
/// </summary>
public record ListItem : SchemaBase
{
    [JsonPropertyName("@type")]
    public override string Type => nameof(ListItem);
    
    /// <summary>
    /// The position of an item in a series or sequence of items.
    /// </summary>
    [JsonPropertyName("position")]
    public int Position { get; init; }
    
    /// <summary>
    /// An entity represented by an entry in a lis or data feed.
    /// </summary>
    [JsonPropertyName("item")]
    public Book? Item { get; init; }
};