namespace ApiService.Interface;

/// <summary>
/// Providers access to static application data load from configuration file.
/// </summary>
public interface IJsonDataProvider<T> where T : class
{
    /// <summary>
    /// Deserializes JSON data into a read-only list of type T.
    /// </summary>
    /// <param name="filepath"> JSON file relative path </param>
    /// <returns>A read-only list containing the deserialized objects.</returns>
    IReadOnlyList<T> GetListDataFromJson(string filepath);
}
