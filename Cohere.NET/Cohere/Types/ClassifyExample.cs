namespace Cohere.Types;

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