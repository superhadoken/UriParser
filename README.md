# UriParser
A sample implementation of URI parsing using only regex as a basis and without the Uri Class

This is a console application that takes input from the `UrlParser.Data.Input.Data` list of URIs and then breaks each down into its components:
* Scheme
* Authority
* Host
* Port
* Path
* Query
* Fragment

Path and Query are returned by the assembler as a `List<string>()` and allow individual access to each of their parameters.

`UrlParser.Services.IUriPrinter` provides a simple output of the results. Currently this is done via `Console.WriteLine` method to the console window for each URI component, with "N/A" indicating no matches. 

### Getting started
Build and run the console application in VS/IDE of choice.

### UriModelAssembler
Provides access to `UriModel` class - this could be to create a helper library (albeit the internal Uri class is much more substantial). 

### TODO List
* Implement a user input reader to allow URIs to be typed
* Read URIs from a .txt or JSON file instead of an in memory object
* Increase test coverage

