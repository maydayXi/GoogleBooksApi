using ApiService.Enums;

namespace ApiService.Dtos.Response;

/// <summary>
/// API response class representing the standard structure of responses returned by the API endpoints,
/// </summary>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the API request was successful or not. 
    /// </summary>
    /// <remarks>
    /// A value of true indicates a successful request, while false indicates a failure.
    /// </remarks>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Numeric HTTP status code returned by the API endpoint.
    /// </summary>
    public int HttpStatusCode { get; set; }

    /// <summary>
    /// API response status indicating the overall outcome of the API request
    /// </summary>
    public ApiResponseStatus ApiResponseStatus { get; set; }

    /// <summary>
    /// Response message providing additional information about the API response, such as error details or success messages.
    /// </summary>
    public string Message
    { get; set; } = string.Empty;

    /// <summary>
    /// Response data of generic type T
    /// </summary>
    public T? Data { get; set; }
}
