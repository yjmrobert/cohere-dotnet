namespace Cohere.Types;

/// <summary>
/// The version information of the API
/// </summary>
public class CohereApiVersion
{
    /// <summary>
    /// The version of the API
    /// </summary>
    public required string Version { get; set; }

    /// <summary>
    /// Specify whether the version is deprecated
    /// </summary>
    public bool? IsDeprecated { get; set; }

    /// <summary>
    /// Specify whether the version is experimental
    /// </summary>
    public bool? IsExperimental { get; set; }
}