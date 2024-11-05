namespace Cohere.SampleRequestsAndResponses;

/// <summary>
/// Examples of valid and invalid classify responses from the Cohere API
/// </summary>
public class SampleClassifyResponses
{
    /// <summary>
    /// A sample valid response from the classify endpoint with a basic request
    /// </summary>
    public static readonly string BasicValidResponse = @"
    {
        ""id"": ""9bc8539d-c063-477b-b6fb-d7822f3fa303"",
        ""classifications"": [
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.8962522,
                ""confidences"": [0.8962522],
                ""id"": ""14702754-d9e1-4e18-8bb2-3e02192c04b8"",
                ""input"": ""Confirm your email address"",
                ""labels"": {
                    ""Not spam"": {""confidence"": 0.8962522},
                    ""Spam"": {""confidence"": 0.1037478}
                },
                ""prediction"": ""Not spam"",
                ""predictions"": [""Not spam""]
            },
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.6810614,
                ""confidences"": [0.6810614],
                ""id"": ""8eb56a3f-f7a5-44f2-bcbc-4c812776d5a5"",
                ""input"": ""hey i need u to send some $"",
                ""labels"": {
                    ""Not spam"": {""confidence"": 0.6810614},
                    ""Spam"": {""confidence"": 0.3189386}
                },
                ""prediction"": ""Not spam"",
                ""predictions"": [""Not spam""]
            }
        ],
        ""meta"": {""api_version"": {""version"": ""2""}, ""billed_units"": {""classifications"": 2}}
    }";

    /// <summary>
    /// A sample response from the classify endpoint with multiple labels for inputs
    /// </summary>
    public static readonly string MultipleLabelsResponse = @"
    {
        ""id"": ""cc061d9e-a084-469b-a703-92d714d340f8"",
        ""classifications"": [
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.99724704,
                ""confidences"": [0.99724704],
                ""id"": ""3e3b4b6b-282d-45d3-9e17-921f42c9d6be"",
                ""input"": ""This is an exclusive offer"",
                ""labels"": {
                    ""Informative"": {""confidence"": 0.001787742},
                    ""Promotional"": {""confidence"": 0.99724704},
                    ""Transactional"": {""confidence"": 0.00096523965}
                },
                ""prediction"": ""Promotional"",
                ""predictions"": [""Promotional""]
            }
        ],
        ""meta"": {""api_version"": {""version"": ""2""}, ""billed_units"": {""classifications"": 3}}
    }";

    /// <summary>
    /// A sample response from the classify endpoint with high confidence inputs
    /// </summary>
    public static readonly string HighConfidenceResponse = @"
    {
        ""id"": ""c9c12bbf-3918-48e3-b5bd-0f15c3bb4f70"",
        ""classifications"": [
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.9990244,
                ""confidences"": [0.9990244],
                ""id"": ""fc0521cf-7a3b-460c-bcbb-2b3f2a2a8b6f"",
                ""input"": ""Special discount just for you!"",
                ""labels"": {
                    ""Not spam"": {""confidence"": 0.0009756337},
                    ""Spam"": {""confidence"": 0.9990244}
                },
                ""prediction"": ""Spam"",
                ""predictions"": [""Spam""]
            }
        ],
        ""meta"": {""api_version"": {""version"": ""2""}, ""billed_units"": {""classifications"": 2}}
    }";

    /// <summary>
    /// A sample response from the classify endpoint with repeated inputs
    /// </summary>
    public static readonly string IdenticalInputsResponse = @"
    {
        ""id"": ""e2c3b50f-3d38-45b1-9d58-563898897afe"",
        ""classifications"": [
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.9991147,
                ""confidences"": [0.9991147],
                ""id"": ""9a654191-a2a8-48d6-a421-75aa18c71d21"",
                ""input"": ""Your order has shipped"",
                ""labels"": {
                    ""Spam"": {""confidence"": 0.0008853096},
                    ""Transactional"": {""confidence"": 0.9991147}
                },
                ""prediction"": ""Transactional"",
                ""predictions"": [""Transactional""]
            },
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.9991147,
                ""confidences"": [0.9991147],
                ""id"": ""54060ccb-476b-42e6-93d1-f4453749c539"",
                ""input"": ""Your order has shipped"",
                ""labels"": {
                    ""Spam"": {""confidence"": 0.0008853096},
                    ""Transactional"": {""confidence"": 0.9991147}
                },
                ""prediction"": ""Transactional"",
                ""predictions"": [""Transactional""]
            }
        ],
        ""meta"": {""api_version"": {""version"": ""2""}, ""billed_units"": {""classifications"": 2}}
    }";

    /// <summary>
    /// A sample response from the classify endpoint with mixed labels
    /// </summary>
    public static readonly string MixedLabelsResponse = @"
    {
        ""id"": ""c327e117-bb1c-48c9-b402-af1add5d3bd1"",
        ""classifications"": [
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.981845,
                ""confidences"": [0.981845],
                ""id"": ""ab112512-5f1d-4d08-b467-a93127f642ae"",
                ""input"": ""Great offer for you"",
                ""labels"": {
                    ""Spam"": {""confidence"": 0.981845},
                    ""Transactional"": {""confidence"": 0.018154975}
                },
                ""prediction"": ""Spam"",
                ""predictions"": [""Spam""]
            },
            {
                ""classification_type"": ""single-label"",
                ""confidence"": 0.96128803,
                ""confidences"": [0.96128803],
                ""id"": ""3077f253-e3f0-42c2-aa69-c46d1527efb0"",
                ""input"": ""Invoice ready"",
                ""labels"": {
                    ""Spam"": {""confidence"": 0.038711943},
                    ""Transactional"": {""confidence"": 0.96128803}
                },
                ""prediction"": ""Transactional"",
                ""predictions"": [""Transactional""]
            }
        ],
        ""meta"": {""api_version"": {""version"": ""2""}, ""billed_units"": {""classifications"": 2}}
    }";

    /// <summary>
    /// A sample response from the classify endpoint with empty inputs
    /// </summary>
    public static readonly string NullValuesResponse = @"
    {
        ""message"": ""invalid request: inputs cannot be empty""
    }";

    /// <summary>
    /// A sample response from the classify endpoint with an invalid truncate value
    /// </summary>
    public static readonly string UnknownTruncateResponse = @"
    {
        ""message"": ""invalid request: `truncate` must be one of \""NONE\"", \""START\""/\""LEFT\"" or \""END\""/\""RIGHT\""""
    }";

    /// <summary>
    /// A sample response from the classify endpoint with less than two examples per label
    /// </summary>
    public static readonly string LessThanTwoExamplesPerLabelResponse = @"
    {
        ""message"": ""invalid request: each unique label must have at least 2 examples. Not enough examples for: Informative, Promotional""
    }";

    /// <summary>
    /// A sample response from the classify endpoint with a single label
    /// </summary>
    public static readonly string SingleLabelOnlyResponse = @"
    {
        ""message"": ""invalid request: min classes for classify request is 2 - received 1""
    }";

    /// <summary>
    /// A sample response from the classify endpoint with empty examples
    /// </summary>
    public static readonly string EmptyExamplesResponse = @"
    {
        ""message"": ""invalid request: min classes for classify request is 2 - received 0""
    }";

    /// <summary>
    /// A sample response from the classify endpoint with high volume inputs
    /// </summary>
    public static readonly string HighVolumeResponse = @"
    {
        ""message"": ""invalid request: inputs cannot contain more than 96 elements, received 1000""
    }";

    /// <summary>
    /// Returns a response based on the test case name
    /// </summary>
    /// <param name="testCase"> The name of the test case </param>
    /// <returns> A classify response </returns>
    public static string GetClassifyResponse(string testCase) => testCase switch
    {
        "BasicValidRequest" => BasicValidResponse,
        "MultipleLabels" => MultipleLabelsResponse,
        "HighConfidence" => HighConfidenceResponse,
        "IdenticalInputs" => IdenticalInputsResponse,
        "MixedLabels" => MixedLabelsResponse,
        "NullValues" => NullValuesResponse,
        "UnknownTruncate" => UnknownTruncateResponse,
        "LessThanTwoExamplesPerLabel" => LessThanTwoExamplesPerLabelResponse,
        "SingleLabelOnly" => SingleLabelOnlyResponse,
        "EmptyExamples" => EmptyExamplesResponse,
        "HighVolume" => HighVolumeResponse,
        _ => throw new ArgumentException($"Invalid test case: {testCase}")
    };
}
