using GoogleBookApi.ViewModels;
using GoogleBookApi.ViewModels.JSONLD;

namespace GoogleBookApi.Extensions;

/// <summary>
/// Provider extension methods for convert <see cref="BookVm" /> collections
/// to Schema.org structured data.
/// </summary>
public static class SchemaExtensions
{
    /// <summary>
    /// Convert a collection of <see cref="BookVm"/> to a Schema.org ItemList.
    /// </summary>
    /// <param name="books"> Collection of books </param>
    /// <returns> Schema.org ItemList </returns>
    public static ItemList ToSchemaItemList(this IEnumerable<BookVm> books) => new()
    {
        ItemListElement = books.Select((book,index) => new ListItem
        {
            Position = index + 1,
            Item = book.ToSchemaBook()
        })
    };
    
    /// <summary>
    /// Convert a single <see cref="BookVm" /> to a Schema.org Book.
    /// </summary>
    /// <param name="bookVm">  </param>
    /// <returns></returns>
    private static Book ToSchemaBook(this BookVm bookVm) => new()
    {
        Name = bookVm.Title,
        Author = new Person
        {
            Name = bookVm.Author
        },
        Publisher = new Organization
        {
            Name = bookVm.Publisher
        },
        Isbn = string.IsNullOrWhiteSpace(bookVm.Isbn13)
            ? bookVm.Isbn10
            : bookVm.Isbn13,
        DatePublished = $"{bookVm.PublishedDate:yyyy-MM-dd}",
        Image = bookVm.ImageLink,
        Description = bookVm.Description
    };
}