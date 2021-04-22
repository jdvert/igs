# IGS Market

## Description
This is a simple API which provides CRUD functionality for a product-based application. The default setup for this application runs on a preconfigured Docker instance and points to a secondary Docker instance running MSSQL for it's data storage.

By default the application will drop and recreate it's database each time it is restarted. This is to aid in the development process and to allow tests to be re-run against the same Docker instance but should not be used in a production environment.

## Dependencies
- [Docker Desktop](https://docs.docker.com/docker-for-windows/install/)
- [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019)

## Usage
1. Clone the repository to your local PC.
2. Open the IgsMarket solution file (IgsMarket.sln) in Visual Studio.
3. Ensure that the `docker-compose` project is set as the startup project.
4. Run the application from Visual Studio. This will open a web browser with the Swagger definition for the API.
5. You can now call the API or run tests against it using Postman.
