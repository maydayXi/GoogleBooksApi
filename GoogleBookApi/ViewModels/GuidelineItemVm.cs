namespace GoogleBookApi.ViewModels;

/// <summary>
/// Represents a view model for an individual guideline item, displaying guideline information in the application.
/// </summary>
public class GuidelineItemVm
{
    /// <summary>
    /// Guideline icon CSS classes.
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Guideline title to be displayed in the view.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Guideline description to be displayed in the view.
    /// </summary>
    public required string Description { get; set; }
}
