# Cohere.NET

**Cohere.NET** provides a simple interface for interacting with the [Cohere API](https://cohere.com), enabling developers to integrate state-of-the-art language models into their .NET applications. The supported endpoints are:

- Chat
- Classify
- Rerank

## Installation

You can install the SDK via NuGet:

```bash
dotnet add package CohereDotnet
```

Or in the Package Manager:

```bash
Install-Package CohereDotnet
```

## Getting Started

1. **API Key**: Sign up at [Cohere](https://cohere.com) to get your API key.

2. **Initialize the Client**:

    ```csharp
    using Cohere;
    using Cohere.Models;

    var apiKey = "your-api-key";
    var cohereClient = new CohereClient(apiKey);
    ```

3. **Example Usage**:

- **Chat Endpoint**:

    Use the ```Chat``` endpoint to generate conversational text by sending a series of messages. The [ChatRequest](./Cohere/Types/Chat/ChatRequest.cs) object allows you to customize settings for tailored responses.

    ```csharp
    using Cohere;
    using Cohere.Types;

    var response = await cohereClient.ChatAsync(new ChatRequest
    {
        Messages = new List<ChatMessage>
        {
            new ChatMessage { Role = "user", Content = "Hello, how can I get started with Cohere?" }
        },
        MaxTokens = 50
    });

    var responseContent = response.Message?.Content as List<ChatResponseMessageText>;

    // Print model's response text
    Console.WriteLine(responseContent?[0].Text);
    ```

- **Classify Endpoint**:

    Classify text inputs into specified labels using the ```Classify``` endpoint. The [ClassifyRequest](./Cohere/Types/Classify/ClassifyRequest.cs) object allows you to customize settings for tailored responses.

    ```csharp
    var response = await cohereClient.ClassifyAsync(new ClassifyRequest
    {
        Examples =
        [
            new ClassifyExample { Text = "Dermatologists don't like her!", Label = "Spam" },
            new ClassifyExample { Text = "'Hello, open to this?'", Label = "Spam" },
            new ClassifyExample { Text = "Your parcel will be delivered today", Label = "Not spam" },
            new ClassifyExample { Text = "Review changes to our Terms and Conditions", Label = "Not spam" },
        ],
        Inputs =
        [
            "Confirm your email address",
            "hey i need u to send some $"
        ]
    });

    // Print label prediction of first input
    Console.WriteLine(response.Classifications[0].Predictions[0]);
    ```

- **Rerank Endpoint**:

    Use the ```Rerank``` endpoint to arrange a list of documents based on relevance to a given query. The [RerankRequest](./Cohere/Types/Rerank/RerankRequest.cs) object allows you to customize settings for tailored responses.

    ```csharp
    var response = await cohereClient.RerankAsync(new RerankRequest
    {
        Query = "What is the capital of the United States?"
        Documents = new List<object>
        {
            "Carson City is the capital city of the American state of Nevada.",
            "The Commonwealth of the Northern Mariana Islands is a group of islands in the Pacific Ocean. Its capital is Saipan.",
            "Capitalization or capitalisation in English grammar is the use of a capital letter at the start of a word. English usage varies from capitalization in other languages.",
            "Washington, D.C. (also known as simply Washington or D.C., and officially as the District of Columbia) is the capital of the United States. It is a federal district.",
            "Capital punishment (the death penalty) has existed in the United States since beforethe United States was a country. As of 2017, capital punishment is legal in 30 of the 50 states.",
        }
    });

    // Print index and relevance score of each document
    foreach (var result in response.Results)
    {
        Console.WriteLine($"Index: {results.Index}, Relevance Score: {result.RelevanceScore}");
    }
    ```

4. **Error Handling**:

    The CohereClient class includes error handling to ensure clear feedback when requests fail:

    ```csharp
    try
    {
        var response = await cohereClient.ChatAsync(new ChatRequest { Messages = null });
    }
    catch (CohereApiException ex)
    {
        Console.WriteLine(ex.ToString());
    }
    ```

## Documentation

For full documentation, please refer to the [Cohere API Docs](https://docs.cohere.com/).

## Contributing

Contributions are welcome! Please open an issue or submit a pull request if you would like to contribute to the SDK.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more information.

---

This README provides a quick overview, installation instructions, basic examples, and links to full documentation, making it easy to get started with the Cohere CSharp SDK.
