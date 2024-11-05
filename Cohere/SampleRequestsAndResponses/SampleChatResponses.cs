namespace Cohere.SampleRequestsAndResponses;

/// <summary>
/// Examples of valid chat responses from the Cohere API
/// </summary>

public class SampleChatResponses
{
    /// <summary>
    /// A sample valid response from the chat endpoint with a basic request
    /// </summary>
    public static readonly string BasicValidResponse = @"
        {
        ""id"": ""c14c80c3-18eb-4519-9460-6c92edd8cfb4"",
        ""finish_reason"": ""COMPLETE"",
        ""message"": {
            ""role"": ""assistant"",
            ""content"": [
            {
                ""type"": ""text"",
                ""text"": ""Hello! How can I help you today?""
            }
            ]
        },
        ""usage"": {
            ""billed_units"": {
            ""input_tokens"": 5,
            ""output_tokens"": 418
            },
            ""tokens"": {
            ""input_tokens"": 71,
            ""output_tokens"": 418
            }
        }
        }";

    /// <summary>
    /// A sample response from the chat endpoint for the max tokens sample request
    /// </summary>
    public static readonly string MaxTokensResponse = @"
    {
        ""id"": ""8653ab81-7b61-4b60-bb23-2fa60f36e8ca"",
        ""finish_reason"": ""COMPLETE"",
        ""message"": {
            ""role"": ""assistant"",
            ""content"": [
            {
                ""type"": ""text"",
                ""text"": ""Title: Neo-Nova: A New Dawn in the Digital Age ...""
            }
            ]
        },
        ""usage"": {
            ""billed_units"": {
                ""input_tokens"": 17,
                ""output_tokens"": 1388
            },
            ""tokens"": {
                ""input_tokens"": 223,
                ""output_tokens"": 1388
            }
        }
    }";

    /// <summary>
    /// A sample response from the chat endpoint for the temperature sample request
    /// </summary>
    public static readonly string TemperatureResponse = @"
    {
        ""id"": ""d47d85f0-c032-472b-88f8-f18c74334f9c"",
        ""finish_reason"": ""COMPLETE"",
        ""message"": {
            ""role"": ""assistant"",
            ""content"": [
            {
                ""type"": ""text"",
                ""text"": ""The water cycle, also known as the hydrological cycle, is a fundamental process ...""
            }
            ]
        },
        ""usage"": {
            ""billed_units"": {
                ""input_tokens"": 11,
                ""output_tokens"": 882
            },
            ""tokens"": {
                ""input_tokens"": 217,
                ""output_tokens"": 882
            }
        }
    }";

    /// <summary>
    /// A sample response from the chat endpoint for the boundary K=0, P=1 request
    /// </summary>
    public static readonly string BoundaryKAndPZeroAndOneResponse = @"
    {
        ""id"": ""7499f125-4579-4b13-a523-a330b428b04c"",
        ""finish_reason"": ""COMPLETE"",
        ""message"": {
            ""role"": ""assistant"",
            ""content"": [
            {
                ""type"": ""text"",
                ""text"": ""Quantum mechanics is a branch of physics that deals with the behavior of matter and energy ...""
            }
            ]
        },
        ""usage"": {
            ""billed_units"": {
                ""input_tokens"": 10,
                ""output_tokens"": 663
            },
            ""tokens"": {
                ""input_tokens"": 216,
                ""output_tokens"": 663
            }
        }
    }";

    /// <summary>
    /// A sample response from the chat endpoint for the boundary K=100, P=0.1 request
    /// </summary>
    public static readonly string BoundaryKAndPMaxAndMinResponse = @"
    {
        ""id"": ""4a86c4ba-a9f1-494b-b296-ee90bfb42d81"",
        ""finish_reason"": ""COMPLETE"",
        ""message"": {
            ""role"": ""assistant"",
            ""content"": [
            {
                ""type"": ""text"",
                ""text"": ""Title: Shadows of Deception ...""
            }
            ]
        },
        ""usage"": {
            ""billed_units"": {
                ""input_tokens"": 10,
                ""output_tokens"": 536
            },
            ""tokens"": {
                ""input_tokens"": 216,
                ""output_tokens"": 536
            }
        }
    }";

