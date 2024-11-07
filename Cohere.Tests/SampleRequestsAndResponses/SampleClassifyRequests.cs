using Cohere.Types.Classify;

namespace Cohere.Tests.SampleRequestsAndResponses;

/// <summary>
/// Examples of valid and invalid classify requests to the Cohere API
/// </summary>
public static class SampleClassifyRequests
{
    // Valid Requests

    /// <summary>
    /// A valid basic classify request
    /// </summary>
    public static readonly ClassifyRequest BasicValidRequest = new()
    {
       Examples =
        [
            new ClassifyExample { Text = "Dermatologists don't like her!", Label = "Spam" },
            new ClassifyExample { Text = "'Hello, open to this?'", Label = "Spam" },
            new ClassifyExample { Text = "I need help please wire me $1000 right now", Label = "Spam" },
            new ClassifyExample { Text = "Nice to know you ;)", Label = "Spam" },
            new ClassifyExample { Text = "Please help me?", Label = "Spam" },
            new ClassifyExample { Text = "Your parcel will be delivered today", Label = "Not spam" },
            new ClassifyExample { Text = "Review changes to our Terms and Conditions", Label = "Not spam" },
            new ClassifyExample { Text = "Weekly sync notes", Label = "Not spam" },
            new ClassifyExample { Text = "'Re: Follow up from today's meeting'", Label = "Not spam" },
            new ClassifyExample { Text = "Pre-read for tomorrow", Label = "Not spam" }
        ],
        Inputs =
        [
            "Confirm your email address",
            "hey i need u to send some $"
        ]
    };

    /// <summary>
    /// A valid classify request with multiple labels for inputs
    /// </summary>
    public static readonly ClassifyRequest MultipleLabelsRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "Exclusive offer just for you!", Label = "Promotional" },
            new ClassifyExample { Text = "Don't miss out on these savings", Label = "Promotional" },
            new ClassifyExample { Text = "Meeting tomorrow at 10 AM", Label = "Informative" },
            new ClassifyExample { Text = "Notes from today's sync", Label = "Informative" },
            new ClassifyExample { Text = "Your invoice is ready", Label = "Transactional" },
            new ClassifyExample { Text = "Monthly billing update", Label = "Transactional" }
        ],
        Inputs = ["This is an exclusive offer", "Let's meet tomorrow", "Invoice for last month"]
    };

    /// <summary>
    /// A valid classify request with high confidence inputs
    /// </summary>
    public static readonly ClassifyRequest HighConfidenceRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "Special discount just for you!", Label = "Spam" },
            new ClassifyExample { Text = "Limited time offer, act now!", Label = "Spam" },
            new ClassifyExample { Text = "Hi, how are you?", Label = "Not spam" },
            new ClassifyExample { Text = "Just checking in with you", Label = "Not spam" }
        ],
        Inputs = ["Special discount just for you!", "Hi, hope you're well!"]
    };

    /// <summary>
    /// A valid classify request with identical inputs
    /// </summary>
    public static readonly ClassifyRequest IdenticalInputsRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "Your order has shipped", Label = "Transactional" },
            new ClassifyExample { Text = "Package is on its way", Label = "Transactional" },
            new ClassifyExample { Text = "Free trial just for you", Label = "Spam" },
            new ClassifyExample { Text = "Limited offer, don't miss out!", Label = "Spam" }
        ],
        Inputs = ["Your order has shipped", "Your order has shipped"]
    };

    /// <summary>
    /// A valid classify request with mixed label examples
    /// </summary>
    public static readonly ClassifyRequest MixedLabelsRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "Don't miss this offer", Label = "Spam" },
            new ClassifyExample { Text = "Grab this limited deal now", Label = "Spam" },
            new ClassifyExample { Text = "Invoice for the last quarter", Label = "Transactional" },
            new ClassifyExample { Text = "Monthly payment due soon", Label = "Transactional" }
        ],
        Inputs = ["Great offer for you", "Invoice ready"]
    };

    // Invalid Requests

    /// <summary>
    /// An invalid classify request with null values in inputs
    /// </summary>
    public static readonly ClassifyRequest NullValuesRequest = new()
    {
        Examples = null!,
        Inputs = null!
    };

    /// <summary>
    /// An invalid classify request with an unknown truncate setting
    /// </summary>
    public static readonly ClassifyRequest UnknownTruncateRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "Free trial for 30 days", Label = "Spam" },
            new ClassifyExample { Text = "Special limited offer", Label = "Spam" }
        ],
        Inputs = ["Try out our free trial today!"],
        Truncate = (ClassifyTruncateEnum)(-1)
    };

    /// <summary>
    /// An invalid classify request with less than two examples per label
    /// </summary>
    public static readonly ClassifyRequest LessThanTwoExamplesPerLabelRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "Exclusive offer!", Label = "Promotional" },
            new ClassifyExample { Text = "Meeting tomorrow at 10 AM", Label = "Informative" }
        ],
        Inputs = ["Special promotion", "Upcoming meeting"]
    };

    /// <summary>
    /// An invalid classify request with only one label
    /// </summary>
    public static readonly ClassifyRequest SingleLabelOnlyRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "Limited time offer, act now!", Label = "Promotional" },
            new ClassifyExample { Text = "Get the best discounts today", Label = "Promotional" }
        ],
        Inputs = ["Special offer just for you"]
    };

    /// <summary>
    /// An invalid classify request with empty examples
    /// </summary>
    public static readonly ClassifyRequest EmptyExamplesRequest = new()
    {
        Examples = [],
        Inputs = ["This should trigger an error due to no examples provided"]
    };

    /// <summary>
    /// An invalid classify request that contains a very high volume of inputs
    /// </summary>
    public static readonly ClassifyRequest HighVolumeRequest = new()
    {
        Examples = [
            new ClassifyExample { Text = "System update notification", Label = "System" },
            new ClassifyExample { Text = "Scheduled maintenance alert", Label = "System" }
        ],
        Inputs = new List<string>(new string[1000].Select(_ => "High volume input"))
    };

    /// <summary>
    /// Returns a classify request based on the test case name
    /// </summary>
    /// <param name="testCase"> The name of the test case </param>
    /// <returns> A classify request </returns>
    public static ClassifyRequest GetClassifyRequest(string testCase) => testCase switch
    {
        "BasicValidRequest" => BasicValidRequest,
        "MultipleLabels" => MultipleLabelsRequest,
        "HighConfidence" => HighConfidenceRequest,
        "IdenticalInputs" => IdenticalInputsRequest,
        "MixedLabels" => MixedLabelsRequest,
        "NullValues" => NullValuesRequest,
        "UnknownTruncate" => UnknownTruncateRequest,
        "LessThanTwoExamplesPerLabel" => LessThanTwoExamplesPerLabelRequest,
        "SingleLabelOnly" => SingleLabelOnlyRequest,
        "EmptyExamples" => EmptyExamplesRequest,
        "HighVolume" => HighVolumeRequest,
        _ => throw new ArgumentException($"Invalid test case: {testCase}")
    };
}
