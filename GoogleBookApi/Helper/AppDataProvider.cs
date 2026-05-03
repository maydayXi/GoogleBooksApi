using ApiService.Interface;
using System.Text.Json;

namespace GoogleBookApi.Helper;

/// <summary>
/// Provides access to application data stored in JSON files using the web hosting environment.
/// </summary>
/// <typeparam name="T">The type of data to deserialize from JSON.</typeparam>
/// <param name="webHostEnvironment">The web hosting environment used to resolve file paths.</param>
public class AppDataProvider<T>(IWebHostEnvironment webHostEnvironment) : IJsonDataProvider<T> where T : class
{
    /// <summary>
    /// Provides information about the web hosting environment an application is running in.
    /// </summary>
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    /// <summary>
    /// Provides JSON serialization options with case-insensitive property name matching.
    /// </summary>
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Gets a list of data of type T from a JSON file located at the specified filepath.    
    /// </summary>
    /// <param name="filepath">The relative path to the JSON file.</param>
    /// <returns>A read-only list of data of type T.</returns>
    public IReadOnlyList<T> GetListDataFromJson(string filepath)
    {
        string fullFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filepath);
        if (string.IsNullOrWhiteSpace(filepath) || !File.Exists(fullFilePath)) return [];

        string json = File.ReadAllText(fullFilePath);
        if (string.IsNullOrWhiteSpace(json)) return [];

        try
        {
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? [];
        }
        catch
        {
            throw;
        }
    }
}
