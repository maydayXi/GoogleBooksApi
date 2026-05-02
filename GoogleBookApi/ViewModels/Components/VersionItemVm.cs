namespace GoogleBookApi.ViewModels.Components;

/// <summary>
/// Represents the view model for displaying version information in the application.
/// </summary>
public class VersionItemVm
{
    /// <summary>
    /// Version number of the application or API, represented as a string.
    /// </summary>
    /// <remarks>
    /// This property is used to display the current version information to the users.
    /// </remarks>
    public required string Version { get; set; } = string.Empty;

    /// <summary>
    /// Release date of the version, represented as a <see cref="DateOnly"/> type.
    /// </summary>
    public required DateOnly ReleasedDate { get; set; }

    /// <summary>
    /// Version description providing details about the changes, improvements, or fixes included in this version.
    /// </summary>
    public required string Description { get; set; } = string.Empty;
}
