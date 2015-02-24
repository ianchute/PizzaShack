Feature: CustomersController

Scenario: Listing customers with responding service
	Given I have a customer service that returns data when list is called
	And I have a controller
	When I request for a list
	Then the response should be ok
	And the response content should be an enumerable customer view model

Scenario: Listing customers with non-responding service
	Given I have a customer service that returns null when list is called
	And I have a controller
	When I request for a list
	Then the response should be not found
	And the response content should be an enumerable customer view model

Scenario: Adding a customer with valid model state
	Given I have a customer service that returns true when add is called
	And I have a controller
	And I have a customer add model
	And the model state is valid
	When I add a customer add model
	Then the response should be created

Scenario: Adding a customer with service returning true
	Given I have a customer service that returns true when add is called
	And I have a controller
	And I have a customer add model
	And the model state is valid
	When I add a customer add model
	Then the response should be created

Scenario: Adding a customer with service returning false
	Given I have a customer service that returns false when add is called
	And I have a controller
	And I have a customer add model
	And the model state is valid
	When I add a customer add model
	Then the response should be internal server error

Scenario: Adding a customer with invalid model state
	Given I have a customer service that returns true when add is called
	And I have a controller
	And I have a customer add model
	And the model state is invalid
	When I add a customer add model
	Then the response should be unprocessable entity

Scenario: Getting a customer by id with data response from service
	Given I have a customer service that returns data when get is called
	And I have a controller
	When I get customer by id
	Then the response should be ok	
	And the response content should be a customer view model

Scenario: Getting a customer by id with null response from service
	Given I have a customer service that returns null when get is called
	And  I have a controller
	When I get customer by id
	Then the response should be not found

Scenario: Editing a customer with valid model state
	Given I have a customer service that returns true when edit is called
	And I have a controller
	And I have a customer edit model
	And the model state is valid
	When I update using the customer edit model
	Then the response should be ok

Scenario: Editing a customer with invalid model state
	Given I have a customer service that returns true when edit is called
	And I have a controller
	And I have a customer edit model
	And the model state is invalid
	When I update using the customer edit model
	Then the response should be unprocessable entity

Scenario: Editing a customer with service returning true
	Given I have a customer service that returns true when edit is called
	And I have a controller
	And I have a customer edit model
	And the model state is valid
	When I update using the customer edit model
	Then the response should be ok

Scenario: Editing a customer with service returning false
	Given I have a customer service that returns false when edit is called
	Given I have a controller
	And I have a customer edit model
	And the model state is valid
	When I update using the customer edit model
	Then the response should be not found

Scenario: Deleting a customer by id with service returning true
	Given I have a customer service that returns true when delete is called
	And I have a controller
	When I delete customer by id
	Then the response should be no content

Scenario: Deleting a customer by id with service returning false
	Given I have a customer service that returns false when delete is called
	And I have a controller
	And I have an invalid id
	When I delete customer by id
	Then the response should be not found