using Cohere.Types.Chat;
using Cohere.Types.Classify;
using Cohere.Types.Rerank;
using Cohere.Types.Shared;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cohere;

/// <summary>
/// The CohereClient class to interact with the Cohere API
/// </summary>
public class CohereClient: IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly int _retryCount = 3;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() }
    };

    /// <summary>
    /// Initalizes a new instance of the CohereClient class
    /// </summary>
    /// <param name="apiKey"> The API key to use for requests </param>
    public CohereClient(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    /// <summary>
    /// Initalizes a new instance of the CohereClient class with a custom HttpClient (e.g a mock)
    /// </summary>
    /// <param name="apiKey"> The API key to use for requests </param>
    /// <param name="httpClient"> The custom HttpClient to use for requests </param>
    public CohereClient(string apiKey, HttpClient httpClient)
    {
        _apiKey = apiKey;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    /// <summary>
    /// Sends a request to the Cohere API to generate text based on messages provided
    /// </summary>
    /// <param name="chatRequest"> The request body sent to Cohere to generate text </param>
    /// <param name="cancellationToken"> The cancellation token to cancel the request </param>
    /// <returns> The response from Cohere as a ChatResponse object </returns>
    public async Task<ChatResponse> ChatAsync(ChatRequest chatRequest, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(chatRequest);

        var response = await SendRequestAsync(CohereEndpointsEnum.Chat, chatRequest, cancellationToken);

        return (ChatResponse) response;
    }

    /// <summary>
    /// Sends a request to the Cohere API to classify text
    /// </summary>
    /// <param name="classifyRequest"> The request body sent to Cohere to classify </param>
    /// <param name="cancellationToken"> The cancellation token to cancel the request </param>
    /// <returns> The response from Cohere as a ClassifyResponse object </returns>
    public async Task<ClassifyResponse> ClassifyAsync(ClassifyRequest classifyRequest, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(classifyRequest);

        var response = await SendRequestAsync(CohereEndpointsEnum.Classify, classifyRequest, cancellationToken);

        return (ClassifyResponse) response;
    }

    /// <summary>
    /// Sends a request to the Cohere API to rerank text
    /// </summary>
    /// <param name="rerankRequest"> The request body sent to Cohere to rerank </param>
    /// <param name="cancellationToken"> The cancellation token to cancel the request </param>
    /// <returns> The response from Cohere as a RerankResponse object </returns>
    public async Task<RerankResponse> RerankAsync(RerankRequest rerankRequest, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(rerankRequest);

        var response = await SendRequestAsync(CohereEndpointsEnum.Rerank, rerankRequest, cancellationToken);

        return (RerankResponse) response;
    }

    /// <summary>
    /// Private method to send a request to the Cohere API
    /// </summary>
    /// <param name="endpoint"> The endpoint to send the request to </param>
    /// <param name="requestBody"> The request body to send to the API </param>
    /// <param name="cancellationToken"> The cancellation token to cancel the request </param>
    /// <returns> The response from the API as an ICohereResponse object </returns> 
    private async Task<ICohereResponse> SendRequestAsync(CohereEndpointsEnum endpoint, ICohereRequest requestBody, CancellationToken cancellationToken)
    {
        var serializedRequest = endpoint switch
        {
            CohereEndpointsEnum.Chat => JsonSerializer.Serialize((ChatRequest)requestBody, _jsonSerializerOptions),
            CohereEndpointsEnum.Classify => JsonSerializer.Serialize((ClassifyRequest)requestBody, _jsonSerializerOptions),
            CohereEndpointsEnum.Rerank => JsonSerializer.Serialize((RerankRequest)requestBody, _jsonSerializerOptions),
            _ => throw new InvalidOperationException("Invalid endpoint provided."),
        };

        var response = await GetRetryPolicy().ExecuteAsync(async ct =>
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.cohere.ai/v2/{endpoint.ToString().ToLower()}")
            {
                Content = new StringContent(serializedRequest, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
            };
            return await _httpClient.SendAsync(request, ct);
            
        }, cancellationToken);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string errorMessage;
            string errorDetails;

            try
            {
                var errorResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent, _jsonSerializerOptions);
                errorMessage = errorResponse?.GetValueOrDefault("message") ?? "An unknown error occurred.";
                errorDetails = JsonSerializer.Serialize(errorResponse, _jsonSerializerOptions);
            }
            catch (JsonException)
            {
                errorMessage = responseContent;
                errorDetails = string.Empty;
            }

            throw new CohereApiException(response.StatusCode, endpoint, errorMessage, errorDetails);
        }

        ICohereResponse result = endpoint switch
        {
            CohereEndpointsEnum.Chat => DeserializeResponse<ChatResponse>(responseContent),
            CohereEndpointsEnum.Classify => DeserializeResponse<ClassifyResponse>(responseContent),
            CohereEndpointsEnum.Rerank => DeserializeResponse<RerankResponse>(responseContent),
            _ => throw new InvalidOperationException("Invalid endpoint provided.")
        };

        return result;
    }

    /// <summary>
    /// Private generic method to deserialize the response from the Cohere API
    /// </summary>
    /// <param name="responseContent"> The content of the response from the API </param>
    /// <returns> The deserialized response as a generic type </returns>
    private T DeserializeResponse<T>(string responseContent) where T : ICohereResponse
    {
        return JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions)
            ?? throw new InvalidOperationException("Failed to deserialize the response from Cohere.");
    }

    /// <summary>
    /// Private method to get the retry policy for the HttpClient
    /// </summary>
    /// <returns> The retry policy for the HttpClient </returns>
    private AsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg =>
                msg.StatusCode is System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    /// <summary>
    /// Disposes of the HttpClient when the CohereClient is disposed
    /// </summary>
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}