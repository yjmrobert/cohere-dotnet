namespace Cohere.Types;

/// <summary>
/// Enum to dictate the approach taken to generating citations
/// </summary>
public enum ChatCitationOptionModeEnum
{
    /// <summary>
    /// The citation generation will be fast
    /// </summary>
    FAST,

    /// <summary>
    /// The citation generation will be accurate
    /// </summary>
    ACCURATE,
    
    /// <summary>
    /// The citation generation will be turned off
    /// </summary>
    OFF
}