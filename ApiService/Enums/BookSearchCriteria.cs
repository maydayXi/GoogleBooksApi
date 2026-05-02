namespace ApiService.Enums;

/// <summary>
/// Book search criteria enumeration that defines the various criteria that can be used to search for books in the application.
/// </summary>
public enum BookSearchCriteria
{
    /// <summary>
    /// No specific search criteria.
    /// </summary>
    None,

    /// <summary>
    /// International Standard Book Number (ISBN) search criteria, which allows searching for books based on their unique ISBN identifiers.
    /// </summary>
    ISBN,

    /// <summary>
    /// Title search criteria, which allows searching for books based on their titles.
    /// </summary>
    Title,

    /// <summary>
    /// Author search criteria, which allows searching for books based on the names of their authors.
    /// </summary>
    Author,

    /// <summary>
    /// Publisher search criteria, which allows searching for books based on the names of their publishers.
    /// </summary>
    Publisher
}
