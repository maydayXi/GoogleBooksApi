namespace GoogleBookApi.ViewModels.Components;

/// <summary>
/// Represents the pagination state for a paged list of items.
/// </summary>
public class PaginationVm
{
    /// <summary>
    /// The total number of pages.
    /// </summary>
    public int TotalPage { get; init; }
    
    /// <summary>
    /// Current page number.
    /// </summary>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Indicates whether any data exists.
    /// </summary>
    public bool HasData => TotalPage > 0;
    
    /// <summary>
    /// Indicates whether the previous page exists.
    /// </summary>
    public bool HasPreviousPage => HasData && CurrentPage > 1;

    /// <summary>
    /// Indicates whether the next page exists.
    /// </summary>
    public bool HasNextPage => HasData && CurrentPage < TotalPage;
}