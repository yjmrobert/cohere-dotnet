using Cohere.Types.Classify;
using Cohere.Types.Shared;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;
using System.Net;

namespace Cohere.Tests;

/// <summary>
/// Step definitions for the Classify feature
/// </summary>
public partial class CohereStepDefinitions
{
    private ClassifyResponse? _classifyResponse;

    /// <summary>
    /// Sends a valid classify request with various configurations to the mocked Cohere API endpoint
    /// </summary>
    [When(@"I send a valid classify request with ""(.*)""")]
    public async Task WhenISendAValidClassifyRequestWith(string testCase)
    {
        if (_client != null && _httpMessageHandlerFake != null)
        {
            _httpMessageHandlerFake.ResponseContent = SampleClassifyResponses.GetClassifyResponse(testCase);
            _classifyResponse = await _client.ClassifyAsync(SampleClassifyRequests.GetClassifyRequest(testCase));
        }
        else
        {
            throw new InvalidOperationException("Client is not initialized.");
        }
    }

    /// <summary>
    /// Verifies that a valid classify response is received from the mocked Cohere API and checks parsing of response fields
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
    /// Sends an invalid classify request with various incorrect configurations to the mocked Cohere API endpoint
    /// </summary>
    [When(@"I send an invalid classify request with ""(.*)""")]
    public async Task WhenISendAnInvalidClassifyRequestWith(string invalidCase)
    {
        if (_client != null && _httpMessageHandlerFake != null)
        {
            _httpMessageHandlerFake.ResponseContent = SampleClassifyResponses.GetClassifyResponse(invalidCase);
            _httpMessageHandlerFake.StatusCode = HttpStatusCode.BadRequest;

            if (invalidCase == "UnknownTruncate")
            {
                _httpMessageHandlerFake.StatusCode = HttpStatusCode.UnprocessableEntity;
            }
            else if (invalidCase == "HighVolumeRequest")
            {
                _httpMessageHandlerFake.StatusCode = HttpStatusCode.RequestEntityTooLarge;
            }
            
            try
            {
                _classifyResponse = await _client.ClassifyAsync(SampleClassifyRequests.GetClassifyRequest(invalidCase));
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }
        else
        {
            throw new InvalidOperationException("Client is not initialized.");
        }
    }

    /// <summary>
    /// Verifies that an error response is received from the mocked Cohere API
    /// </summary>
    [Then(@"I should receive a classify error response")]
    public void ThenIShouldReceiveAClassifyErrorResponse()
    {
        Assert.NotNull(_caughtException);
        Assert.IsType<CohereApiException>(_caughtException);
        _output?.WriteLine(_caughtException.ToString());
    }
}