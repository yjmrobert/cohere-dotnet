namespace Cohere.Types;

public class RerankResult
{
    /// <summary>
    /// Corresponds to the index in the original list of documents to which the ranked document belongs
    /// </summary>
    public required int Index { get; set; }

    /// <summary>
    /// Relevance scores are normalized to be in the range [0, 1]
    /// Scores close to 1 indicate a high relevance to the query, and scores closer to 0 indicate low relevance
    /// </summary>
    public required double RelevanceScore { get; set; }

    /// <summary>
    /// If return_documents is set as false this will return none, if true it will return the documents passed in the request
    /// </summary>
    public object? Document { get; set; }
}