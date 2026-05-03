using ApiService.Dtos;
using GoogleBookApi.Helper;
using GoogleBookApi.ViewModels;

namespace GoogleBookApi.Extensions;

public static class ResponseDataExtension
{
    /// <summary>
    /// Convert BookSearchResponseDto to BookVm
    /// </summary>
    /// <param name="book">The book response data transfer object to convert.</param>
    /// <returns>A Book view model representing the book information.</returns>
    public static BookVm ToBookViewModel(this BookSearchResponseDto book)
    {
        (string isbn10, string isbn13) = WebHelper.GetIsbnByTypeFromBookIdentifier(book.BookIdentifier);
        string description = book.Description.Length > 150
            ? $"{book.Description[..150]}..."
            : book.Description;

        return new BookVm
        {
            Title = book.Title,
            Author = book.Author,
            Description = description,
            ImageLink = book.ImageLink,
            Publisher = book.Publisher,
            PublishedDate = book.PublishedDate,
            Isbn10 = isbn10,
            Isbn13 = isbn13
        };
    }

    /// <summary>
    /// Convert a collection of BookSearchResponseDto objects to a collection of BookVm objects.
    /// </summary>
    /// <param name="books">The books response data transfer object to convert.</param>
    /// <returns>A collection of Book view models representing the book information.</returns>
    public static IEnumerable<BookVm> ToBooksViewModel(this IEnumerable<BookSearchResponseDto> books) => books.Select(book => book.ToBookViewModel());
}
