namespace Cohere.Types.Chat;

/// <summary>
/// Used to select the safety instruction inserted into the prompt
/// </summary>
public enum ChatSafetyModeEnum
{
    /// <summary>
    /// The safety instruction is turned off
    /// </summary>
    OFF = 0,

    /// <summary>
    /// The safety instruction is contextual to the prompt
    /// </summary>
    CONTEXTUAL = 1,

    /// <summary>
    /// The safety instruction is stricy
    /// </summary>
    STRICT = 2
}