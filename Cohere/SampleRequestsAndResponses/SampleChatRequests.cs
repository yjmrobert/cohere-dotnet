using Cohere.Types.Chat;

namespace Cohere.SampleRequestsAndResponses;

/// <summary>
/// Examples of valid and invalid chat requests to the Cohere API
/// </summary>
public class SampleChatRequests
{
     /// <summary>
    /// A basic valid chat request
    /// </summary>
    public static readonly ChatRequest BasicValidRequest = new()
    {
        Messages =
        [
            new ChatMessage
            {
                Role = "user",
                Content = "hello world!"
            }
        ]
    };

    /// <summary>
    /// A valid chat request with max tokens provided
    /// </summary>
    public static readonly ChatRequest MaxTokensRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "Generate a long, detailed story about a futuristic world in 2048." }
        ],
        MaxTokens = 2048
    };

    /// <summary>
    /// A valid chat request with temperature set to 0 (factual response)
    /// </summary>
    public static readonly ChatRequest TemperatureRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "Give me a highly factual answer about the water cycle." }
        ],
        Temperature = 0.0
    };

    /// <summary>
    /// A valid chat request with boundary values K=0 and P=1
    /// </summary>
    public static readonly ChatRequest BoundaryKAndPZeroAndOneRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "Explain the basics of quantum mechanics in simple terms." }
        ],
        K = 0,
        P = 1.0
    };

    /// <summary>
    /// A valid chat request with boundary values K=100 and P=0.1
    /// </summary>
    public static readonly ChatRequest BoundaryKAndPMaxAndMinRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "Describe a suspenseful plot for a detective novel." }
        ],
        K = 100,
        P = 0.1
    };

    /// <summary>
    /// A valid chat request with five stop sequences
    /// </summary>
    public static readonly ChatRequest FiveStopSequencesRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "List five inspirational quotes." }
        ],
        StopSequences = ["inspire", "quote", "wisdom", "life", "motivation"]
    };

    /// <summary>
    /// An invalid chat request with MaxTokens set to -1
    /// </summary>
    public static readonly ChatRequest InvalidMaxTokensRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "This should trigger an error due to invalid token count." }
        ],
        MaxTokens = -1
    };

    /// <summary>
    /// An invalid chat request with Temperature set to -1
    /// </summary>
    public static readonly ChatRequest InvalidTemperatureRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "This should trigger an error due to invalid temperature setting." }
        ],
        Temperature = -1.0
    };

    /// <summary>
    /// An invalid chat request with an unrecognized SafetyMode enum
    /// </summary>
    public static readonly ChatRequest InvalidSafetyModeRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "This should trigger an error due to invalid safety mode setting." }
        ],
        SafetyMode = (ChatSafetyModeEnum)(-1)
    };

    /// <summary>
    /// An invalid chat request with six stop sequences, exceeding the limit
    /// </summary>
    public static readonly ChatRequest ExceedStopSequencesLimitRequest = new()
    {
        Messages =
        [
            new ChatMessage { Role = "user", Content = "Attempt to use more than the allowed number of stop sequences." }
        ],
        StopSequences = ["stop1", "stop2", "stop3", "stop4", "stop5", "stop6"]
    };

    /// <summary>
    /// An invalid chat request with missing required fields
    /// </summary>
    public static readonly ChatRequest MissingRequiredFieldsRequest = new()
    {
        Messages = []
    };

    /// <summary>
    /// Retrieves the corresponding ChatRequest based on the test case name
    /// </summary>
    /// <param name="testCase"> The name of the test case </param>
    /// <returns> A ChatRequest for the specified test case </returns>
    public static ChatRequest GetChatRequest(string testCase) => testCase switch
    {
        "BasicValidRequest" => BasicValidRequest,
        "MaxTokensRequest" => MaxTokensRequest,
        "TemperatureRequest" => TemperatureRequest,
        "BoundaryKAndPZeroAndOne" => BoundaryKAndPZeroAndOneRequest,
        "BoundaryKAndPMaxAndMin" => BoundaryKAndPMaxAndMinRequest,
        "FiveStopSequencesRequest" => FiveStopSequencesRequest,
        "InvalidMaxTokens" => InvalidMaxTokensRequest,
        "InvalidTemperature" => InvalidTemperatureRequest,
        "InvalidSafetyMode" => InvalidSafetyModeRequest,
        "ExceedStopSequencesLimit" => ExceedStopSequencesLimitRequest,
        "MissingRequiredFields" => MissingRequiredFieldsRequest,
        _ => throw new ArgumentException("Invalid test case provided.")
    };
}