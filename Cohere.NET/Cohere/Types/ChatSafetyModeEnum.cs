namespace Cohere.Types;

public enum ChatSafetyModeEnum
{
    /// <summary>
    /// Used to select the safety instruction inserted into the prompt
    /// </summary>
    CONTEXTUAL,
    STRICT,
    OFF
}