Feature: SearchEngine
	As an User I want to be able to use the service
	To search for shirts with different sizes
	To search for shirts with different colors
	To search for shirts with different sizes and colors

@searchengine

Scenario: Search for shirts

	Given I configure search engine with the following details:
	| Id									| Name				| Size		| Color		|
	| E8F89748-9E61-404A-BE81-558383019A9C	| Red - Small		| Small		| Red		|
	| 1109C7C9-619C-445E-9313-6D525050AAEA	| Red - Large		| Large		| Red		|
	| 209475B9-CFCA-4A60-9EFC-C6255AA0DDEF	| Red - Medium		| Medium	| Red		|
	| 6E9A151E-1E25-494F-9F0A-A8F6F5BA3C3D	| White - Small		| Small		| White		|
	| C8BAA8AA-8137-4D7D-8B6E-C35162F7B0A4	| Yellow - Small	| Small		| Yellow	|
	| 884D53D1-0176-4E41-9ABB-23425A9D6DBD	| Black - Small		| Small		| Black		|

	When I do search with the following search options:
	| Size									| Color				|
	| Small									| Red				|
	| Medium								| Blue				|

	Then I get the following search results:
	| Id                                    | Name				| Size		| Color		|
	| E8F89748-9E61-404A-BE81-558383019A9C  | Red - Small		| Small		| Red		|
	| 209475B9-CFCA-4A60-9EFC-C6255AA0DDEF  | Red - Medium		| Medium	| Red		|

	And I get the following size counts:
	| Name                                  | Count				|
	| Small						 		    | 1					|
	| Medium					 		    | 1					|
	| Large						 		    | 0					|

	And I get the following color counts:
	| Name									| Count				|
	| Red									| 2					|
	| Blue									| 0					|
	| Yellow								| 0					|
	| White									| 0					|
	| Black									| 0					|