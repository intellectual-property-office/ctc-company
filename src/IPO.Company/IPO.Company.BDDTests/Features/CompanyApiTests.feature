Feature: CompanyApi

Scenario: Request Company House company details successfully
	Given Requesting company details for every valid type of company number
		| CompanyNumber | Type                      |
		| 12345678      | All digits                |
		| SC654321      | Address prefix and digits |
	When apiURL / requested
	Then the company details are returned succesfully