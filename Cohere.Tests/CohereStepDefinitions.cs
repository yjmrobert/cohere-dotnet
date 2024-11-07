using Reqnroll;
using Xunit;
using Xunit.Abstractions;

namespace Cohere.Tests;

[Binding]

/// <summary>
/// Defines the step definitions used by all Cohere tests
/// </summary>
public partial class CohereStepDefinitions
{
    protected CohereClient? _client;
    protected CohereHttpClientFake? _httpMessageHandlerFake;
    protected readonly ITestOutputHelper? _output;
    private readonly string _apiKey = "test-api-key";
    protected Exception? _caughtException;
    
    /// <summary>
    /// Verifies that an API key is available for testing
    /// Since no call to the API is made, the API key is not validated
    /// </summary>
    [Given(@"I have a valid API key")]
    public void GivenIHaveAValidApiKey()
    {
        Assert.NotNull(_apiKey);
    }

    /// <summary>
    /// Initializes a new instance of the Cohere client with a mock HttpClient for testing
    /// </summary>
    [Given(@"I have instantiated the Cohere client")]
    public void GivenIHaveInstantiatedTheCohereClient()
    { 
        _httpMessageHandlerFake = new CohereHttpClientFake();

        var httpClient = new HttpClient(_httpMessageHandlerFake)
        {
            BaseAddress = new Uri("https://api.cohere.ai")
        };
        _client = new CohereClient(_apiKey, httpClient);
    }
}