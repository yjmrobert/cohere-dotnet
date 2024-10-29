using System.Net;
using System.Text;
using Cohere.Types;
using Moq;
using Moq.Protected;
using Reqnroll;

namespace Cohere.Tests
{
    [Binding]
    public class CohereStepDefinitions
    {
        private CohereClient _client;
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpResponseMessage _responseMessage;
        private readonly string _apiKey = "test-api-key";
        private ChatRequest _chatRequest;
        private ClassifyRequest _classifyRequest;
        private RerankRequest _rerankRequest;
        private ICohereResponse _response;
        
        /// <summary>
        /// Verifies that an API key is available for testing
        /// Since no call to the API is made, the API key is not validated
        /// </summary>
        [Given(@"I have a valid API key")]
        public void GivenIHaveAValidApiKey()
        {
            Assert.NotNull(_apiKey);
        }

        /// <summary>
        /// Initializes a new instance of the Cohere client with a mock HttpClient for testing
        /// </summary>
        [Given(@"I have instantiated the Cohere client")]
        public void GivenIHaveInstantiatedTheCohereClient()
        { 
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.cohere.ai")
            };
            _client = new CohereClient(_apiKey, httpClient);
        }

        /// <summary>
        /// Sends a valid chat request to the mocked Cohere API endpoint and sets up a mock response to be returned
        /// Uses example data from the Cohere API documentation
        /// </summary>
        [When(@"I send a valid chat request")]
        public async Task WhenISendAValidChatRequest()
        {
            _chatRequest = new ChatRequest
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

            string chatResponseContent = @"
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

            _responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(chatResponseContent, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(_responseMessage);

            _response = await _client.ChatAsync(_chatRequest);
        }

        /// <summary>
        /// Verifies that a valid chat response is received from the mocked Cohere API and checks parsing of specific response fields
        /// </summary>
        [Then(@"I should receive a valid chat response")]
        public void ThenIShouldReceiveAValidChatResponse()
        {
            Assert.NotNull(_response);
            var chatResponse = (ChatResponse)_response;

            var text = chatResponse.Message?.Content as List<ChatResponseMessageText>;

            Assert.Equal(typeof(string), chatResponse.Id.GetType());
            Assert.Equal("Hello from Cohere!", text?[0].Text);
            Assert.Equal(ChatResponseFinishReasonEnum.COMPLETE, chatResponse.FinishReason);
            Assert.Equal(typeof(ChatUsage), chatResponse.Usage?.GetType());
        }

        /// <summary>
        /// Sends a valid classify request to the mocked Cohere API and sets up a mock classification response to be returned
        /// Uses example data from the Cohere API documentation
        /// </summary>
        [When(@"I send a valid classify request")]
        public async Task WhenISendAValidClassifyRequest()
        {
            List<ClassifyExample> examples =
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

            ];

            List<string> inputs = [
                "Confirm your email address",
                "hey i need u to send some $"
            ];
            
            _classifyRequest = new ClassifyRequest
            {
                Examples = examples,
                Inputs = inputs
            };

            string classifyResponseContent = @"
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

            _responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(classifyResponseContent, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(_responseMessage);

            _response = await _client.ClassifyAsync(_classifyRequest);
        }

        /// <summary>
        /// Verifies that a valid classification response is received from the mocked Cohere API and checks parsing of specific fields
        /// </summary>
        [Then(@"I should receive a valid classification response")]
        public void ThenIShouldReceiveAValidClassifyResponse()
        {
            Assert.NotNull(_response);
            var classifyResponse = (ClassifyResponse)_response;

            Assert.Equal("Not spam", classifyResponse.Classifications[0].Predictions[0]);
            Assert.Equal("Spam", classifyResponse.Classifications[1].Predictions[0]);
            Assert.Equal(typeof(string), classifyResponse.Id.GetType());
            Assert.Equal(typeof(CohereMeta), classifyResponse.Meta?.GetType());
        }

        /// <summary>
        /// Sends a valid rerank request to the mocked Cohere API and sets up a mock rerank response to be returned
        /// Uses example data from the Cohere API documentation
        /// </summary>
        [When(@"I send a valid rerank request")]
        public async Task WhenISendAValidRerankRequest(){

            List<object> documents = [
                "Carson City is the capital city of the American state of Nevada.",
                "The Commonwealth of the Northern Mariana Islands is a group of islands in the Pacific Ocean. Its capital is Saipan.",
                "Capitalization or capitalisation in English grammar is the use of a capital letter at the start of a word. English usage varies from capitalization in other languages.",
                "Washington, D.C. (also known as simply Washington or D.C., and officially as the District of Columbia) is the capital of the United States. It is a federal district.",
                "Capital punishment (the death penalty) has existed in the United States since before the United States was a country. As of 2017, capital punishment is legal in 30 of the 50 states."
            ];

            _rerankRequest = new RerankRequest
            {
                Model = "rerank-english-v3.0",
                Query = "What is the capital of the United States?",
                Documents = documents,
                TopN = 3
            };

            string rerankResponseContent = @"
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

            _responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(rerankResponseContent, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(_responseMessage);

            _response = await _client.RerankAsync(_rerankRequest); 
        }

        /// <summary>
        /// Verifies that a valid rerank response is received from the mocked Cohere API and checks parsing of specific response fields
        /// </summary>
        [Then(@"I should receive a valid rerank response")]
        public void ThenIShouldReceiveAValidRerankResponse()
        {
            Assert.NotNull(_response);
            var rerankResponse = (RerankResponse)_response;

            Assert.Equal(3, rerankResponse.Results[0].Index);
            Assert.Equal(typeof(double), rerankResponse.Results[0].RelevanceScore.GetType());
            Assert.Equal(typeof(CohereMeta), rerankResponse.Meta?.GetType());
            Assert.Equal(typeof(List<string>), rerankResponse.Meta?.Warnings?.GetType());
        }
    }
}
