namespace Cohere.Types.Shared;

/// <summary>
/// Summary of the billed units for a request and response
/// </summary>
public class CohereBillledUnits
{
    /// <summary>
    /// The number of billed images
    /// </summary>
    public double? Images { get; set; }

    /// <summary>
    /// The number of billed input tokens
    /// </summary>
    public double? InputTokens { get; set; }

    /// <summary>
    /// The number of billed output tokens
    /// </summary>
    public double? OutputTokens { get; set; }

    /// <summary>
    /// The number of billed search units
    /// </summary>
    public double? SearchUnits { get; set; }

    /// <summary>
    /// The number of billed classifications
    /// </summary>
    public double? Classifications { get; set; }
}