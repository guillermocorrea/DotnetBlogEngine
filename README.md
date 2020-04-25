# DotnetBlogEngine

Simple blog engine in dotnet core 3.1.

![Class diagram](/docs/ClassDiagram.png 'Entity model')

## Web App

Go to [https://blogengineapp.azurewebsites.net/].

## REST Api

Link to the swagger documentation [https://blogengineapp.azurewebsites.net/swagger].

It uses JWT Bearer token for authorization, use the endpoint `POST /api/auth/login` to get the access token.

## Users

| username | password |   Role |
| -------- | :------: | -----: |
| writer   |  writer  | Writer |
| editor   |  editor  | Editor |

## Requirements

- [.NET Core 3.1](https://dotnet.microsoft.com/download)
- [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)

## Set up the database

Create a new database `BlogEngineDB` in SQL Server Express LocalDB and run the script `BlogEngineDB.sql` or just run the EF migration in the `src/Infrastructure` project to create the database.

## Build the app

Execute `dotnet build`

## Run the app

Execute `dotnet run -p src/WebUI/WebUI.csproj` or open in Visual Studio and run from there.

## Run the unit tests

Execute `dotnet test`
