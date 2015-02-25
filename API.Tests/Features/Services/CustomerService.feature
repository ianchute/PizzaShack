Feature: CustomerService

Scenario: Listing customers from service
	Given I have a mock customer repository
	And I have a customer service
	When I get the customer list from the service
	Then the return value should be a non-empty enumerable of customer view models

Scenario: Listing customers when there are repository problems
	Given I have a mock customer repository that throws exceptions
	And I have a customer service
	When I get the customer list from the service
	Then the return value should be an empty enumerable of customer view models

Scenario: Adding a customer to service 
	Given I have a mock customer repository
	And I have a customer service
	And I have a customer add model
	When I ask the service to add the customer add model
	Then the return value should be true

Scenario: Adding a customer to service when there are repository problems
	Given I have a mock customer repository that throws exceptions
	And I have a customer service
	And I have a customer add model
	When I ask the service to add the customer add model
	Then the return value should be false

Scenario: Getting a customer by id from service
	Given I have a mock customer repository
	And I have a customer service
	And I have an id
	When I get customer by id from service
	Then the return value should be a customer view model

Scenario: Getting a customer by id from service when there are repository problems
	Given I have a mock customer repository that throws exceptions
	And I have a customer service
	And I have an id
	When I get customer by id from service
	Then the return value should be an empty customer view model

Scenario: Editing a customer by using service
	Given I have a mock customer repository
	And I have a customer service
	And I have a customer edit model
	When I update using the customer edit model using the service
	Then the return value should be true

Scenario: Editing a customer by using service and there are repository problems
	Given I have a mock customer repository that throws exceptions
	And I have a customer service
	And I have a customer edit model
	When I update using the customer edit model using the service
	Then the return value should be false

Scenario: Deleting a customer by id by using service
	Given I have a mock customer repository
	And I have a customer service
	And I have an id
	When I delete customer by id using the service
	Then the return value should be true

Scenario: Deleting a customer by id by using service and there are repository problems
	Given I have a mock customer repository that throws exceptions
	And I have a customer service
	And I have a customer edit model
	When I update using the customer edit model using the service
	Then the return value should be false