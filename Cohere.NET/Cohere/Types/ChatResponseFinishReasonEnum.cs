namespace Cohere.Types;

public enum ChatResponseFinishReasonEnum
{
    /// <summary>
    /// The possible reasons a chat request has finished
    /// </summary>
    COMPLETE,
    STOP_SEQUENCE,
    MAX_TOKENS,
    TOOL_CALL,
    ERROR
}