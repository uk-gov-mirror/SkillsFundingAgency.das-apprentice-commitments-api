@database
@api
Feature: ChangeEmailAddress
	As an apprentice
	I want to be able to change the email address associated with my digital account
	So that I can still access my commitment & receive updates from the service

Scenario: Change email address
	Given we have an existing apprentice
	When we change the apprentice's email address
	Then the apprentice record is created