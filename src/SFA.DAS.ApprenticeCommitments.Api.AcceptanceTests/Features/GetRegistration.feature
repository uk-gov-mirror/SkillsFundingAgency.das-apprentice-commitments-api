@database
@api

Feature: GetRegistration
	As a application user
	I want to retrieve registration details

Scenario: Getting a registration which does exist
	Given there is a registration
	When we try to retrieve the registration
	Then the result should return ok
	And the response should match the registration in the database

Scenario: Trying to get a registration which does NOT exist
	Given there is no registration
	When we try to retrieve the registration
	Then the result should return not found

Scenario: Trying to get a registration with an invalid Id
	When we try to retrieve the registration using a bad request format
	Then the result should return bad request

Scenario: Trying to get a registration with an empty Id
	Given there is an empty registration
	When we try to retrieve the registration
	Then the result should return bad request
	And the error must be say registration must be valid
