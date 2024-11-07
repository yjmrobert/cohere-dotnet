using Cohere.Types.Chat;
using Cohere.Types.Shared;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;

namespace Cohere.IntegrationTests;

/// <summary>
/// Step definitions for the Chat feature
/// </summary>
public partial class CohereStepDefinitions
{
    private ChatResponse? _chatResponse;

    /// <summary>
    /// Sends a valid chat request with various configurations to the Cohere API endpoint
    /// </summary>
    [When(@"I send a valid chat request with ""(.*)""")]
    public async Task WhenISendAValidChatRequestWith(string testCase)
    {
        if (_client != null)
        {
            _chatResponse = await _client.ChatAsync(SampleChatRequests.GetChatRequest(testCase));
        }
        else
        {
            throw new InvalidOperationException("Client is not initialized.");
        }
    }

    /// <summary>
    /// Verifies that a valid chat response is received from the Cohere API and checks parsing of response fields
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
    /// Sends an invalid chat request with various incorrect configurations to the Cohere API endpoint
    /// </summary>
    [When(@"I send an invalid chat request with ""(.*)""")]
    public async Task WhenISendAnInvalidChatRequestWith(string invalidCase)
    {
        try
        {
            if (_client != null)
            {
                _chatResponse = await _client.ChatAsync(SampleChatRequests.GetChatRequest(invalidCase));
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
    [Then(@"I should receive a chat error response")]
    public void ThenIShouldReceiveAChatErrorResponse()
    {
        Assert.NotNull(_caughtException);
        Assert.IsType<CohereApiException>(_caughtException);
        _output?.WriteLine(_caughtException.ToString());
    }
}