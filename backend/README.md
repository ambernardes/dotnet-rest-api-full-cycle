# Equipment Management App

# Description:

A simple web application for managing a list of equipment, demonstrating skills in .NET Framework, C#, ASP.NET Core, and Vue.

# Technologies Used:

To create this application, the following technologies were used:

- [.netcore](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

# Setup Instructions

If you haven't already installed the .NET 8 SDK, you can download it from the [.NET download page](https://dotnet.microsoft.com/download).

If .NET 8 SDK is properly installed and configured, it is possible to build and test the application by running the `dotnet` script. To do this, run the commands in the root directory:

### Project Setup

```sh
cd EquipmentManagementApp/
```

### Restore Dependencies

```sh
dotnet restore
```

### Build the Project

```sh
dotnet build
```

### Run the Project

```sh
dotnet run
```

### Run Unit Tests

In the root directory:

```sh
dotnet test
```

The application uses dotnet core. And it was developed using 8.0.

# Implementation

The approach used to create this application consists of generating a template `ASP.NET Core Web Api` application using `Visual Studio 2022` and then implementing CRUD actions.

The records created are stored in an in-memory database and are deleted whenever the application is restarted.

The test cases were implemented using `xunit`.

# Endpoints

Once the application is running, there will be two endpoints:

- GET /Equipment - used to check complete list of registered equipments. Empty Array if no item has been registered.

- GET /Equipment/{id} - retrieves the equipment with the corresponding id. 404 if there is no corresponding id.

- POST /Equipment/ - send a dto to create the equipment. in case of success returns the created object.

- PUT /Equipment/{id} - sends a dto with the corresponding id to update the equipment.

- DELETE /Equipment/{id} - deletes the equipment with corresponding id.

The REST API server will run in [localhost:5062](http://localhost:5062/), with [Swagger enabled](http://localhost:5062/swagger/index.html).
