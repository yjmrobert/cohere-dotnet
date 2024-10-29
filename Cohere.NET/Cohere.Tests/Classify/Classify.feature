Feature: Classify
  
  As a developer
  I want to use the CohereClient class to interact with the Cohere API Classify endpoint

  Background: 
    Given I have a valid API key
    And I have instantiated the Cohere client
    
  Scenario Outline: Classify valid text with Cohere
    When I send a valid classify request
    Then I should receive a valid classification response