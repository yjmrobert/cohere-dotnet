namespace Cohere.Types;

/// <summary>
/// The object used to display the confidence of a classification label
/// </summary>
public class ClassificationLabelConfidence
{
    /// <summary>
    /// The corresponding confidence of the classification label
    /// </summary>
    public double? Confidence { get; set; }
}