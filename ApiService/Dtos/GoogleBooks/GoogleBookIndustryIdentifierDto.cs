namespace ApiService.Dtos.GoogleBooks;

/// <summary>
/// Google Book Industry Identifier Data Transfer Object (DTO)
/// </summary>
public class GoogleBookIndustryIdentifierDto
{
    /// <summary>
    /// Google book identifier type, representing the type of industry identifier (e.g., ISBN_10, ISBN_13) 
    /// as provided by the Google Books API.
    /// </summary>
    public required string Type { get; set; }
    /// <summary>
    /// Google book identifier, representing the actual identifier value (e.g., the ISBN number)
    /// </summary>
    public required string Identifier { get; set; }
}
