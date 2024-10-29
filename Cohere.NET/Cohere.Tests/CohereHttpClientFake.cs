using System.Net;
using System.Text;

namespace Cohere.Tests;
public class CohereHttpClientFake : HttpMessageHandler
{
    /// <summary>
    /// The JSON content to be returned in the fake response
    /// </summary>
    public string ResponseContent { get; set; }

    /// <summary>
    /// The HTTP status code to return in the fake response
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Initializes a new instance of the CohereHttpClientFake class with default response content and status code
    /// </summary>
    public CohereHttpClientFake(string responseContent = "{}", HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        ResponseContent = responseContent;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Sends a simulated HTTP response based on predefined content and status code
    /// </summary>
    /// <param name="request">The HTTP request message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the HTTP response message</returns>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage
        {
            StatusCode = StatusCode,
            Content = new StringContent(ResponseContent, Encoding.UTF8, "application/json")
        };
        return Task.FromResult(response);
    }
}