namespace Cohere.Types.Shared;

/// <summary>
///  Meta information about the request and response
/// </summary>
public class CohereMeta
{
    /// <summary>
    /// The version and details of the API that generated the response
    /// </summary>
    public CohereApiVersion? ApiVersion { get; set; }

    /// <summary>
    /// Summary of the billed units for the request
    /// </summary>
    public CohereBillledUnits? BilledUnits { get; set; }

    /// <summary>
    /// Summary of the tokens used for the request
    /// </summary>
    public CohereTokens? Tokens { get; set; }

    /// <summary>
    /// Warnings generated during the request
    /// </summary>
    public List<string>? Warnings { get; set; }
}