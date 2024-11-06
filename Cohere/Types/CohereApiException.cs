using System.Net;

namespace Cohere.Types;

/// <summary>
/// Exception thrown when an error occurs while interacting with the Cohere API
/// </summary>
public class CohereApiException : Exception
{
    /// <summary>
    /// The status code of the response from the Cohere API
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// The endpoint that the error occurred on
    /// </summary>
    public string Endpoint { get; }

    /// <summary>
    /// The details of the error that occurred
    /// </summary>
    public string ErrorDetails { get; }

    /// <summary>
    /// Initializes a new instance of the CohereApiException class
    /// </summary>
    public CohereApiException(HttpStatusCode statusCode, string endpoint, string message, string errorDetails)
        : base($"Error from Cohere API: {message}")
    {
        StatusCode = statusCode;
        Endpoint = endpoint;
        ErrorDetails = errorDetails;
    }

    /// <summary>
    /// Returns a string representation of the CohereApiException
    /// </summary>
    public override string ToString()
    {
        return $"CohereApiException: StatusCode = {StatusCode}, Endpoint = {Endpoint}, Message = {Message}, Details = {ErrorDetails}";
    }
}
