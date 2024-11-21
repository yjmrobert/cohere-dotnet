namespace Cohere.Types.Shared;

/// <summary>
/// The default model names for each currently supported endpoint
/// </summary>
public class CohereDefaultModelNames
{
    /// <summary>
    /// The default model name for the chat endpoint
    /// </summary>
    public const string DefaultChatModel = "command-r-plus-08-2024";

    /// <summary>
    /// The default model name for the classify endpoint
    /// </summary>
    public const string DefaultClassifyModel = "embed-english-v2.0";

    /// <summary>
    /// The default model name for the rerank endpoint
    /// </summary>
    public const string DefaultRerankModel = "rerank-english-v3.0";
}