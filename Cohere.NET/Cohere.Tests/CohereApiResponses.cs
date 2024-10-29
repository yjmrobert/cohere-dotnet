namespace Cohere.Tests;

public static class CohereApiResponses
{
    /// Examples of valid responses from the Cohere API guided by the API documentation
    public static readonly string ValidChatResponse1 = @"
        {
        ""id"": ""c14c80c3-18eb-4519-9460-6c92edd8cfb4"",
        ""finish_reason"": ""COMPLETE"",
        ""message"": {
            ""role"": ""assistant"",
            ""content"": [
            {
                ""type"": ""text"",
                ""text"": ""Hello from Cohere!""
            }
            ]
        },
        ""usage"": {
            ""billed_units"": {
            ""input_tokens"": 5,
            ""output_tokens"": 15
            },
            ""tokens"": {
            ""input_tokens"": 71,
            ""output_tokens"": 15
            }
        }
        }";

    public static readonly string ValidClassifyResponse1 = @"
        {
        ""id"": ""86886163-b3f3-4e36-8554-60eca7696216"",
        ""classifications"": [
            {
            ""id"": ""842d12fe-934b-4b71-82c2-c581eca00718"",
            ""predictions"": [
                ""Not spam""
            ],
            ""confidences"": [
                0.5661598
            ],
            ""labels"": {
                ""Not spam"": {
                ""confidence"": 0.5661598
                },
                ""Spam"": {
                ""confidence"": 0.43384025
                }
            },
            ""classification_type"": ""single-label"",
            ""input"": ""Confirm your email address"",
            ""prediction"": ""Not spam"",
            ""confidence"": 0.5661598
            },
            {
            ""id"": ""e1a39b3e-1ecd-41d2-be75-90ed726f7b9e"",
            ""predictions"": [
                ""Spam""
            ],
            ""confidences"": [
                0.9909811
            ],
            ""labels"": {
                ""Not spam"": {
                ""confidence"": 0.009018883
                },
                ""Spam"": {
                ""confidence"": 0.9909811
                }
            },
            ""classification_type"": ""single-label"",
            ""input"": ""hey i need u to send some $"",
            ""prediction"": ""Spam"",
            ""confidence"": 0.9909811
            }
        ],
        ""meta"": {
            ""api_version"": {
            ""version"": ""1""
            },
            ""billed_units"": {
            ""classifications"": 2
            }
        }
        }";

    public static readonly string ValidRerankResponse1 = @"
        {
        ""results"": [
            {
            ""index"": 3,
            ""relevance_score"": 0.999071
            },
            {
            ""index"": 4,
            ""relevance_score"": 0.7867867
            },
            {
            ""index"": 0,
            ""relevance_score"": 0.32713068
            }
        ],
        ""id"": ""07734bd2-2473-4f07-94e1-0d9f0e6843cf"",
        ""meta"": {
            ""api_version"": {
            ""version"": ""2"",
            ""is_experimental"": true
            },
            ""billed_units"": {
            ""search_units"": 1
            },
            ""warnings"": [
            ""You are using an experimental version, for more information please refer to https://docs.cohere.com/versioning-reference""
            ]
            }
        }";

    // TO DO: Add more valid responses for other API endpoints
    // TO DO: Add invalid responses for all API endpoints
}

