namespace Cohere.Types;

public enum ClassifyTruncateEnum
{
    /// <summary>
    /// Option to specify how the API will handle inputs longer than the maximum token length
    /// </summary>
    NONE,
    START,
    END
}