namespace Cohere.Types;

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