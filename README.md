# CPW219-eCommerceSite

This is an E-commerce website built with ASP.NET MVC for Joe's Gaming, a store specializing in video games. It allows users to browse a selection of new and vintage games, consoles, and accessories.

## Prerequisites

To run this project locally, you need to have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore/)
- [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)

## Setting Up the Project

1. **Clone the Repository:**
   - Go to the GitHub repository: `https://github.com/yourusername/joes-gaming`
   - Click on the green "Code" button and copy the repository URL.
   - Open Visual Studio 2022.
   - Go to `File > Clone Repository`.
   - Paste the repository URL and click `Clone`.

2. **Restore Dependencies:**
   - Open the `Joe's Gaming` solution in Visual Studio.
   - In the `Solution Explorer`, right-click on the solution and select `Restore NuGet Packages`.

3. **Add Required Packages:**
   - In the `Solution Explorer`, right-click on the project and select `Manage NuGet Packages`.
   - Search for and install the following packages:
     - [Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore/)
     - [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)

4. **Update the Database:**
   - Ensure that SQL Server is running.
   - Open the `Package Manager Console` in Visual Studio by going to `Tools > NuGet Package Manager > Package Manager Console`.
   - In the `Package Manager Console`, type `Update-Database` and press `Enter`. This will apply the latest migrations to the database.

5. **Run the Application:**
   - In Visual Studio, set the startup project to the main web application project.
   - Press `F5` or click the `Start` button to run the application.
   - The application should now open in your default web browser.

## Useful Resources

Here are some links to useful resources for the project:

- [ASP.NET MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-8.0)
- [CRUD Functionality in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-8.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Bootstrap 5](https://getbootstrap.com/docs/5.0/getting-started/introduction/)
