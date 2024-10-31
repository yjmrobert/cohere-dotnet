using Reqnroll;
using Xunit;

namespace Cohere.IntegrationTests;

[Binding]

/// <summary>
/// Defines the step definitions used by all Cohere integration tests
/// </summary>
public class CohereStepDefinitions
{
    public CohereClient _client;
    
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