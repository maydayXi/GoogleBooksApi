namespace GoogleBookApi.ViewModels.Components;

/// <summary>
/// Represent the view model for a page title component.
/// </summary>
/// <param name="Title"> The primary title of the page. </param>
/// <param name="SubTitle"> The secondary title or subtitle of the page. </param>
public record PageTitleVm(
    string Title,
    string SubTitle);