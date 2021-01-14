@api

Feature: HealthCheck
	In order to check if the api is working
	As a appliocation monitor
	I want to be told the status of the api

Scenario: Checking Api is running correctly
	Given the api has started
	When the ping endpoint is called
	Then the result should be return okay