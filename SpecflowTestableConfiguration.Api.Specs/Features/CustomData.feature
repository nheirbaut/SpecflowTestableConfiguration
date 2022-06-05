Feature: CustomData
	Web API for Custom data

Scenario: Get custom data
	Given I am a client
	And the repository has custom data
	When I make a GET request to 'customdata'
	Then the response status code is '200'
	And the response json should be the expected custom data items
