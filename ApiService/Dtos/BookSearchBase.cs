using ApiService.Enums;

namespace ApiService.Dtos;

/// <summary>
/// Base DTO for book search operations, containing common pagination parameters.
/// </summary>
public class BookSearchBase
{
    /// <summary>
    /// The number of book items to return per page. Defaults to <see cref="PageSize.Ten"/>.
    /// </summary>
    public PageSize PageSize { get; set; } = PageSize.Ten;

    /// <summary>
    /// The one-based index of the current page to retrieve.
    /// </summary>
    public int Page { get; set; } = 1;
}