using Cohere.Types.Shared;

namespace Cohere.Types.Chat;

/// <summary>
/// Summary of usage for a chat request and response
/// </summary>
public class ChatUsage
{
    /// <summary>
    /// Summary of billed units
    /// </summary>
    public CohereBillledUnits? BilledUnits { get; set; }

    /// <summary>
    /// Summary of tokens used for both the input and output of the request
    /// </summary>
    public CohereTokens? Tokens { get; set; }
}