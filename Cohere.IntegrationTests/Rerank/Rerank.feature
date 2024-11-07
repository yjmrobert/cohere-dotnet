@Ignore
Feature: Rerank Integration
  
  As a developer
  I want to use the CohereClient class to interact with the Cohere API Rerank endpoint

  Background: 
    Given I have a valid API key
    And I have instantiated the Cohere client

  @ValidRequests
  Scenario Outline: Send a valid rerank request with various configurations
    When I send a valid rerank request with "<TestCase>"
    Then I should receive a valid rerank response

    Examples:
      | TestCase                |
      | BasicValidRequest       |
      | LargeDocumentSet        |
      | HighRelevanceThreshold  |
      | DuplicateDocuments      |
      | NoRelevance             |

  @InvalidRequests
  Scenario Outline: Send an invalid rerank request with incorrect settings
    When I send an invalid rerank request with "<InvalidCase>"
    Then I should receive a rerank error response

    Examples:
      | InvalidCase             |
      | NullValues              |
      | EmptyQuery              |
      | NoDocumentsProvided     |
