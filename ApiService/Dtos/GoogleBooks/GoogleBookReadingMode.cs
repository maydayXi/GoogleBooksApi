namespace ApiService.Dtos.GoogleBooks;

/// <summary>
/// Google Book Reading Mode Data Transfer Object (DTO) 
/// for encapsulating the relevant information about a book's reading mode
/// </summary>
public class GoogleBookReadingMode
{
    /// <summary>
    /// Gets or sets a value indicating whether the text is enabled.
    /// </summary>
    public bool Text { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the image is enabled.
    /// </summary>
    public bool Image { get; set; }
}
