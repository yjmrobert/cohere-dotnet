namespace Cohere.Types.Chat;

/// <summary>
/// Enum to dictate the approach taken to generating citations
/// </summary>
public enum ChatCitationOptionModeEnum
{
    /// <summary>
    /// The citation generation will be turned off
    /// </summary>
    OFF = 0,

    /// <summary>
    /// The citation generation will be fast
    /// </summary>
    FAST = 1,

    /// <summary>
    /// The citation generation will be accurate
    /// </summary>
    ACCURATE = 2
}