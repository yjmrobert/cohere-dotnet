namespace Cohere.Types;

public class CohereTokens
{
    /// <summary>
    /// The number of tokens used as input to the model
    /// </summary>
    public double? InputTokens { get; set; }

    /// <summary>
    /// The number of tokens produced by the model
    /// </summary>
    public double? OutputTokens { get; set; }
}