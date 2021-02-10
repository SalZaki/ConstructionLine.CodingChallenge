# Construction Line code challenge - Acceptance Tests

![alt text](https://github.com/SalZaki/ConstructionLine.CodingChallenge/blob/master/ConstructionLine.CodingChallenge.Tests.Acceptance/specflow.jpg?raw=true)

## Overview
This test project, demonstrates the use of Speflow is used with C# for our acceptance testing with business domain expert.

SpecFlow aims at bridging the communication gap between domain experts and developers by binding business readable behavior specifications to the underlying implementation. It is an open-source .NET tool inspired by the Cucumber framework which allows writing specification in human readable Gherkin format. Gherkin is a Business Readable, Domain Specific Language that lets you describe software behavior without dealing with how that behavior is implemented and required functionality for a given system.

The most frequently reported benefits of using SpecFlow are:

- Encourages collaboration between team members - both technical and business oriented

- Feature description can be written and/or understood by non-technical people

- Implementing changes more efficiently

- Higher product quality

- Less rework

An example feature file is shown below,

```
Scenario: Search for shirts

	Given I configure search engine with the following details:
	`#1589F0`| Id					| Name			| Size		| Color		|
	| E8F89748-9E61-404A-BE81-558383019A9C	| Red - Small		| Small		| Red		|
	| 1109C7C9-619C-445E-9313-6D525050AAEA	| Red - Large		| Large		| Red		|
	| 209475B9-CFCA-4A60-9EFC-C6255AA0DDEF	| Red - Medium		| Medium	| Red		|
	| 6E9A151E-1E25-494F-9F0A-A8F6F5BA3C3D	| White - Small		| Small		| White		|
	| C8BAA8AA-8137-4D7D-8B6E-C35162F7B0A4	| Yellow - Small	| Small		| Yellow	|
	| 884D53D1-0176-4E41-9ABB-23425A9D6DBD	| Black - Small		| Small		| Black		|

	When I do search with the following search options:
	`#1589F0`| Size					| Color			|
	| Small					| Red			|
	| Medium				| Blue			|

	Then I get the following search results:
	`#1589F0`| Id                                    | Name			| Size		| Color		|
	| E8F89748-9E61-404A-BE81-558383019A9C  | Red - Small		| Small		| Red		|
	| 209475B9-CFCA-4A60-9EFC-C6255AA0DDEF  | Red - Medium		| Medium	| Red		|

	And I get the following size counts:
	`#1589F0`| Name                                  | Count			|
	| Small					| 1			|
	| Medium				| 1			|
	| Large					| 0			|

	And I get the following color counts:
	`#1589F0`| Name					| Count			|
	| Red					| 2			|
	| Blue					| 0			|
	| Yellow				| 0			|
	| White					| 0			|
	| Black					| 0			|

```
