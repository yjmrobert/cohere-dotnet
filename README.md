# Cohere.NET

**Cohere.NET** provides a simple interface for interacting with the [Cohere API](https://cohere.com), enabling developers to integrate state-of-the-art language models into their .NET applications.

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

- **Generate Text**:

```csharp
var response = await cohereClient.GenerateAsync(new GenerateRequest
{
    Prompt = "Once upon a time",
    MaxTokens = 50
});
Console.WriteLine(response.GeneratedText);
```

- **Get Embeddings**:

```csharp
var response = await cohereClient.EmbedAsync(new EmbedRequest
{
    Texts = new List<string> { "This is a sentence to embed." }
});
Console.WriteLine(response.Embeddings.First());
```

- **Classify Text**:

```csharp
var response = await cohereClient.ClassifyAsync(new ClassifyRequest
{
    Inputs = new List<string> { "Is this text positive or negative?" },
    Labels = new List<string> { "Positive", "Negative" }
});
Console.WriteLine(response.Classifications.First().Prediction);
```

## Documentation

For full documentation, please refer to the [Cohere API Docs](https://docs.cohere.com/).

## Contributing

Contributions are welcome! Please open an issue or submit a pull request if you would like to contribute to the SDK.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more information.

---

This README provides a quick overview, installation instructions, basic examples, and links to full documentation, making it easy to get started with the Cohere CSharp SDK.