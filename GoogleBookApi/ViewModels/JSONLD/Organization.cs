using System.Text.Json.Serialization;

namespace GoogleBookApi.ViewModels.JSONLD;

/// <summary>
/// Represents a Schema.org Organization type.
/// </summary>
public record Organization : SchemaBase
{
    
    [JsonIgnore] public override string Type => nameof(Organization);

    [JsonPropertyName("@type")]
    public string SchemaType => Type;

    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
}