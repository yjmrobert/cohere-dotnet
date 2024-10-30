using Cohere.Types;
using Reqnroll;
using Xunit;

namespace Cohere.Tests.Chat;

[Binding, Scope(Feature = "Chat")]

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
    /// Sends ValidchatRequest1 to the mocked Cohere API endpoint and sets ValidChatResponse1 to be returned
    /// </summary>
    [When(@"I send a valid chat request")]
    public async Task WhenISendAValidChatRequest()
    {
        _cohereStepDefinitions._httpMessageHandlerFake.ResponseContent = CohereApiResponses.ValidChatResponse1;
        _chatResponse = await _cohereStepDefinitions._client.ChatAsync(CohereApiRequests.ValidChatRequest1);
    }

    /// <summary>
    /// Verifies that ValidChatResponse1 is received from the mocked Cohere API and checks parsing of response fields
    /// </summary>
    [Then(@"I should receive a valid chat response")]
    public void ThenIShouldReceiveAValidChatResponse()
    {
        Assert.NotNull(_chatResponse);

        var text = _chatResponse.Message?.Content as List<ChatResponseMessageText>;

        Assert.Equal(typeof(string), _chatResponse.Id.GetType());
        Assert.Equal("assistant", _chatResponse.Message?.Role);
        Assert.Equal("Hello from Cohere!", text?[0].Text);
        Assert.Equal(ChatResponseFinishReasonEnum.COMPLETE, _chatResponse.FinishReason);
        Assert.Equal(typeof(ChatUsage), _chatResponse.Usage?.GetType());
        Assert.Equal(5, _chatResponse.Usage?.BilledUnits?.InputTokens);
        Assert.Equal(15, _chatResponse.Usage?.BilledUnits?.OutputTokens);
        Assert.Equal(71, _chatResponse.Usage?.Tokens?.InputTokens);
        Assert.Equal(15, _chatResponse.Usage?.Tokens?.OutputTokens);
    }
}