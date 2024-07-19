# Companies House Microservice

# About
The Companies House Microservice, implemented as a RESTful API, is a service to query company details from the Companies House RESTful API.

# Installation guide
### System Requirements
- IDE capable of running .NET 6.0 or above i.e. Visual Studio

### Prerequisites
- API key for Companies House RESTful API

### Installation instructions
1. Clone the repository to your local machine.

2. Open the 'IPO.Company.sln' solution file in Visual Studio.

3. Update the APIKey value in the 'appsettings.json' file with your personal key.

4. Build the solution.

5. Set the Web API (IPO.Company.API) as the Startup project in Visual Studio and run in debug configuration.

7. A command window will launch, in which you will see the Console output.

8. The swagger page will launch in your default browser ready to test the endpoints.

9. Validate any responses via the [Companies House web-based search](https://find-and-update.company-information.service.gov.uk/).

# Usage Instructions
   *baseUrl* - The base URL is dependant on your setup.

   **Note:** All endpoints with Full Request and Response contracts can be viewed in the Swagger documentation.

### Service Endpoints

1. GET request - /

    This endpoint takes the CompanyNumber and formats a request to the Companies House RESTful API. The standard response is then returned, unless there is an error with the API or Request.
       
    Example URL - {baseUrl}/?CompanyNumber=12345678

    Example GET Results
    ```JSON
    {
        "companyName": "IPO Office",
        "companyNumber": "12345678",
        "dateOfCreation": "2022-11-09",
        "addressLine1": "Concept House",
        "addressLine2": "Cardiff Road",
        "country": "Wales",
        "locality": "Newport",
        "postOfficeBox": null,
        "postCode": "NP10 8QQ",
        "premises": null,
        "region": null,
        "isActive": true,
        "registeredOfficeIsInDispute": false,
        "undeliverableRegisteredOfficeAddress": false
    }
    ```

### System Endpoints
All of these endpoint paths assume that {baseurl} corresponds to a fully qualified url pointing to the location of the microservice.

1. GET request - version

    Use this end point to return the version of the microservice api.
    
    Example URL - {baseUrl}/version
2. GET request - health

    Use this end point to return the health status of the microservice api.
    
    Example URL - {baseUrl}/health
3. GET request - error list

    Use this end point to return a JSON array containing a list of all the possible error codes and descriptions that the microservice could encounter and return.
    
    Example URL - {baseUrl}/error/list

    Example Get Id Error List Results
    ```JSON
    [
        {
            "code": "E000",
            "description": "Internal Error"
        },
        {
            "code": "E001",
            "description": "Validation Error"
        },
        {
            "code": "E002",
            "description": "The CompanyManagementService encountered an error."
        },
        {
            "code": "E003",
            "description": "The CompaniesHouseGateway encountered an error."
        }
    ]
    ```


# Licence
Unless stated otherwise, the codebase is released under [License](./License.md). This covers both the codebase and any sample code in the documentation.

The documentation is © Crown copyright and available under the terms of the Open Government 3.0 licence.