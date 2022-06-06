Feature: CustomData
	Web API for Custom data

Scenario: Get default custom data as IOptions
	Given the custom configuration file does not exist
	When I make a GET request to 'customdata'
	Then the response status code is '200'
	And the response should contain the default custom data options

Scenario: Get no custom data as IOptions from undefined configuration
	Given the custom configuration file has no entries
	When I make a GET request to 'customdata'
	Then the response status code is '200'
	And the response should contain no custom data options

Scenario: Get no custom data as IOptions from empty configuration
	Given the custom configuration file has an empty entry
	When I make a GET request to 'customdata'
	Then the response status code is '200'
	And the response should contain no custom data options

#
#Scenario: Get test specifc custom data as IOptions
#
#Scenario: Get default custom data as options object
#	Given I am a client
#	And the repository has custom data
#	When I make a GET request to 'customdata'
#	Then the response status code is '200'
#	And the response json should be the expected custom data items
#
#Scenario: Get no custom data as options object
#
#Scenario: Get test specifc custom data as options object
