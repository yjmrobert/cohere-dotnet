using Cohere.Types.Chat;
using Cohere.Types.Shared;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;
using System.Net;

namespace Cohere.Tests;

/// <summary>
/// Step definitions for the Chat feature
/// </summary>
public partial class CohereStepDefinitions
{
    private ChatResponse? _chatResponse;

    /// <summary>
    /// Sends a valid chat request with various configurations to the mocked Cohere API endpoint
    /// </summary>
    [When(@"I send a valid chat request with ""(.*)""")]
    public async Task WhenISendAValidChatRequestWith(string testCase)
    {
        if (_client != null && _httpMessageHandlerFake != null)
        {
            _httpMessageHandlerFake.ResponseContent = SampleChatResponses.GetChatResponse(testCase);
            _chatResponse = await _client.ChatAsync(SampleChatRequests.GetChatRequest(testCase));
        }
        else
        {
            throw new InvalidOperationException("Client is not initialized.");
        }
    }

    /// <summary>
    /// Verifies that a valid chat response is received from the mocked Cohere API and checks parsing of response fields
    /// </summary>
    [Then(@"I should receive a valid chat response")]
    public void ThenIShouldReceiveAValidChatResponse()
    {
        Assert.NotNull(_chatResponse);

        var text = _chatResponse.Message?.Content as List<ChatResponseMessageText>;

        Assert.IsType<string>(_chatResponse.Id);
        Assert.Equal("assistant", _chatResponse.Message?.Role);
        Assert.IsType<string>(text?[0].Text);
        Assert.True(
            _chatResponse.FinishReason == ChatResponseFinishReasonEnum.COMPLETE ||
            _chatResponse.FinishReason == ChatResponseFinishReasonEnum.STOP_SEQUENCE,
            "FinishReason should be either COMPLETE or STOP_SEQUENCE"
        );
        Assert.IsType<ChatUsage>(_chatResponse.Usage);
        Assert.NotNull(_chatResponse.Usage?.BilledUnits?.InputTokens);
        Assert.NotNull(_chatResponse.Usage?.BilledUnits?.OutputTokens);
        Assert.NotNull(_chatResponse.Usage?.Tokens?.InputTokens);
        Assert.NotNull(_chatResponse.Usage?.Tokens?.OutputTokens);
    }

    /// <summary>
    /// Sends an invalid chat request with various incorrect configurations to the mocked Cohere API endpoint
    /// </summary>
    [When(@"I send an invalid chat request with ""(.*)""")]
    public async Task WhenISendAnInvalidChatRequestWith(string invalidCase)
    {
        if (_client != null && _httpMessageHandlerFake != null)
        {
            _httpMessageHandlerFake.ResponseContent = SampleChatResponses.GetChatResponse(invalidCase);
            _httpMessageHandlerFake.StatusCode = HttpStatusCode.BadRequest;

            if (invalidCase == "InvalidSafetyMode")
            {
                _httpMessageHandlerFake.StatusCode = HttpStatusCode.UnprocessableEntity;
            }
            
            try
            {
                _chatResponse = await _client.ChatAsync(SampleChatRequests.GetChatRequest(invalidCase));
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
    [Then(@"I should receive a chat error response")]
    public void ThenIShouldReceiveAnErrorResponse()
    {
        Assert.NotNull(_caughtException);
        Assert.IsType<CohereApiException>(_caughtException);
        _output?.WriteLine(_caughtException.ToString());
    }
}
