namespace Cohere.Types;

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