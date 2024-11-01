@Ignore
Feature: Chat Integration
  
  As a developer
  I want to use the CohereClient class to interact with the Cohere API Chat endpoint

  Background: 
    Given I have a valid API key
    And I have instantiated the Cohere client

  Scenario Outline: Chat with Cohere
    When I send a valid chat request
    Then I should receive a valid chat response