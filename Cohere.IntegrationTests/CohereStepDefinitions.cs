using Reqnroll;
using Xunit;
using Xunit.Abstractions;

namespace Cohere.IntegrationTests;

[Binding]

/// <summary>
/// Defines the step definitions used by all Cohere integration tests
/// </summary>
public partial class CohereStepDefinitions
{
    private  CohereClient? _client;
    private readonly ITestOutputHelper? _output;
    private Exception? _caughtException;

    public CohereStepDefinitions(ITestOutputHelper output)
    {
        _output = output;
    }
    
    /// <summary>
    /// Verifies that an API key is available for testing
    /// </summary>
    [Given(@"I have a valid API key")]
    public static void GivenIHaveAValidApiKey()
    {
        Assert.NotNull(Configuration.ApiKey);
    }

    /// <summary>
    /// Initializes a new instance of the Cohere client
    /// </summary>
    [Given(@"I have instantiated the Cohere client")]
    public void GivenIHaveInstantiatedTheCohereClient()
    { 
        _client = new CohereClient(Configuration.ApiKey);
    }
}