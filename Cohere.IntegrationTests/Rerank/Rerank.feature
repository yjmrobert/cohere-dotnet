Feature: Rerank Integration
  
  As a developer
  I want to use the CohereClient class to interact with the Cohere API Rerank endpoint

  Background: 
    Given I have a valid API key
    And I have instantiated the Cohere client

  Scenario Outline: Rerank text with Cohere
    When I send a valid rerank request
    Then I should receive a valid rerank response