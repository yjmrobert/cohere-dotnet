using Cohere.Types.Classify;
using Cohere.Types.Shared;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;
using Xunit.Abstractions;

namespace Cohere.IntegrationTests.Classify;

[Binding, Scope(Feature = "Classify Integration")]

/// <summary>
/// Step definitions for the Classify feature
/// </summary>
public class ClassifyStepDefinitions
{
    private readonly CohereStepDefinitions _cohereStepDefinitions;
    private ClassifyResponse? _classifyResponse;
    private Exception? _caughtException;
    private readonly ITestOutputHelper _output;

    public ClassifyStepDefinitions(CohereStepDefinitions cohereStepDefinitions, ITestOutputHelper output)
    {
        _cohereStepDefinitions = cohereStepDefinitions;
        _output = output;
    }

   /// <summary>
    /// Sends a valid classify request with various configurations to the Cohere API endpoint
    /// </summary>
    [When(@"I send a valid classify request with ""(.*)""")]
    public async Task WhenISendAValidClassifyRequestWith(string testCase)
    {
        if (_cohereStepDefinitions._client != null)
        {
            _classifyResponse = await _cohereStepDefinitions._client.ClassifyAsync(SampleClassifyRequests.GetClassifyRequest(testCase));
        }
        else
        {
            throw new InvalidOperationException("Client is not initialized.");
        }
    }

    /// <summary>
    /// Verifies that a valid classify response is received from the Cohere API and checks parsing of response fields
    /// </summary>
    [Then(@"I should receive a valid classify response")]
    public void ThenIShouldReceiveAValidClassifyResponse()
    {
        Assert.NotNull(_classifyResponse);
        
        Assert.IsType<string>(_classifyResponse.Id);
        Assert.False(string.IsNullOrWhiteSpace(_classifyResponse.Id));

        Assert.NotNull(_classifyResponse.Classifications);
        Assert.NotEmpty(_classifyResponse.Classifications);

        foreach (var classification in _classifyResponse.Classifications)
        {
            Assert.IsType<string>(classification.ClassificationType);
            Assert.False(string.IsNullOrWhiteSpace(classification.ClassificationType));

            Assert.NotNull(classification.Predictions);
            Assert.NotEmpty(classification.Predictions);
            Assert.All(classification.Predictions, prediction => Assert.IsType<string>(prediction));

            Assert.NotNull(classification.Confidences);
            Assert.NotEmpty(classification.Confidences);
            Assert.All(classification.Confidences, confidence => Assert.IsType<double>(confidence));

            Assert.NotNull(classification.Labels);
            Assert.NotEmpty(classification.Labels);
        }

        Assert.IsType<CohereMeta>(_classifyResponse.Meta);
        Assert.NotNull(_classifyResponse.Meta?.ApiVersion);
        Assert.IsType<string>(_classifyResponse.Meta.ApiVersion.Version);
        Assert.NotNull(_classifyResponse.Meta?.BilledUnits);
        Assert.IsType<double>(_classifyResponse.Meta.BilledUnits.Classifications);
    }

    /// <summary>
    /// Sends an invalid classify request with various incorrect configurations to the Cohere API endpoint
    /// </summary>
    [When(@"I send an invalid classify request with ""(.*)""")]
    public async Task WhenISendAnInvalidClassifyRequestWith(string invalidCase)
    {
        try
        {
            if (_cohereStepDefinitions._client != null)
            {
                _classifyResponse = await _cohereStepDefinitions._client.ClassifyAsync(SampleClassifyRequests.GetClassifyRequest(invalidCase));
            }
            else
            {
                throw new InvalidOperationException("Client is not initialized.");
            }
        }
        catch (Exception ex)
        {
            _caughtException = ex;
        }
    }

    /// <summary>
    /// Verifies that an error response is received from the Cohere API
    /// </summary>
    [Then(@"I should receive an error response")]
    public void ThenIShouldReceiveAnErrorResponse()
    {
        Assert.NotNull(_caughtException);
        Assert.IsType<CohereApiException>(_caughtException);
        _output.WriteLine(_caughtException.ToString());
    }
}