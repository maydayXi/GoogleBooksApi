using System.Text.Json.Serialization;

namespace GoogleBookApi.ViewModels.JSONLD;

/// <summary>
/// Represents a Schema.org ItemList type for a collection of books.
/// </summary>
public record ItemList : SchemaBase
{
    [JsonIgnore]
    public override string Type => nameof(ItemList);

    /// <summary>
    /// For itemListElement values.
    /// </summary>
    [JsonPropertyName("itemListElement")]
    public IEnumerable<ListItem> ItemListElement { get; init; } = [];
};
