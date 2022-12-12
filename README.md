<h1 align="center">Roomed</h1>
<h3 align="center">Hotel management system</h3>

## 📒 Description

[Roomed](https://github.com/Sirmov/Roomed) is my project defense for the [ASP.NET Advanced](https://softuni.bg/trainings/3854/asp-net-advanced-october-2022) course [@Softuni](https://softuni.bg/). The course is the last part of the [C# web](https://softuni.bg/modules/108/csharp-web/1365) module. It was inspired by my summer job as a hotel receptionist.

The software aims to help companies manage the whole accommodation process from making a reservation and checking in to managing bills and checking out.

This is my first real [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-7.0) application and it has taught me a lot and continuos to do so. I have a lot of future plans for the application and therefor it has a lot of missing features. I plan to maintain and improve this project.

## 🌐Technologies

### 👨‍💻 Backend

-   ASP.NET Core 6.0.10
-   ASP.NET Core Identity 6.0.10
-   Entity Framework Core 6.0.10
-   Newtonsoft Json 13.0.2
-   AutoMapper 12.0.0
-   Html Sanitizer 8.0.601

### 🎴 Frontend

-   Bootstrap 4.6.2
-   Bootstrap Select 1.13.18
-   Font Awesome 6.2.0
-   JQuery 3.6.1

### 🗄️ Database

-   Microsoft SQL Server 2022 Developer Edition

### 🧪 Tests

-   NUnit 3.13.3
-   NUnit3 TestAdapter 4.3.1
-   NUnit Analyzers 3.5.0
-   Microsoft.NET TestSdk 17.4.0
-   Microsoft EntityFramework Core InMemory 6.0.10
-   Moq 4.8.13
-   coverlet collector 3.2.0

## 🧱 Architecture

The application follows the standard [MVC architecture](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-7.0#mvc-pattern) including some key principles and patterns like:

-   [Service layer pattern](https://en.wikipedia.org/wiki/Service_layer_pattern)
-   [Repository pattern]()
-   [Inversion of control principle](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
-   [Unit of work pattern](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application) and other.

<div align="center">
<p align="center"><img src="https://github.com/Sirmov/Roomed/blob/main/assets/images/database-schema-1.png"></p>
<small>Figure 1. Frameworks specific database tables schema</small>
</div>  
<br/>
<div align="center">
<p align="center"><img src="https://github.com/Sirmov/Roomed/blob/main/assets/images/database-schema-2.png"></p>
<small>Figure 2. Application specific database tables schema</small>
</div>

## 🎿 Installing the project

You can install the project in three different ways.

### 1. Cloninig the repository

-   Open Git Bash.
-   Change the current working directory to the location where you want the cloned project.
-   Type `git clone https://github.com/Sirmov/Mimega` and press enter.

### 2. Downloading the repository

-   Go to the [root](https://github.com/Sirmov/Mimega) of the repository.
-   Click the green code button.
-   Click download zip.
    <img width="50%" src="https://docs.github.com/assets/cb-20363/images/help/repository/code-button.png">

### 3. Using a [Git GUI client](https://git-scm.com/downloads/guis)

## ⌨️🖱️ Usage

To do...

## 📑 License

The project is licensed under the [GNU GPL v3](https://github.com/Sirmov/Roomed/blob/main/LICENSE) license.
