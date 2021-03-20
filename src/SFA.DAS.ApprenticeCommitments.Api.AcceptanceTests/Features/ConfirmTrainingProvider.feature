@database
@api
Feature: ConfirmTrainingProvider
	As an apprentice I want to be able to confirm my training provider

Scenario: Positively confirm a training provider
	Given we have an apprenticeship waiting to be confirmed
	And a ConfirmTrainingProviderRequest stating the training provider is correct
	When we send the confirmation
	Then the response is OK
	And the apprenticeship record is updated

Scenario: Negatively confirm a training provider
	Given we have an apprenticeship waiting to be confirmed
	And a ConfirmTrainingProviderRequest stating the training provider is incorrect
	When we send the confirmation
	Then the response is OK
	And the apprenticeship record is updated

Scenario: Attempt to change a training provider confirmirmation
	Given we have an apprenticeship that has previously had its training provider positively confirmed
	And a ConfirmTrainingProviderRequest stating the training provider is incorrect
	When we send the confirmation
	Then the response is BadRequest
	And the apprenticeship record remains unchanged

Scenario: Positively confirm an employer
	Given we have an apprenticeship waiting to be confirmed
	And a ConfirmEmployerRequest stating the employer is correct
	When we send the confirmation
	Then the response is OK
	And the apprenticeship record is updated

Scenario: Negatively confirm an employer
	Given we have an apprenticeship waiting to be confirmed
	And a ConfirmEmployerRequest stating the employer is incorrect
	When we send the confirmation
	Then the response is OK
	And the apprenticeship record is updated

Scenario: Attempt to change an employer confirmirmation
	Given we have an apprenticeship that has previously had its employer positively confirmed
	And a ConfirmEmployerRequest stating the employer is incorrect
	When we send the confirmation
	Then the response is BadRequest
	And the apprenticeship record remains unchanged

