namespace GoogleBookApi.ViewModels;

/// <summary>
/// Represents an item in a dropdown list for view models.
/// </summary>
public class DropdownItemVm
{
    /// <summary>
    /// Gets or sets the text to display.
    /// </summary>
    public required string DisplayText { get; set; }

    /// <summary>
    /// Icon CSS classes.
    /// </summary>
    public string Icon { get; set; } = string.Empty;
}
