using ApiService.Enums;

namespace GoogleBookApi.ViewModels.Components;

/// <summary>
/// Represents an item in a dropdown list for view models.
/// </summary>
public class DropdownItemVm
{
    /// <summary>
    /// Display text for the dropdown item, which is derived from the associated book search criteria.
    /// </summary>
    public string DisplayText => $"{Criteria}";

    /// <summary>
    /// Icon CSS classes.
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Book search criteria associated with this dropdown item, defaulting to ISBN.
    /// </summary>
    public BookSearchCriteria Criteria { get; set; } = BookSearchCriteria.ISBN;
}
