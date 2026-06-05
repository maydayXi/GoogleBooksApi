namespace GoogleBookApi.Models;

/// <summary>
/// Represents a single URL entry in the sitemap
/// </summary>
/// <param name="Loc">The absolute URL of the page to index.</param>
/// <param name="ChangeFreq">The expected frequency of page changes (e.g. daily, weekly, monthly). </param>
/// <param name="Priority"> The priority of this URL relative to other URLs on the site, ranging from 0.0 to 1.0 </param>
public record SitemapUrl(
    string Loc,
    string ChangeFreq,
    string Priority);