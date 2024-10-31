using Cohere.Types;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;

namespace Cohere.IntegrationTests.Classify;

[Binding, Scope(Feature = "Classify Integration")]

/// <summary>
/// Step definitions for the Classify feature
/// </summary>
public class ClassifyStepDefinitions
{
    private readonly CohereStepDefinitions _cohereStepDefinitions;
    private ClassifyResponse? _classifyResponse;

    public ClassifyStepDefinitions(CohereStepDefinitions cohereStepDefinitions)
    {
        _cohereStepDefinitions = cohereStepDefinitions;
    }

    /// <summary>
    /// Sends ValidClassifyRequest1 to the Cohere API
    /// </summary>
    [When(@"I send a valid classify request")]
    public async Task WhenISendAValidClassifyRequest()
    {
        _classifyResponse = await _cohereStepDefinitions._client.ClassifyAsync(CohereApiRequests.ValidClassifyRequest1);
    }

    /// <summary>
    /// Verifies that ValidClassifyResponse1 is received from the Cohere API and checks parsing of response fields
    /// </summary>
    [Then(@"I should receive a valid classification response")]
    public void ThenIShouldReceiveAValidClassifyResponse()
    {
        Assert.NotNull(_classifyResponse);
        Assert.Equal("Not spam", _classifyResponse.Classifications[0].Predictions[0]);
        Assert.Equal("Spam", _classifyResponse.Classifications[1].Predictions[0]);
        Assert.Equal(0.5661598, _classifyResponse.Classifications[0].Confidences[0]);
        Assert.Equal(0.43384025, _classifyResponse.Classifications[0].Labels["Spam"].Confidence);
        Assert.Equal("single-label", _classifyResponse.Classifications[0].ClassificationType);
        Assert.Equal("Confirm your email address", _classifyResponse.Classifications[0].Input);
        Assert.Equal(typeof(string), _classifyResponse.Id.GetType());
        Assert.Equal(typeof(CohereMeta), _classifyResponse.Meta?.GetType());
        Assert.Equal("2", _classifyResponse.Meta?.ApiVersion?.Version);
        Assert.Equal(2, _classifyResponse.Meta?.BilledUnits?.Classifications);
    }
}