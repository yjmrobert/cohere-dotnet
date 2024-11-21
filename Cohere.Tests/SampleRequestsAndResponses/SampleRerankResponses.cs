namespace Cohere.Tests.SampleRequestsAndResponses;

/// <summary>
/// Examples of valid and invalid rerank responses from the Cohere API
/// </summary>
public static class SampleRerankResponses
{

    /// <summary>
    /// A basic valid response from the rerank endpoint
    /// </summary>
    public static readonly string BasicValidResponse = @"
    {
        ""id"": ""6b74c18c-31ea-4949-9ebf-9b83259b4d9a"",
        ""results"": [
            { ""index"": 3, ""relevance_score"": 0.999071 },
            { ""index"": 4, ""relevance_score"": 0.7779754 },
            { ""index"": 0, ""relevance_score"": 0.32713068 }
        ],
        ""meta"": {
            ""api_version"": { ""version"": ""1"" },
            ""billed_units"": { ""search_units"": 1 }
        }
    }";

    /// <summary>
    /// A response from the rerank endpoint with a large document set
    /// </summary>
    public static readonly string LargeDocumentSetResponse = @"
    {
        ""id"": ""561757ee-0bab-478d-ac68-a82cdd5a4a87"",
        ""results"": [
            { ""index"": 0, ""relevance_score"": 9.535461e-05 }
        ],
        ""meta"": {
            ""api_version"": { ""version"": ""1"" },
            ""billed_units"": { ""search_units"": 1 }
        }
    }";

    /// <summary>
    /// A response from the rerank endpoint with a high relevance threshold
    /// </summary>
    public static readonly string HighRelevanceThresholdResponse = @"
    {
        ""id"": ""47538894-0a82-4f0d-b9cc-986240ca89e0"",
        ""results"": [
            { ""index"": 0, ""relevance_score"": 0.21750368 },
            { ""index"": 1, ""relevance_score"": 0.0008761799 }
        ],
        ""meta"": {
            ""api_version"": { ""version"": ""1"" },
            ""billed_units"": { ""search_units"": 1 }
        }
    }";

    /// <summary>
    /// A response from the rerank endpoint with duplicate documents
    /// </summary>
    public static readonly string DuplicateDocumentsResponse = @"
    {
        ""id"": ""e0e32b56-035c-4783-8d10-e9261b818edd"",
        ""results"": [
            { ""index"": 0, ""relevance_score"": 7.87275e-07 },
            { ""index"": 1, ""relevance_score"": 7.87275e-07 }
        ],
        ""meta"": {
            ""api_version"": { ""version"": ""1"" },
            ""billed_units"": { ""search_units"": 1 }
        }
    }";

    /// <summary>
    /// A response from the rerank endpoint with no relevance
    /// </summary>
    public static readonly string NoRelevanceResponse = @"
    {
        ""id"": ""67780d07-4e8b-4958-8026-0551f49190d7"",
        ""results"": [
            { ""index"": 1, ""relevance_score"": 0.00053578056 },
            { ""index"": 0, ""relevance_score"": 0.00023968685 }
        ],
        ""meta"": {
            ""api_version"": { ""version"": ""1"" },
            ""billed_units"": { ""search_units"": 1 }
        }
    }";

    /// <summary>
    /// A response from the rerank endpoint with null values
    /// </summary>
    public static readonly string NullValuesResponse = @"
    {
        ""message"": ""invalid request: list of documents must not be empty""
    }";

    /// <summary>
    /// A response from the rerank endpoint with an empty query
    /// </summary>
    public static readonly string EmptyQueryResponse = @"
    {
        ""message"": ""invalid request: query must not be empty or be only whitespace""
    }";

    /// <summary>
    /// A response from the rerank endpoint with no documents provided
    /// </summary>
    public static readonly string NoDocumentsProvidedResponse = @"
    {
        ""message"": ""invalid request: list of documents must not be empty""
    }";

    /// <summary>
    /// Retrieves a rerank response based on the provided test case
    /// </summary>
    /// <param name="testCase">The name of the test case</param>
    /// <returns>The rerank response as a string</returns>
    public static string GetRerankResponse(string testCase) => testCase switch
    {
        "BasicValidRequest" => BasicValidResponse,
        "LargeDocumentSet" => LargeDocumentSetResponse,
        "HighRelevanceThreshold" => HighRelevanceThresholdResponse,
        "DuplicateDocuments" => DuplicateDocumentsResponse,
        "NoRelevance" => NoRelevanceResponse,
        "NullValues" => NullValuesResponse,
        "EmptyQuery" => EmptyQueryResponse,
        "NoDocumentsProvided" => NoDocumentsProvidedResponse,
        _ => throw new ArgumentException($"Invalid test case: {testCase}")
    };
}
