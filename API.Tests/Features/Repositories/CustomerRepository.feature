Feature: CustomerRepository

Scenario: Listing customers from repository
	Given I have a mock customer database context
	And A customer to get exists in the mock database set
	And I have a customer repository
	And The mapping service has been configured
	When I get the customer list from the repository
	Then the return value should be a non-empty enumerable of customer view models

Scenario: Adding a customer to service 
	Given I have a mock customer database context
	And I have a customer repository
	And I have a customer add model
	And The mapping service has been configured
	When I ask the repository to add the customer add model
	Then the mock customer database context must contain the added customer

Scenario: Getting a customer by id from service
	Given I have a mock customer database context
	And A customer to get exists in the mock database set
	And I have a customer repository
	And The mapping service has been configured
	When I get customer by id from repository
	Then the return value should be a customer view model

Scenario: Editing a customer by using service
	Given I have a mock customer database context
	And I have a customer repository
	And I have a customer edit model
	And The mapping service has been configured
	When I update using the customer edit model using the repository
	Then the mock customer database context must contain the edited customer

Scenario: Deleting a customer by id by using service
	Given I have a mock customer database context
	And A customer to delete exists in the mock database set
	And I have a customer repository
	And I have an id of the customer to delete
	When I delete customer by id using the repository
	Then the mock customer database context must not contain the deleted customer