Feature: Chat
  
  As a developer
  I want to use the CohereClient class to interact with the Cohere API Chat endpoint

  Background: 
    Given I have a valid API key
    And I have instantiated the Cohere client

  @ValidRequests
  Scenario Outline: Send a valid chat request with various settings
    When I send a valid chat request with "<TestCase>"
    Then I should receive a valid chat response

    Examples:
      | TestCase                    |
      | BasicValidRequest           |
      | MaxTokensRequest            |
      | TemperatureRequest          |
      | BoundaryKAndPZeroAndOne     |
      | BoundaryKAndPMaxAndMin      |
      | FiveStopSequencesRequest    |

  @InvalidRequests
  Scenario Outline: Send an invalid chat request with incorrect settings
    When I send an invalid chat request with "<InvalidCase>"
    Then I should receive a chat error response

    Examples:
      | InvalidCase                 |
      | InvalidMaxTokens            |
      | InvalidTemperature          |
      | InvalidSafetyMode           |
      | ExceedStopSequencesLimit    |
      | MissingRequiredFields       |
      