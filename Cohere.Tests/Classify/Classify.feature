Feature: Classify
  
  As a developer
  I want to use the CohereClient class to interact with the Cohere API Classify endpoint

  Background:
    Given I have a valid API key
    And I have instantiated the Cohere client

  @ValidRequests
  Scenario Outline: Send a valid classify request with various settings
    When I send a valid classify request with "<TestCase>"
    Then I should receive a valid classify response

    Examples:
      | TestCase            |
      | BasicValidRequest   |
      | MultipleLabels      |
      | HighConfidence      |
      | IdenticalInputs     |
      | MixedLabels         |

  @InvalidRequests
  Scenario Outline: Send an invalid classify request with incorrect settings
    When I send an invalid classify request with "<InvalidCase>"
    Then I should receive an error response

    Examples:
      | InvalidCase                       |
      | NullValues                        |
      | UnknownTruncate                   |
      | LessThanTwoExamplesPerLabel       |
      | SingleLabelOnly                   |
      | EmptyExamples                     |
      | HighVolume                        |
