using Cohere.Types.Rerank;
using Cohere.Types.Shared;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;
using Xunit.Abstractions;
using System.Net;

namespace Cohere.Tests.Rerank;

[Binding, Scope(Feature = "Rerank")]

/// <summary>
/// Step definitions for the Rerank feature
/// </summary>
public class RerankStepDefinitions
{
    private readonly CohereStepDefinitions _cohereStepDefinitions;
    private RerankResponse? _rerankResponse;
    private Exception? _caughtException;
    private readonly ITestOutputHelper _output;

    public RerankStepDefinitions(CohereStepDefinitions cohereStepDefinitions, ITestOutputHelper output)
    {
        _cohereStepDefinitions = cohereStepDefinitions;
        _output = output;
    }

    /// <summary>
    /// Sends a valid rerank request with various configurations to the mocked Cohere API endpoint
    /// </summary>
    [When(@"I send a valid rerank request with ""(.*)""")]
    public async Task WhenISendAValidRerankRequestWith(string testCase)
    {
        if (_cohereStepDefinitions._client != null && _cohereStepDefinitions._httpMessageHandlerFake != null)
        {
            _cohereStepDefinitions._httpMessageHandlerFake.ResponseContent = SampleRerankResponses.GetRerankResponse(testCase);
            _rerankResponse = await _cohereStepDefinitions._client.RerankAsync(SampleRerankRequests.GetRerankRequest(testCase));
        }
        else
        {
            throw new InvalidOperationException("Client is not initialized.");
        }
    }

    /// <summary>
    /// Sends an invalid rerank request with various incorrect configurations to the mocked Cohere API endpoint
    /// </summary>
    [When(@"I send an invalid rerank request with ""(.*)""")]
    public async Task WhenISendAnInvalidRerankRequestWith(string invalidCase)
    {
        if (_cohereStepDefinitions._client != null && _cohereStepDefinitions._httpMessageHandlerFake != null)
        {
            _cohereStepDefinitions._httpMessageHandlerFake.ResponseContent = SampleRerankResponses.GetRerankResponse(invalidCase);
            _cohereStepDefinitions._httpMessageHandlerFake.StatusCode = HttpStatusCode.BadRequest;
            
            try
            {
                _rerankResponse = await _cohereStepDefinitions._client.RerankAsync(SampleRerankRequests.GetRerankRequest(invalidCase));
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
    /// Verifies that a valid rerank response is received from the mocked Cohere API and checks response types
    /// </summary>
    [Then(@"I should receive a valid rerank response")]
    public void ThenIShouldReceiveAValidRerankResponse()
    {
        Assert.NotNull(_rerankResponse);
        Assert.IsType<string>(_rerankResponse.Id);
        Assert.NotNull(_rerankResponse.Results);
        Assert.All(_rerankResponse.Results, result =>
        {
            Assert.IsType<int>(result.Index);
            Assert.IsType<double>(result.RelevanceScore);
        });
    }

    /// <summary>
    /// Verifies that an error response is received for an invalid rerank request
    /// </summary>
    [Then(@"I should receive an error response")]
    public void ThenIShouldReceiveAnErrorResponse()
    {
        Assert.NotNull(_caughtException);
        Assert.IsType<CohereApiException>(_caughtException);
        _output.WriteLine(_caughtException.ToString());
    }
}