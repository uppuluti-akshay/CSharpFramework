@ui
Feature: Demo

@tag1
Scenario: Verify Google homepage loads
	Given I navigate to "https://www.google.com"
	When the homepage is loaded 
	Then the title should be "Google"

 Scenario: Verify Google search for Java Selenium
    Given I navigate to "https://www.google.com"
    When the homepage is loaded
    And I enter "Java Selenium" into the search box
    And I click the search button
    Then the search results page is loaded
    And I click the first search result link

