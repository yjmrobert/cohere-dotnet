using Cohere.Types;

namespace Cohere.Tests;

/// <summary>
/// Examples of valid request to the Cohere API guided by the API documentation
/// </summary>
public class CohereApiRequests
{
    public static readonly ChatRequest ValidChatRequest1 = new()
    {
        Messages =
        [
            new ChatMessage
            {
                Role = "user",
                Content = "hello world!"
            }
        ]
    };

    public static readonly ClassifyRequest ValidClassifyRequest1 = new()
    {
        Examples =
        [
            new ClassifyExample { Text = "Dermatologists don't like her!", Label = "Spam" },
            new ClassifyExample { Text = "'Hello, open to this?'", Label = "Spam" },
            new ClassifyExample { Text = "I need help please wire me $1000 right now", Label = "Spam" },
            new ClassifyExample { Text = "Nice to know you ;)", Label = "Spam" },
            new ClassifyExample { Text = "Please help me?", Label = "Spam" },
            new ClassifyExample { Text = "Your parcel will be delivered today", Label = "Not spam" },
            new ClassifyExample { Text = "Review changes to our Terms and Conditions", Label = "Not spam" },
            new ClassifyExample { Text = "Weekly sync notes", Label = "Not spam" },
            new ClassifyExample { Text = "'Re: Follow up from today's meeting'", Label = "Not spam" },
            new ClassifyExample { Text = "Pre-read for tomorrow", Label = "Not spam" }
        ],
        Inputs =
        [
            "Confirm your email address",
            "hey i need u to send some $"
        ]
    };

    public static readonly RerankRequest ValidRerankRequest1 = new()
    {
        Model = "rerank-english-v3.0",
        Query = "What is the capital of the United States?",
        Documents = [
        "Carson City is the capital city of the American state of Nevada.",
        "The Commonwealth of the Northern Mariana Islands is a group of islands in the Pacific Ocean. Its capital is Saipan.",
        "Capitalization or capitalisation in English grammar is the use of a capital letter at the start of a word. English usage varies from capitalization in other languages.",
        "Washington, D.C. (also known as simply Washington or D.C., and officially as the District of Columbia) is the capital of the United States. It is a federal district.",
        "Capital punishment (the death penalty) has existed in the United States since before the United States was a country. As of 2017, capital punishment is legal in 30 of the 50 states."
        ],
        TopN = 3
    };
}