namespace Cohere.Types;

/// <summary>
/// A text message in the content of a chat response
/// </summary>
public class ChatResponseMessageText
{
    /// <summary>
    /// The type of message
    /// </summary>
    public required string Type { get; set; } = "text";

    /// <summary>
    /// The text of the message
    /// </summary>
    public required string Text { get; set; }
}