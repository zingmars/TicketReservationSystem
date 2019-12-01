Ticket reservation system
===

Homework for DIP485(1),19/20-R (Programmatūras metroloģijas un plānošanas modeļi).

## Requirements

* [Dotnet core 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/). If using Visual Studio Code, install the C# plugin. Note that this will only work with the official MS build of VS Code (Meaning that custom builds like VS Codium won't work).
* Dotnet EF - install by running `dotnet tool install --global dotnet-ef --version 3.0.0-*` in your shell

## Setup instructions

In project directory:

* `dotnet restore`
* `dotnet ef database update`

### Default credentials

After migrations are applied, you can log in with "admin@admin.com / Password!123" and change the default admin password.

## Running instructions

Using visual studio code:

* Open the directory using VS Code (File -> Open Folder, or in Explorer (this needs to be enabled when installing VS Code) right click the folder and press "Open with Code").
* Press f5 (default) or Debug -> Start Debugging to run the app in debug mode.

## Commands for scaffolding

Scaffolding entity type classes and a DbContext class based on a database schema:

`dotnet ef dbcontext scaffold "Data Source=database.db" Microsoft.EntityFrameworkCore.Sqlite --table BusinessHours --table Categories --table Performances --table PerformanceCategories --table Theatres --table PerformanceDates --table PurchaseMethods --table AspNetUsers --table Purchases --context ApplicationDbContext --context-dir Data --output-dir Models --force`

[Detailed documentation](https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding)

:warning: **If you reverse engineer the model from the database again, any changes you've made to the files will be lost.** :warning:
