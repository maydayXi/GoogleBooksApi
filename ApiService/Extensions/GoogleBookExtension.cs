using ApiService.Dtos;
using ApiService.Dtos.GoogleBooks;
using ApiService.Enums;

namespace ApiService.Extensions;

/// <summary>
/// Google Book Extension class provides extension methods for handling Google Book related operations,
/// </summary>
public static class GoogleBookExtension
{
    /// <summary>
    /// Converts a GoogleBookItemDto to a BookSearchResponseDto, 
    /// mapping relevant properties and handling book identifiers.
    /// </summary>
    /// <param name="googleBook">The Google Book item DTO to convert.</param>
    /// <returns> Book response </returns>
    public static BookSearchResponseDto ToBookSearchResponse(this GoogleBookItemDto googleBook)
    {
        var bookIdentifier = googleBook.VolumeInfo?.IndustryIdentifiers;

        BookSearchResponseDto searchResponse = new()
        {
            Title = googleBook.VolumeInfo?.Title ?? string.Empty,
            Author = googleBook.VolumeInfo?.Authors != null
                ? string.Join("/", googleBook.VolumeInfo.Authors)
                : string.Empty,
            Description = googleBook.VolumeInfo?.Description ?? string.Empty,
            ImageLink = googleBook.VolumeInfo?.ImageLinks.Thumbnail ?? string.Empty,
            Publisher = googleBook.VolumeInfo?.Publisher ?? string.Empty,
            PublishedDate = DateOnly.TryParse(googleBook.VolumeInfo?.PublishedDate, out DateOnly publishedDate)
                ? publishedDate
                : null
        };

        if (bookIdentifier is null) return searchResponse;

        bookIdentifier.ForEach(identifier =>
        {
            if (!Enum.IsDefined(typeof(BookIdentifierType), identifier.Type) ||
                !Enum.TryParse(identifier.Type, out BookIdentifierType result))
                return;

            searchResponse.BookIdentifier[result] = identifier.Identifier;
        });

        return searchResponse;
    }
}
