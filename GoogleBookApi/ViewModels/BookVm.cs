namespace GoogleBookApi.ViewModels;

/// <summary>
/// Represents the view model for a book.
/// </summary>
public class BookVm
{
    /// <summary>
    /// Title of the book.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Authors of the book.
    /// </summary>
    public required string Author { get; set; }

    /// <summary>
    /// Publisher of the book.
    /// </summary>
    public required string Publisher { get; set; }

    /// <summary>
    /// Description of the book.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// 10-digit International Standard Book Number (ISBN-10) of the book.
    /// </summary>
    public required string Isbn10 { get; set; }

    /// <summary>
    /// 13-digit International Standard Book Number (ISBN-13) of the book.
    /// </summary>
    public required string Isbn13 { get; set; }

    /// <summary>
    /// Published date of the book.
    /// </summary>
    public required DateOnly? PublishedDate { get; set; }

    /// <summary>
    /// Cover image link of the book.
    /// </summary>
    public string ImageLink { get; set; } = string.Empty;
}
