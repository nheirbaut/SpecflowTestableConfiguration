Feature: CustomData
	Web API for Custom data

Scenario: Get default custom data as IOptions
	Given the custom configuration file does not exist
	When I make a GET request to 'optionscustomdata'
	Then the response status code is '200'
	And the response should contain the default custom data options

Scenario: Get no custom data as IOptions from undefined configuration
	Given the custom configuration file has no entries
	When I make a GET request to 'optionscustomdata'
	Then the response status code is '200'
	And the response should contain no custom data options

Scenario: Get no custom data as IOptions from empty configuration
	Given the custom configuration file has an empty entry
	When I make a GET request to 'optionscustomdata'
	Then the response status code is '200'
	And the response should contain no custom data options

Scenario: Get custom data as IOptions when defined
	Given the custom configuration file has the following custom data entries:
		| Name    |
		| Entry 1 |
		| Entry 2 |
		| Entry 3 |
	When I make a GET request to 'optionscustomdata'
	Then the response status code is '200'
	And the response should contain the following custom data options:
		| Name    |
		| Entry 1 |
		| Entry 2 |
		| Entry 3 |

