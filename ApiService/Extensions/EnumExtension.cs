using System.ComponentModel;
using System.Reflection;

namespace ApiService.Extensions;

/// <summary>
/// Provider extension methods for <see cref="Enum"/> types.
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// Retrieves the <see cref="DescriptionAttribute"/> value of an enum number.
    /// </summary>
    /// <param name="value">
    /// The enum value to retrieve the description for.
    /// </param>
    /// <returns>
    /// The description string defined by <see cref="DescriptionAttribute"/> or <see cref="string.Empty"/>
    /// if no description not found.
    /// </returns>
    public static string GetDescription(this Enum value)
    {
        if (value.GetType().GetMember($"{value}").FirstOrDefault() is not { } memberInfo) 
            return string.Empty;

        return memberInfo.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault() 
            is { } descriptionAttribute
            ? descriptionAttribute.Description
            : string.Empty;
    }
}