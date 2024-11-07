namespace Cohere.Types.Chat;

/// <summary>
/// The possible reasons a chat request has finished
/// </summary>
public enum ChatResponseFinishReasonEnum
{
    /// <summary>
    /// The chat request has been stopped by the model due to an error
    /// </summary>
    ERROR = 0,

    /// <summary>
    /// The chat request has completed
    /// </summary>
    COMPLETE = 1,

    /// <summary>
    /// The chat request has been stopped by the user
    /// </summary>
    STOP_SEQUENCE = 2,

    /// <summary>
    /// The chat request has been stopped by the model due to reaching the maximum number of tokens
    /// </summary>
    MAX_TOKENS = 3,

    /// <summary>
    /// The chat request has been stopped by the model due to a tool call
    /// </summary>
    TOOL_CALL = 4
}