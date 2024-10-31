namespace Cohere.Types;

/// <summary>
/// Used to select the safety instruction inserted into the prompt
/// </summary>
public enum ChatSafetyModeEnum
{
    /// <summary>
    /// The safety instruction is contextual to the prompt
    /// </summary>
    CONTEXTUAL,

    /// <summary>
    /// The safety instruction is stricy
    /// </summary>
    STRICT,

    /// <summary>
    /// The safety instruction is turned off
    /// </summary>
    OFF
}