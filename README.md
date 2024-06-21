# FullStackWebTest #

This is a Web project to test my fullstack capabilities.

## Table of Contents
- [Tecnologies](#tecnologies)
- [Setup of the Project](#setup)
- [Using Docker](#docker)
- [Local Setup](#local)
- [Developer Testing](#testing)
- [Use of the system](#use)

## Tecnologies used <a name="tecnologies"></a>

* ASP.NET Core 8.0
* Entity Framework Core
* Fluent Validation
* MediaTr
* Material
* Angular
* SqlServer

The main programming language is C-Sharp 9.0, and typescript 5.5.2 for the front end.

## Setup <a name="setup"></a>

Theres a SqlServer script for the Database in the main folder.
The default data is seeded in there for faster setup of the app.
You can run the system in a virtual enviroment by easily using Docker compose, or setup it locally.

## Execute Docker Compose <a name="docker"></a>
1. Open up a new terminal window in the main folder
2. Execute the next command in terminal window

```sh
docker-compose up -d
```

3. Once done, in same terminal window execute:

```sh
docker container ls
```

4. and make sure that all 3 containers are running, they should have __fullstackdevtest__ as a prefix.
This will initialize the frontend, the backend and the database.

5. Open in the browser the port

```sh
https://client-app.fullstackdevtest.orb.local
```

## Preparing Environment to Run Local<a name="local"></a>
### Computer Requirements
 - At least 8gb of available RAM
 - [Node.js v20.13.1+](https://nodejs.org/en/download/) and [Package Manager Instructions](https://nodejs.org/en/download/package-manager/)
 - [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
 - [dotnet-ef tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) (CLI TOOL)
 - [docker](https://docs.docker.com/get-docker/) (CLI TOOL)
 - [docker-compose](https://docs.docker.com/compose/install/) (CLI TOOL)

### Step 1 - Change Connection Strings if needed
1. Open `WebApi` project folder.
2. Open `AppSettings.json`.
3. Change the credentials to your liking.

### Step 2 - Apply the migrations
1. You can either run the WebApi and automatically create the database. Or you can:
2. Open the terminal in the main folder and execute:

```sh
dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project WebApi/WebApi.csproj --context Infrastructure.Persistence.AppDbContext --configuration Debug 20240621132508_addedNewDataSeed
```

### Step 3 - Run the Web Api
1. Launch WebApi from IDE, or you can:
2. Open the terminal in the WebApi folder and restore the dependencies by executing:

```sh
dotnet restore
```

3. Then run the backend:

```sh
dotnet run
```

### Step 4 - Run the Client App
1. Open the terminal on the Client App folder.
2. Update the required dependencies, by executing the next command:

```sh
npm i --f
```

3. Build the app.

```sh
ng build
```

4. Launch the app.

```sh
npm run start
```

5. Open the browser in the next url:

```sh
localhost:4200
```

## Automated Testing <a name="testing"></a>
1. Framework `XUnit`
2. **Unit tests** projects naming convention: `<Production_Project_Name>.Facts`
    * If we have a project called `Domain` its corresponding test project must be called `Domain.Facts` 
    * To run unit tests only use in the main folder:
    
`dotnet test /p:CollectCoverage=true --filter FullyQualifiedName~Wepsys.PoluxBPM.Core.Facts`

3. Test projects location: `tests/`

## Use of the system <a name="use"></a>

1. You need to acces into the system by entering your user credentials.
2. You can register in the register page or use the default system User:
  * User: `Admin`
  * Password: `12345678`
3. The first main page is directly the products management, where you can:
  * Create products
  * Modify existing products
  * Delete products
  * And search products.
4. You can do the page navigation by clicking the menu items in the header of the page.
5. To go to the User manager, your user needs to be admin, or the menu item wont show.
6. It has the same management capabilities as user management.

