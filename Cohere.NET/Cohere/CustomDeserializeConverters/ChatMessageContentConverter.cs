using System.Text.Json;
using System.Text.Json.Serialization;
using Cohere.Types;

namespace Cohere.CustomDeserializeConverters;

public class ChatMessageContentConverter : JsonConverter<object>
{
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

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
