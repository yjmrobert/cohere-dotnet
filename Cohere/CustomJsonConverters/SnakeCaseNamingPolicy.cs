using System.Text.Json;

namespace Cohere.CustomJsonConverters;

/// <summary>
/// A custom JsonNamingPolicy to convert PascalCase or camelCase to snake_case
/// </summary>
public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    /// <summary>
    /// Converts the name of a property to snake_case
    /// </summary>
    /// <param name="name"> The name of the property to convert </param>
    public override string ConvertName(string name)
    {
        return string.Concat(name.Select((x, i) =>
            i > 0 && char.IsUpper(x) ? "_" + x.ToString().ToLower() : x.ToString().ToLower()));
    }
}