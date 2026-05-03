using ApiService.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GoogleBookApi.Helper;

/// <summary>
/// Provides helper methods.
/// </summary>
public class WebHelper
{
    /// <summary>
    /// Gets the relative file path to the guideline JSON file.
    /// </summary>
    public static string GuidelineJsonFilePath => @"data\guideline.json";

    /// <summary>
    /// Gets the relative file path to the version.json data file.
    /// </summary>
    public static string VersionJsonFilePath => @"data\version.json";

    /// <summary>
    /// Gets the icon class for a dropdown item based on the specified book search criteria.
    /// </summary>
    /// <param name="criteria">The book search criteria.</param>
    /// <returns>The icon class for the dropdown item.</returns>
    public static string GetDropdownItemIconByCriteria(BookSearchCriteria criteria) =>
        criteria switch
        {
            BookSearchCriteria.ISBN => "bi bi-upc",
            BookSearchCriteria.Title => "bi bi-journal-text",
            BookSearchCriteria.Author => "bi bi-people-fill",
            BookSearchCriteria.Publisher => "bi bi-building",
            _ => string.Empty
        };

    /// <summary>
    /// Gets the name of the specified controller type without the 'Controller' suffix. 
    /// </summary>
    /// <typeparam name="TController">The controller type.</typeparam>
    /// <returns>The controller name without the 'Controller' suffix.</returns>
    public static string GetControllerName<TController>() =>
        typeof(TController).Name.Replace(nameof(Controller), string.Empty);

    /// <summary>
    /// Gets the ISBN-10 and ISBN-13 values from the specified book identifier dictionary.
    /// </summary>
    /// <param name="bookIdentifier">A dictionary containing book identifiers with their corresponding types.</param>
    /// <returns>A tuple containing the ISBN-10 and ISBN-13 values.</returns>
    public static (string ISBN10, string ISBN13) GetIsbnByTypeFromBookIdentifier(Dictionary<BookIdentifierType, string> bookIdentifier)
    {
        bookIdentifier.TryGetValue(BookIdentifierType.ISBN_10, out string? isbn10);
        bookIdentifier.TryGetValue(BookIdentifierType.ISBN_13, out string? isbn13);
        return (isbn10 ?? string.Empty, isbn13 ?? string.Empty);
    }
}
