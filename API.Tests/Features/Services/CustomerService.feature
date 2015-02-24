Feature: CustomerService

Scenario: Listing customers when there are no repository problems
	Given I have a service
	When I get the customer list from the service
	Then the return value should be an enumerable of customer view models

Scenario: Listing customers when there are repository problems
	Given I have a service
	And There is an internal problem with the customer repository
	When I get the customer list from the service
	Then the return value should be null

Scenario: Adding a customer to service when there are no repository problems
	Given I have a service
	And I have a customer add model
	When I ask the service to add the customer add model
	Then there should be no exceptions

Scenario: Adding a customer to service when there are repository problems
	Given I have a service
	And There is an internal problem with the customer repository
	And I have a customer add model
	When I ask the service to add the customer add model
	Then there should be no exceptions

Scenario: Getting a customer by id from service when there are no repository problems
	Given I have a service
	And I have a valid id
	When I get customer by id from service
	Then the return value should be a customer view model

Scenario: Getting a customer by id from service when there are repository problems
	Given I have a service
	And I have a valid id
	And There is an internal problem with the customer repository
	When I get customer by id from service
	Then the return value should be null

Scenario: Getting a customer by id with valid id from service
	Given I have a service
	And I have a valid id
	When I get customer by id from service
	Then the return value should be a customer view model

Scenario: Getting a customer by id with invalid id from service
	Given I have a service
	And I have an invalid id
	When I get customer by id from service
	Then the return value should be null

Scenario: Editing a customer by using service and there are no repository problems
	Given I have a service
	And I have a customer edit model with a valid id
	When I update using the customer edit model using the service
	Then the response should be ok

Scenario: Editing a customer by using service and there are repository problems
	Given I have a service
	And There is an internal problem with the customer repository
	And I have a customer edit model with a valid id
	When I update using the customer edit model using the service
	Then the response should be ok

Scenario: Deleting a customer by id by using service
	Given I have a service
	And I have a valid id
	When I delete customer by id using the service
	Then the response should be no content