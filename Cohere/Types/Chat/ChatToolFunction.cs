namespace Cohere.Types.Chat;

/// <summary>
/// A function to be executed by the chat tool
/// </summary>
public class ChatToolFunction
{
    /// <summary>
    /// The name of the function.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// A description of the function.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A map of parameters for the function, represented as a JSON schema.
    /// </summary>
    public Dictionary<string, object>? Parameters { get; set; }
}