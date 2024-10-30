namespace Cohere.Types;

/// <summary>
/// The possible reasons a chat request has finished
/// </summary>
public enum ChatResponseFinishReasonEnum
{
    /// <summary>
    /// The chat request has completed
    /// </summary>
    COMPLETE,

    /// <summary>
    /// The chat request has been stopped by the user
    /// </summary>
    STOP_SEQUENCE,

    /// <summary>
    /// The chat request has been stopped by the model due to reaching the maximum number of tokens
    /// </summary>
    MAX_TOKENS,

    /// <summary>
    /// The chat request has been stopped by the model due to a tool call
    /// </summary>

    TOOL_CALL,
    /// <summary>
    /// The chat request has been stopped by the model due to an error
    /// </summary>
    ERROR
}