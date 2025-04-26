Feature: API Testing
  As a user
  I want to test API endpoints
  So that I can verify their functionality

  Scenario: Verify GET request to fetch all posts
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a GET request to "/posts"
    Then the response status code should be 200
    And the response content should not be null

  Scenario: Verify GET request to fetch a specific post
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a GET request to "/posts/1"
    Then the response status code should be 200
    And the response content should not be null

  Scenario: Verify GET request to fetch comments for a specific post
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a GET request to "/posts/1/comments"
    Then the response status code should be 200
    And the response content should not be null

  Scenario: Verify GET request to fetch comments by postId
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a GET request to "/comments?postId=1"
    Then the response status code should be 200
    And the response content should not be null

  Scenario: Verify POST request to create a new post
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a POST request to "/posts" with the following data:
      | title       | body          | userId |
      | Test Title  | Test Content  | 1      |
    Then the response status code should be 201
    And the response content should not be null

  Scenario: Verify PUT request to update a post
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a PUT request to "/posts/1" with the following data:
      | title       | body          | userId |
      | Updated Title | Updated Content | 1  |
    Then the response status code should be 200
    And the response content should not be null

  Scenario: Verify PATCH request to partially update a post
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a PATCH request to "/posts/1" with the following data:
      | title       |
      | Patched Title |
    Then the response status code should be 200
    And the response content should not be null

  Scenario: Verify DELETE request to delete a post
    Given the API base URL is "https://jsonplaceholder.typicode.com"
    When I send a DELETE request to "/posts/1"
    Then the response status code should be 200
