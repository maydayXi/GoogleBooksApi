namespace GoogleBookApi.ViewModels.Components;


public class BookSearchResultVm
{
    /// <summary>
    /// 
    /// </summary>
    public List<BookVm> Books { get; init; } = [];

    /// <summary>
    /// Pagination metadata for the book search result.
    /// </summary>
    public PaginationVm Pagination { get; init; } = new();
}