    /// <summary>
    /// A sample response from the chat endpoint for the five stop sequences request
    /// </summary>
    public static readonly string FiveStopSequencesResponse = @"
    {
        ""id"": ""298af2f4-3191-4bde-95c4-3ff75c278897"",
        ""finish_reason"": ""STOP_SEQUENCE"",
        ""message"": {
            ""role"": ""assistant"",
            ""content"": [
            {
                ""type"": ""text"",
                ""text"": ""Here are five inspirational quotes ...""
            }
            ]
        },
        ""usage"": {
            ""billed_units"": {
                ""input_tokens"": 5,
                ""output_tokens"": 6
            },
            ""tokens"": {
                ""input_tokens"": 211,
                ""output_tokens"": 252
            }
        }
    }";

    /// <summary>
    /// An error response from the chat endpoint for the invalid MaxTokens request
    /// </summary>
    public static readonly string InvalidMaxTokensResponse = @"
    {
        ""message"": ""invalid request: max_tokens cannot be less than 0.""
    }";

    /// <summary>
    /// An error response from the chat endpoint for the invalid temperature request
    /// </summary>
    public static readonly string InvalidTemperatureResponse = @"
    {
        ""message"": ""invalid request: temperature must be between 0 and 1.0 inclusive.""
    }";

    /// <summary>
    /// An error response from the chat endpoint for the invalid safety mode request
    /// </summary>
    public static readonly string InvalidSafetyModeResponse = @"
    {
        ""message"": ""unrecognized safety mode 'INVALID_MODE'. For proper usage, please refer to https://docs.cohere.com/reference/chat""
    }";

    /// <summary>
    /// An error response from the chat endpoint for the exceeding stop sequences request
    /// </summary>
    public static readonly string ExceedStopSequencesLimitResponse = @"
    {
        ""message"": ""invalid request: too many stop sequences provided, maximum is 5, received 6.""
    }";

    /// <summary>
    /// An error response from the chat endpoint for the invalid missing fields request
    /// </summary>
    public static readonly string MissingRequiredFieldsResponse = @"
    {
        ""message"": ""invalid request: message must be at least 1 token long or tool results must be specified.""
    }";

    /// <summary>
    /// Retrieves the expected response content based on the test case name
    /// </summary>
    /// <param name="testCase"> The name of the test case </param>
    /// <returns> A string containing the expected response JSON for the specified test case </returns>
    public static string GetValidResponse(string testCase) => testCase switch
    {
        "BasicValidRequest" => BasicValidResponse,
        "MaxTokensRequest" => MaxTokensResponse,
        "TemperatureRequest" => TemperatureResponse,
        "BoundaryKAndPZeroAndOne" => BoundaryKAndPZeroAndOneResponse,
        "BoundaryKAndPMaxAndMin" => BoundaryKAndPZeroAndOneResponse,
        "FiveStopSequencesRequest" => FiveStopSequencesResponse,
        _ => throw new ArgumentException("Invalid test case provided.")
    };

    /// <summary>
    /// Retrieves the expected invalid response content based on the test case name
    /// </summary>
    /// <param name="testCase"> The name of the test case </param>
    /// <returns> A string containing the expected response JSON for the specified test case </returns>
    public static string GetInvalidResponse(string testCase) => testCase switch
    {
        "InvalidMaxTokens" => InvalidMaxTokensResponse,
        "InvalidTemperature" => InvalidTemperatureResponse,
        "InvalidSafetyMode" => InvalidSafetyModeResponse,
        "ExceedStopSequencesLimit" => ExceedStopSequencesLimitResponse,
        "MissingRequiredFields" => MissingRequiredFieldsResponse,
        _ => throw new ArgumentException("Invalid test case provided.")
    };
}