using System.Text.Json.Serialization;

namespace GoogleBookApi.ViewModels.JSONLD;

/// <summary>
/// Base class for all Schema.org structured data types.
/// </summary>
public abstract record SchemaBase
{
    /// <summary>
    /// The Schema.org context URL.
    /// </summary>
    /// <remarks>
    /// Excluded from serialization - only output at root level via Dictionary wrapper.
    /// </remarks>
    [JsonIgnore]
    public virtual string Context => "https://schema.org";
    
    /// <summary>
    /// The Schema.org type identifier.
    /// </summary>
    /// <remarks>
    /// Excluded from default serialization —
    /// output via @type property in each subclass.
    /// </remarks>
    [JsonIgnore]
    public abstract string Type { get; }
};