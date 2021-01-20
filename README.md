# Challenge Rest API

The Challenge API is a open-source project written in .NET Core

The goal of this project is implement the most common used technologies with the technica community the best way to develop the Web Rest API with .NET Core

# Technologies implemented:

- ASP.NET Core 3.1 (with .NET Core 3.1)
- ASP.NET WebApi Core
- Entity Framework Core 3.1
- SQL Server
- AutoMapper
- Flunt
- FluentValidator
- Swagger UI

# Architecture:

- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Repository and Generic Repository
- Dependency Injection

# How to use:

You will need the latest Visual Code and the latest .NET Core SDK. Tools can be downloaded from https://dotnet.microsoft.com/download. Also you can run the WebApi in Visual Code (Windows, Linux or MacOS).

- You must create the database named AeroAPI
- Example for compiling the web api project: D:\dev\AeroAPI\Passageiro dotnet build
- Example for compiling the web api project: D:\dev\AeroAPI\Passageiro\Passageiro.WebApi dotnet run

When compiling the project, open it in the browser using the https://localhost:5001/swagger/index.html link where it will display all the end points and ViewModels needed to run the API.
