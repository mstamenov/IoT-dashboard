# Project README

## Overview

This project is a WebApi that utilizes IoT data to display information in a user-friendly way.
For a front end is used Angular 15

## Getting Started

1. Clone this repository
2. Open the solution in Visual Studio or Visual Studio Code
3. Install the required dependencies with `npm install` command
3. Build angular Dashboard project with `ng build` or `npm run build`. This will output data to wwwroot folder of WebApi project
4. Set the WebApi project as the startup project and run it or use `dotnet run ./WebApi/WebApi.csproj` command
5. Every commit to master branch will trigger a build and deploy to Azure

### Prerequisites

- .NET Core 7
- NodeJs 10.15.3

## Usage

Open the WebApi project and navigate to the specified endpoints to view and interact with the IoT data.

## AppSettings

The following AppSettings values are available for customization:

- `TokenKey` - A unique token key used for authentication.
- `IoTContext` - Connection string to the database.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)
