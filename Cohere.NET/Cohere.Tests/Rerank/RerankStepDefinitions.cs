using Cohere.Types;
using Reqnroll;
using Xunit;

namespace Cohere.Tests.Rerank;

[Binding, Scope(Feature = "Rerank")]

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
    /// Sends a ValidRerankRequest1 to the mocked Cohere API and sets the ValidRerankResponse1 to be returned
    /// </summary>
    [When(@"I send a valid rerank request")]
    public async Task WhenISendAValidRerankRequest()
    {
        _cohereStepDefinitions._httpMessageHandlerFake.ResponseContent = CohereApiResponses.ValidRerankResponse1;
        _rerankResponse = await _cohereStepDefinitions._client.RerankAsync(CohereApiRequests.ValidRerankRequest1); 
    }

    /// <summary>
    /// Verifies that ValidRerankResponse1 is received from the mocked Cohere API and checks parsing of response fields
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
        Assert.True(_rerankResponse.Meta?.ApiVersion?.IsExperimental);
        Assert.Equal(1, _rerankResponse.Meta?.BilledUnits?.SearchUnits);
        Assert.Equal(typeof(List<string>), _rerankResponse.Meta?.Warnings?.GetType());
    }
}