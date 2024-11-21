namespace Cohere.Types.Classify;

/// <summary>
/// Option to specify how the API will handle inputs longer than the maximum token length
/// </summary>
public enum ClassifyTruncateEnum
{
    /// <summary>
    /// Truncate the input to the maximum token length
    /// </summary>
    NONE = 0,

    /// <summary>
    /// Discard the start of the input to fit the maximum token length
    /// </summary>
    START = 1,

    /// <summary>
    /// Discard the end of the input to fit the maximum token length
    /// </summary>
    END = 2
}