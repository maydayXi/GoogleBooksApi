using System.ComponentModel;
using System.Reflection;

namespace ApiService.Extensions;

/// <summary>
/// Provides extension methods for retrieving field metadata.
/// </summary>
public static class FieldExtension
{
    /// <summary>
    /// Retrieves the value of the DisplayNameAttribute applied to the specified property of type T.
    /// </summary>
    /// <typeparam name="T">The type containing the property.</typeparam>
    /// <param name="propertyName">The name of the property to inspect.</param>
    /// <returns>The display name if the attribute is present; otherwise, an empty string.</returns>
    public static string GetDisplayName<T>(this string propertyName)
    {
        var prop = typeof(T).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (prop is null) return string.Empty;

        DisplayNameAttribute? displayAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
        return displayAttr?.DisplayName ?? string.Empty;
    }
}
