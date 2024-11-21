namespace Cohere.Types.Shared;

/// <summary>
/// Interface for a Cohere request
/// </summary>
public interface ICohereRequest
{
    /// <summary>
    /// The model to use for the request
    /// </summary>
    public string? Model { get; set; }
}