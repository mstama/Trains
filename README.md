# Trains

Trains

[![Build status](https://ci.appveyor.com/api/projects/status/mlp1dcqkymkkpd7w?svg=true)](https://ci.appveyor.com/project/mstama/trains)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/0178af2880644d9ab370d9357010a933)](https://www.codacy.com/app/marcostamashiro/Trains?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=mstama/Trains&amp;utm_campaign=Badge_Grade)

### How to build

* [Install](https://www.microsoft.com/net/download/core#/current) .NET Core 1.1 
* Restore the packages **(required once)**. In the solution folder, where **Trains.sln** is, folder execute the followin command:

```
dotnet restore
```

* In the project folder, where **Trains.csproj** is, execute the following command: 

```
dotnet build -c release
```
### How to run

* Running using the project file. In the project folder, where the **Trains.csproj** is, execute the following command: 

```
dotnet run input.txt
```

* Running using the binary. Execute the following command with the binary file:

```
dotnet Trains.dll input.txt
```
### How to test

* Executing unit tests. In the unit tests project folder, where the **UnitTests.csproj** is, execute the following

```
dotnet test
```

### Architecture

The solution is composed of:

* Models
    * Town        : Information about a town.
    * Route       : Information about a route between towns.
    * Graph       : Information about towns and routes.
* Services
    * GraphParser : Responsible for reading the input file to create the graph.
    * GraphFactory: Responsible for creating the appropriate graph.
    * GraphWalker : Responsible to traverse the graph to perfom the calculation.
* Interfaces      : Services are implemented using interfaces to be extensible.

The main program is composed of:

* Composition Root: where all modules are put together.
* Execution of the graph parser.
* Execution of the graph walker to answer each one of the questions proposed.

Highlights:
* Extensibility: main modules are provided with an interface so it can be replaced in the composition root for evolution.

[GitHub Project Repository](https://github.com/mstama/Trains)
