## About the project
I build a project with .NET Core 2.0 and Visual studio 2017. My architecture is inspirated by the Hexagonal Architecture of Alistair Cockburn. 

I used some libraries : 
* Swagger in Checkout.API to build the documentation
* FakeItEasy to fake interfaces on my tests
* TestStack.BDDFy to BDDFy my tests
* RestSharp to making Http calls. 

Everything was build in TDD.

### Project structure 

* **Checkout.API** : HTTP endpoint to provide the functionalities required by the business
* **Checkout.Domain** : Contains the business of my code, without any "framework" dependancies
* **Checkout.Infrastructure** : Provide data of my API/Domain. I keep data in memory, if we want to plug a database, you can do it by reimplementing all the classes in this project.
* **Checkout.SDK** : The SDK to consume the API.
* **Checkout.AcceptanceTests** : Acceptance tests of my domain.
* **Checkout.E2ETests** : End to End testing. Thoses tests are the garantee of the integration of the HTTP, Domain & Database parts.

### About the security
To do this project, I assume that's the security is handle by an other service. That's why I start my API with /consumers/:consumerId. 

## How to tests

### AcceptanceTests
To run AcceptanceTests, you can just run them with the Test Explorer. 

### E2ETests
First, you have to start (without the debugger) the Checkout.API project to up the API endpoint. After that's, you can run all tests.

## Documentations 
The API Documentation is available here : http://localhost:1337/swagger

The tests documentation are generated in
* /Checkout.E2ETests/bin/Debug/netcoreapp2.0/BDDfy.html
* /Checkout/Checkout.AcceptanceTests/bin/Debug/netcoreapp2.0/BDDfy.html

Joffrey,