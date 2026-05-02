using Microsoft.AspNetCore.Mvc;

namespace GoogleBookApi.Helper;

/// <summary>
/// Provides helper methods.
/// </summary>
public class WebHelper
{
    /// <summary>
    /// Gets the name of the specified controller type without the 'Controller' suffix. 
    /// </summary>
    /// <typeparam name="TController">The controller type.</typeparam>
    /// <returns>The controller name without the 'Controller' suffix.</returns>
    public static string GetControllerName<TController>() =>
        typeof(TController).Name.Replace(nameof(Controller), string.Empty);
}
