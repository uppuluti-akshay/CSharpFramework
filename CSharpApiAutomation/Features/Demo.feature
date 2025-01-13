@api
Feature: Demo

A short summary of the feature

@tc:1234
Scenario: Create a new User
	Given User with name "Akshay"
	And User with job "Manager"
	When send request to create user
	Then validate the user is created
