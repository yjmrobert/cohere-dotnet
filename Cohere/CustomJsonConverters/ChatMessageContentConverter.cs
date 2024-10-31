using System.Text.Json;
using System.Text.Json.Serialization;
using Cohere.Types;

namespace Cohere.CustomJsonConverters;

/// <summary>
/// A custom JSON converter for handling deserialization of the ChatMessageContent type
/// </summary>
public class ChatMessageContentConverter : JsonConverter<object>
{
    /// <summary>
    /// Reads and converts JSON content to either a string or a list of ChatResponseMessageText objects, depending on the format
    /// </summary>
    /// <param name="reader"> The reader that provides JSON content to be converted </param>
    /// <param name="typeToConvert"> The type of object to convert, not explicitly used here due to flexible content types </param>
    /// <param name="options"> Options to control deserialization behavior </param>
    /// <returns>
    /// Returns a string if the JSON token is a single string, or a List of ChatResponseMessageText objects 
    /// if the JSON token is an array. Throws a JsonException if the content format is unsupported.
    /// </returns>
    /// <exception cref="JsonException">Thrown if the JSON content does not match an expected format.</exception>
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }
        else if (reader.TokenType == JsonTokenType.StartArray)
        {
            return JsonSerializer.Deserialize<List<ChatResponseMessageText>>(ref reader, options);
        }

        throw new JsonException("Content must be either a string or an array of ChatResponseMessageText.");
    }

    /// <summary>
    /// Writes the ChatMessageContent object to JSON, serializing either a string or a list of ChatResponseMessageText objects
    /// </summary>
    /// <param name="writer"> The writer to which the JSON output is written </param>
    /// <param name="value"> The ChatMessageContent value to serialize, which may be a string or a list of message objects </param>
    /// <param name="options"> Options to control serialization behavior </param>
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
