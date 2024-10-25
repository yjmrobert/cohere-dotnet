namespace Cohere.Types;

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