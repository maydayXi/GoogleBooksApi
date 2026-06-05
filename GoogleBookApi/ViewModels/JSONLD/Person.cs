using System.Text.Json.Serialization;

namespace GoogleBookApi.ViewModels.JSONLD;

/// <summary>
/// Represents a Schema.org Person type
/// </summary>
public record Person : SchemaBase
{
    [JsonIgnore]
    public override string Type => nameof(Person);

    [JsonPropertyName("@type")]
    public string SchemaType => Type;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}