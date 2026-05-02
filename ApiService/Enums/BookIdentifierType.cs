namespace ApiService.Enums;

/// <summary>
/// Book identifier type enumeration, 
/// representing the different types of identifiers that can be associated with a book,
/// </summary>
public enum BookIdentifierType : int
{
    None = 0,
    /// <summary>
    /// International Standard Book Number (ISBN) 10-digit format
    /// </summary>
    ISBN_10 = 1,
    /// <summary>
    /// International Standard Book Number (ISBN) 13-digit format
    /// </summary>
    ISBN_13 = 2,
}
