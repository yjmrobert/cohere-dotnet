namespace Cohere.Types;

public class ChatTool
{
        /// <summary>
        /// The type of tool to be executed
        /// </summary>
        public string? Type { get; set; } = "function";

        /// <summary>
        /// The function to be executed
        /// </summary>
        public ChatToolFunction? Function { get; set; }

        /// <summary>
        /// The id of the tool call
        /// </summary>
        public string? Id { get; set; }
    }
