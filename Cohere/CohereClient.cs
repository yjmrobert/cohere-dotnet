using Cohere.CustomJsonConverters;
using Cohere.SampleRequestsAndResponses;
using Cohere.Types;
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
public class CohereClient
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
    /// <returns> The response from Cohere as a ChatResponse object </returns>
    public async Task<ChatResponse> ChatAsync(ChatRequest chatRequest)
    {
        var response = await SendRequestAsync("chat", CohereApiRequests.ValidChatRequest1);

        return (ChatResponse) response;
    }

    /// <summary>
    /// Sends a request to the Cohere API to classify text
    /// </summary>
    /// <param name="classifyRequest"> The request body sent to Cohere to classify </param>
    /// <returns> The response from Cohere as a ClassifyResponse object </returns>
    public async Task<ClassifyResponse> ClassifyAsync(ClassifyRequest classifyRequest)
    {
        var response = await SendRequestAsync("classify", classifyRequest);

        return (ClassifyResponse) response;
    }

    /// <summary>
    /// Sends a request to the Cohere API to rerank text
    /// </summary>
    /// <param name="rerankRequest"> The request body sent to Cohere to rerank </param>
    /// <returns> The response from Cohere as a RerankResponse object </returns>
    public async Task<RerankResponse> RerankAsync(RerankRequest rerankRequest)
    {
        var response = await SendRequestAsync("rerank", rerankRequest);

        return (RerankResponse) response;
    }

    /// <summary>
    /// Private method to send a request to the Cohere API
    /// </summary>
    /// <param name="endpoint"> The endpoint to send the request to </param>
    /// <param name="requestBody"> The request body to send to the API </param>
    /// <returns> The response from the API as an ICohereResponse object </returns> 
    private async Task<ICohereResponse> SendRequestAsync(string endpoint, ICohereRequest requestBody)
    {
        var serializedRequest = endpoint switch
        {
            "chat" => JsonSerializer.Serialize((ChatRequest)requestBody, _jsonSerializerOptions),
            "classify" => JsonSerializer.Serialize((ClassifyRequest)requestBody, _jsonSerializerOptions),
            "rerank" => JsonSerializer.Serialize((RerankRequest)requestBody, _jsonSerializerOptions),
            _ => throw new InvalidOperationException("Invalid endpoint provided."),
        };

        var response = await GetRetryPolicy().ExecuteAsync(async () => {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.cohere.ai/v2/{endpoint}")
            {
                Content = new StringContent(serializedRequest, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
            };

            return await _httpClient.SendAsync(request);
        });

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Failed to retrieve results from Cohere. Status code: {response.StatusCode}");
            throw new HttpRequestException($"Cohere API call failed with status code: {errorContent}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();

        ICohereResponse result = endpoint switch
        {
            "chat" => DeserializeResponse<ChatResponse>(responseContent),
            "classify" => DeserializeResponse<ClassifyResponse>(responseContent),
            "rerank" => DeserializeResponse<RerankResponse>(responseContent),
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