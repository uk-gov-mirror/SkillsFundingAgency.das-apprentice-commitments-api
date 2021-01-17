@api

Feature: CreateApprenticeship
	As a application user
	I want to create a valid apprenticeship in the database

Scenario: Trying to create an apprenticeship with invalid values
	Given we have an invalid apprenticeship request
	When the apprenticeship is posted
	Then the result should return bad request

Scenario: Trying to create an apprenticeship with valid values
	Given we have a valid apprenticeship request
	When the apprenticeship is posted
	Then the result should return accepted