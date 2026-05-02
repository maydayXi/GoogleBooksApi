using ApiService.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;

namespace GoogleBookApi.Helper.TagHelpers;

/// <summary>
/// Custom tag helper for rendering a dropdown item in a Bootstrap-styled dropdown menu.
/// </summary>
[HtmlTargetElement("dropdonw-item")]
public class DropdownItemTagHelper : TagHelper
{
    /// <summary>
    /// Icon CSS classses
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// HTML aria-label attribute
    /// </summary>
    [HtmlAttributeName("aria-label"), DisplayName("aria-label")]
    public string AriaLabel { get; set; } = string.Empty;

    /// <summary>
    /// button tag name.
    /// </summary>
    private readonly string button = nameof(button);

    /// <summary>
    /// Processes the tag helper asynchronously.
    /// </summary>
    /// <param name="context">The context in which the tag helper is operating.</param>
    /// <param name="output">The output of the tag helper.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // Generate button tag.
        output.TagName = button;
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("type", button);
        output.Attributes.SetAttribute("class", "dropdown-item");

        if (!string.IsNullOrWhiteSpace(AriaLabel))
            output.Attributes.SetAttribute(AriaLabel.GetDisplayName<DropdownItemTagHelper>(), AriaLabel);

        // Tag child content
        string childContent = (await output.GetChildContentAsync()).GetContent();

        // Html icon tag
        string iconHtml = string.Empty;
        if (!string.IsNullOrWhiteSpace(Icon))
            iconHtml = $@"<i class=""{Icon} me-2""></i>";

        output.Content.SetHtmlContent($"{iconHtml} {childContent}");
    }
}
