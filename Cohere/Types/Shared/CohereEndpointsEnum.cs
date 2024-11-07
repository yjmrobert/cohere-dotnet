namespace Cohere.Types.Shared;

/// <summary>
/// The endpoints currently supported by the Cohere DotNet SDK
/// </summary>
public enum CohereEndpointsEnum
{
    /// <summary>
    /// The endpoint for Cohere chat
    /// </summary>
    Chat = 1,

    /// <summary>
    /// The endpoint for Cohere classification
    /// </summary>
    Classify = 2,

    /// <summary>
    /// The endpoint for Cohere reranking
    /// </summary>
    Rerank = 3
}