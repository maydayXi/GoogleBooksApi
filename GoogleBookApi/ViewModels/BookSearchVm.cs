using GoogleBookApi.ViewModels.Components;

namespace GoogleBookApi.ViewModels;

/// <summary>
/// View model for book search page
/// </summary>
public class BookSearchVm
{
    /// <summary>
    /// Gets or sets the list of dropdown items for the book search functionality.
    /// </summary>
    public List<DropdownItemVm> DropdownItems { get; set; } = [];
}
