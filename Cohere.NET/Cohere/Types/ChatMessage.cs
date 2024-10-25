namespace Cohere.Types;

public class ChatMessage
{
    /// <summary>
    /// The role of the user sending the message
    /// Possible values are "user", "assistant", "tool", and "system"
    /// </summary>
    public required string Role { get; set; }

    /// <summary>
    /// The content of the message
    /// </summary>
    public object? Content { get; set; }

    /// <summary>
    /// The id of the associated tool call that has provided the given content
    /// Only applicable if the role is "tool"
    /// </summary>
    public string? ToolCallId { get; set; }

    /// <summary>
    /// List of tools that have been executed to generate the content
    /// </summary>
    public List<ChatTool>? ToolCalls { get; set; }

    /// <summary>
    /// A chain-of-thought style reflection and plan that the model generates when working with Tools
    /// </summary>
    public string? ToolPlan { get; set; }

    /// <summary>
    /// A list of citations that the model has generated
    /// </summary>
    /// TO DO: Handle parsing various citation object types
    public List<object>? Citations { get; set; }


}
