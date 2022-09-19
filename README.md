# Cyber coffee machine

## Requirements
Design and implement an HTTP API that controls an imaginary internet-connected coffee machine. Your solution should fulfil the following criteria:
1.	When the endpoint GET /brew-coffee is called, the endpoint returns a 200 OK status code with a status message and the current date/time in the response body as a JSON object, with the date/time formatted as an ISO-8601 value e.g. 
```json
{
  "message": "Your piping hot coffee is ready",
  "prepared": "2021-02-03T11:56:24+0900"
}
```
2.	On every fifth call to the endpoint defined in #1, the endpoint should return `503 Service Unavailable` with an empty response body, to signify that the coffee machine is out of coffee.
3.	If the date is April 1st, then all calls to the endpoint defined in #1 should return `418 I’m a teapot` instead, with an empty response body, to signify that the endpoint is not brewing coffee today (see https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/418).
4. Extra: When the endpoint `GET /brew-coffee` is called, the endpoint should check a third-party weather service (e.g. https://openweathermap.org/api), and if the current temperature is greater than 30°C, the returned message should be changed to `"Your refreshing iced coffee is ready"`.

## Solution design
There are two project in this solution, one for the API, another one for testing things in the API project.

## Design patterns and techs used
- Dependency injection: modular classes, easy to test.
- Typed HttpClient: reused HttpMessageHandler, easy to naming and configuring HttpClient.

## How to start the API
You can run this project by Visual Studio 2022 or .NET CLI tool.

For Visual Studio 2022, all you have to do is open solution, select Api project from list and hit Run button.

For dotnet cli, change directory into solution root folder, and run following command: `dotnet run --project Api`