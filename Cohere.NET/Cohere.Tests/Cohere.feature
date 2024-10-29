Feature: Cohere API Client
  
  As a developer
  I want to use the CohereClient class to interact with the Cohere API
  So that I can generate, classify, and rerank text data

  Background: 
    Given I have a valid API key
    And I have instantiated the Cohere client

  Scenario Outline: Chat with Cohere
    When I send a valid chat request
    Then I should receive a valid chat response

  Scenario Outline: Classify text with Cohere
    When I send a valid classify request
    Then I should receive a valid classification response

  Scenario Outline: Rerank text with Cohere
    When I send a valid rerank request
    Then I should receive a valid rerank response
