namespace ApiInfrastructure.Options;

/// <summary>
/// Represents configuration options for accessing the Google Books API.
/// </summary>
public class GoogleBooksOptions
{
    /// <summary>
    /// Gets or sets the base URL for API requests.
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;
    /// <summary>
    /// ApiKey used for authenticating requests to the Google Books API.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}
