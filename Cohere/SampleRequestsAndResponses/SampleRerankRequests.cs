using Cohere.Types.Rerank;

namespace Cohere.SampleRequestsAndResponses;

/// <summary>
/// Examples of valid and invalid rerank requests to the Cohere API
/// </summary>
public class SampleRerankRequests
{
    /// <summary>
    /// A valid rerank request
    /// </summary>
    public static readonly RerankRequest BasicValidRequest = new()
    {
        Model = "rerank-english-v3.0",
        Query = "What is the capital of the United States?",
        Documents = [
        "Carson City is the capital city of the American state of Nevada.",
        "The Commonwealth of the Northern Mariana Islands is a group of islands in the Pacific Ocean. Its capital is Saipan.",
        "Capitalization or capitalisation in English grammar is the use of a capital letter at the start of a word. English usage varies from capitalization in other languages.",
        "Washington, D.C. (also known as simply Washington or D.C., and officially as the District of Columbia) is the capital of the United States. It is a federal district.",
        "Capital punishment (the death penalty) has existed in the United States since before the United States was a country. As of 2017, capital punishment is legal in 30 of the 50 states."
        ],
        TopN = 3
    };

     /// <summary>
    /// A valid rerank request with a large document set
    /// </summary>
    public static readonly RerankRequest LargeDocumentSetRequest = new()
    {
        Query = "Filter for documents with specific content",
        Documents = Enumerable.Range(1, 100).Select(i => (object)$"Document {i}").ToList()
    };

    /// <summary>
    /// A valid rerank request with a high relevance threshold in documents
    /// </summary>
    public static readonly RerankRequest HighRelevanceThresholdRequest = new()
    {
        Query = "Important documents only",
        Documents =
        [
            "Highly relevant document on important topic",
            "Somewhat relevant document"
        ]
    };

    /// <summary>
    /// A valid rerank request with duplicate documents
    /// </summary>
    public static readonly RerankRequest DuplicateDocumentsRequest = new()
    {
        Query = "Identify distinct relevance",
        Documents =
        [
            "Repeated document content",
            "Repeated document content"
        ]
    };

    /// <summary>
    /// A valid rerank request with no relevance
    /// </summary>
    public static readonly RerankRequest NoRelevanceRequest = new()
    {
        Query = "Non-relevant topic",
        Documents =
        [
            "Document on a completely different topic",
            "Another unrelated document"
        ]
    };

    /// <summary>
    /// An invalid rerank request with null values
    /// </summary>
    public static readonly RerankRequest NullValuesRequest = new()
    {
        Query = null!,
        Documents = null!
    };

    /// <summary>
    /// An invalid rerank request with an empty query
    /// </summary>
    public static readonly RerankRequest EmptyQueryRequest = new()
    {
        Query = string.Empty,
        Documents =
        [
            "Some document content",
            "More document content"
        ]
    };

    /// <summary>
    /// An invalid rerank request with no documents provided
    /// </summary>
    public static readonly RerankRequest NoDocumentsProvidedRequest = new()
    {
        Query = "Any relevant information",
        Documents = []
    };

    /// <summary>
    /// Returns a rerank request based on the test case name
    /// </summary>
    /// <param name="testCase"> The name of the test case </param>
    /// <returns> A rerank request </returns>
    public static RerankRequest GetRerankRequest(string testCase) => testCase switch
    {
        "BasicValidRequest" => BasicValidRequest,
        "LargeDocumentSet" => LargeDocumentSetRequest,
        "HighRelevanceThreshold" => HighRelevanceThresholdRequest,
        "DuplicateDocuments" => DuplicateDocumentsRequest,
        "NoRelevance" => NoRelevanceRequest,
        "NullValues" => NullValuesRequest,
        "EmptyQuery" => EmptyQueryRequest,
        "NoDocumentsProvided" => NoDocumentsProvidedRequest,
        _ => throw new ArgumentException($"Invalid test case: {testCase}")
    };
}
