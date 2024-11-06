namespace Cohere.Types.Classify;

/// <summary>
/// The model to define an example for classification training
/// </summary>
public class ClassifyExample
{
    /// <summary>
    /// A text string already manually classified
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// The label of the text string
    /// </summary>
    public string? Label { get; set; }
}