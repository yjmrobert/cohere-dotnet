using Cohere.Types;
using Cohere.SampleRequestsAndResponses;
using Reqnroll;
using Xunit;

namespace Cohere.IntegrationTests.Chat;

[Binding, Scope(Feature = "Chat Integration")]

/// <summary>
/// Step definitions for the Chat feature
/// </summary>
public class ChatStepDefinitions
{
    private readonly CohereStepDefinitions _cohereStepDefinitions;
    private ChatResponse? _chatResponse;

    
    public ChatStepDefinitions(CohereStepDefinitions cohereStepDefinitions)
    {
        _cohereStepDefinitions = cohereStepDefinitions;
    }

    /// <summary>
    /// Sends ValidchatRequest1 to the Cohere API endpoint
    /// </summary>
    [When(@"I send a valid chat request")]
    public async Task WhenISendAValidChatRequest()
    {
        _chatResponse = await _cohereStepDefinitions._client.ChatAsync(CohereApiRequests.ValidChatRequest1);
    }

    /// <summary>
    /// Verifies that ValidChatResponse1 is received from the Cohere API and checks parsing of response fields
    /// </summary>
    [Then(@"I should receive a valid chat response")]
    public void ThenIShouldReceiveAValidChatResponse()
    {
        Assert.NotNull(_chatResponse);

        var text = _chatResponse.Message?.Content as List<ChatResponseMessageText>;

        Assert.Equal(typeof(string), _chatResponse.Id.GetType());
        Assert.Equal("assistant", _chatResponse.Message?.Role);
        Assert.Equal(typeof(string), text?[0].Text.GetType());
        Assert.Equal(ChatResponseFinishReasonEnum.COMPLETE, _chatResponse.FinishReason);
        Assert.Equal(typeof(ChatUsage), _chatResponse.Usage?.GetType());
        Assert.Equal(typeof(double), _chatResponse.Usage?.BilledUnits?.InputTokens?.GetType());
        Assert.Equal(typeof(double), _chatResponse.Usage?.Tokens?.OutputTokens?.GetType());
    }
}