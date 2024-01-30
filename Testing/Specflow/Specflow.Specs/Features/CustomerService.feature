Feature: CustomerService

Scenario: Searching for a customer called Nick
	Given there is a customer called Nick
	When I search for a customer called Nick
	Then the name of the customer returned should be Nick