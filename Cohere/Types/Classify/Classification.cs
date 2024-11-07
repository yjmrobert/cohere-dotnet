namespace Cohere.Types.Classify;

/// <summary>
/// A class representing the classification of a text
/// </summary>
public class Classification
{
    /// <summary>
    /// The id of the classifications
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// List of predictions for the text
    /// </summary>
    public required List<string> Predictions { get; set; }

    /// <summary>
    /// An array containing the confidence scores of all the predictions in the same order
    /// </summary>
    public required List<double> Confidences { get; set; }

    /// <summary>
    /// A map containing each label and its confidence score according to the classifier
    /// </summary>
    public required Dictionary<string, ClassificationLabelConfidence> Labels { get; set; }

    /// <summary>
    /// The type of classification performed
    /// Allwed values are single-label and multi-label
    /// </summary>
    public required string ClassificationType { get; set; }

    /// <summary>
    /// The input text that was classified
    /// </summary>
    public string? Input { get; set; }
}