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

After migrations are applied, log in with "admin@admin.com / Password!123" and change the default admin password.

## Miscellaneous

* To generate pages from models - `dotnet tool install --global dotnet-aspnet-codegenerator`
