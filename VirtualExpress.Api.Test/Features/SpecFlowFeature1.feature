Feature: Package
	As user
	I want to know the status of my package
	To know if it is in good condition

@mytag
Scenario: User want to know the status of your package
	Given a verified user
	And the user want to know the status of package
	When the user clicks on package
	Then the system will show his status of package