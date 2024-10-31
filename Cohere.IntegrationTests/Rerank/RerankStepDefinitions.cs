using Cohere.Types;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;

namespace Cohere.IntegrationTests.Rerank;

[Binding, Scope(Feature = "Rerank Integration")]

/// <summary>
/// Step definitions for the Rerank feature
/// </summary>
public class RerankStepDefinitions
{
    private readonly CohereStepDefinitions _cohereStepDefinitions;
    private RerankResponse? _rerankResponse;

    public RerankStepDefinitions(CohereStepDefinitions cohereStepDefinitions)
    {
        _cohereStepDefinitions = cohereStepDefinitions;
    }

    /// <summary>
    /// Sends a ValidRerankRequest1 to the Cohere API
    /// </summary>
    [When(@"I send a valid rerank request")]
    public async Task WhenISendAValidRerankRequest()
    {
        _rerankResponse = await _cohereStepDefinitions._client.RerankAsync(CohereApiRequests.ValidRerankRequest1); 
    }

    /// <summary>
    /// Verifies that ValidRerankResponse1 is received from the Cohere API and checks parsing of response fields
    /// </summary>
    [Then(@"I should receive a valid rerank response")]
    public void ThenIShouldReceiveAValidRerankResponse()
    {
        Assert.NotNull(_rerankResponse);
        Assert.Equal(3, _rerankResponse.Results.Count());
        Assert.Equal(3, _rerankResponse.Results[0].Index);
        Assert.Equal(typeof(double), _rerankResponse.Results[0].RelevanceScore.GetType());
        Assert.Equal(typeof(CohereMeta), _rerankResponse.Meta?.GetType());
        Assert.Equal("2", _rerankResponse.Meta?.ApiVersion?.Version);
        Assert.Equal(1, _rerankResponse.Meta?.BilledUnits?.SearchUnits);
    }
}