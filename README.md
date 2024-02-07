# Library App

## A library style web application using ASP.NET Core, EF Core, and Razor to perform CRUD functions
---
I built a basic ASP.NET Core web application that mimics something similar to what a library may use to hold data on:
  1. Authors (their info and works),
  2. Borrowers (their info, books borrowed, when they were borrowed, and when they were returned),
  3. Finally Books (basic book info, list of Authors associated, and list of Borrowers as well as borrowed and returned dates)
     
I initially wrote this as an all-in-one project but then rebuilt it in N-tier/clean style architecture for better separation of concerns.

## Installation
---
This is still a work in progress and given it's a web app that has not been deployed, you'll need to download the repo files. 
To run the application you'll need:
  1. A database. Specifically, you'll need to set up the database and migrate then seed the data I used the developer version of <a href="https://www.microsoft.com/en-us/sql-server/sql-server-downloads">Microsoft SQL Server 2019</a> and <a href="https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms">SSMS</a> because they're free and (I believe) the easiest to integrate given the use of EntityFramework.
  2. <a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0">.NET 8 SDK and ASP.NET Core Runtime</a> (see project files for version if latest isn't working)
  3. An IDE capable of running this project successfully. I've tested in/successfully used: Visual Studio, Visual Studio Code, and JetBrains Rider. All have worked successfully.
  4. EF-Core tools. I believe most IDEs will prompt you to install these automatically, otherwise, the command is ```"C:\Program Files\dotnet\dotnet.exe" tool install --ignore-failed-sources --add-source https://api.nuget.org/v3/index.json --global dotnet-ef```

## Running The Project
--- 
1. Create your database in SSMS, then grab the connection string.
2. Put it into the "DefaultConnection: " in src/CRUDLibrary.Web/appsettings.json where mine is currently.
3. In your IDE using .NET Core CLI, make sure you're in the src directory, then run:<br/>
         ```dotnet ef migrations add InitialCreate --project CRUDLibrary.Data --context LIBDBContext --startup-project CRUDLibrary.Web```
4. If there are no errors run:<br/>
         ```dotnet ef database update --project CRUDLibrary.Data --context LIBDBContext --startup-project CRUDLibrary.Web```
5. If your database migration and update are successful, seed the database by moving into the CRUDLibrary.Web directory and running:<br/>
         ```dotnet run seeddata```

## Usage
---


## Credit
---
N/A

## License(s)
---
N/A
