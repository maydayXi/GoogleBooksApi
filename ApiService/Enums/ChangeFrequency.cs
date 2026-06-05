using System.ComponentModel;

namespace ApiService.Enums;

/// <summary>
/// Defines how frequently a page is likely to change.
/// </summary>
/// <remarks>
/// <see cref="https//www.sitemaps.org/protocol.html" />
/// </remarks>
public enum ChangeFrequency
{
    /// <summary>
    /// The page changes every week.
    /// </summary>
    [Description("weekly")]
    Weekly,
    
    /// <summary>
    /// The page changes every month.
    /// </summary>
    [Description("monthly")]
    Monthly
}