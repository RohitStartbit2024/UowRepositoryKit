# 🧩 UowRepositoryKit

**Lightweight .NET 9 library that provides a clean Unit of Work and Repository pattern for EF Core with async CRUD and easy DI setup.**

---

## 🚀 Overview

UowRepositoryKit is a reusable .NET 9 package designed to simplify data access in Web API and MVC applications.  
It implements a **generic Repository** and **Unit of Work** pattern that automatically handles all EF Core entities — without needing to register repositories manually.

### ✨ Features
- Generic repository for all DbContext entities
- Centralized Unit of Work to manage multiple repositories
- Async CRUD support
- Automatic dependency injection setup
- Compatible with Entity Framework Core 9
- Works seamlessly in .NET 9 WebAPI or MVC projects

---

## 📦 Installation

### From NuGet.org
```bash
dotnet add package UowRepositoryKit
```

### From Local Source
If you built the package locally, add it using:
```bash
dotnet add package UowRepositoryKit --source "C:\Path\To\bin\Release"
```

---

## 📁 Project Structure

```
UowRepositoryKit/
│
├── Interfaces/
│   ├── IRepository.cs
│   └── IUnitOfWork.cs
│
├── Repositories/
│   └── GenericRepository.cs
│
├── Uow/
│   └── UnitOfWork.cs
│
├── Extensions/
│   └── ServiceCollectionExtensions.cs
│
└── UowRepositoryKit.csproj
```

---

## 🧰 How It Works

- The **Generic Repository** provides a common CRUD interface for any entity in your `DbContext`.  
- The **Unit of Work** coordinates multiple repositories and ensures all changes are saved together through a single `SaveChangesAsync()` call.  
- The **ServiceCollection Extension** lets you add the entire setup with a single line of configuration.

No manual registration is required for new entities — once added to the `DbContext`, they are automatically supported.

---

## 🧱 How to Create the Package

1. Open the project in Visual Studio 2022.  
2. Make sure the `.csproj` has:
   - `<GeneratePackageOnBuild>true</GeneratePackageOnBuild>`  
   - Proper metadata (PackageId, Version, Authors, etc.)  
3. Build the project in **Release** mode.  
4. The NuGet package (`.nupkg`) will be generated at:
   ```
   bin\Release\UowRepositoryKit.1.0.0.nupkg
   ```

---

## ⚙️ How to Use

1. **Install the package** in your WebAPI project:
   ```bash
   dotnet add package UowRepositoryKit
   ```
2. **Register it** in your `Program.cs`:
   ```csharp
   builder.Services.AddUowRepositoryKit<MyDbContext>();
   ```
3. **Inject `IUnitOfWork`** into your controllers or services.  
   Use `uow.Repository<TEntity>()` to access any entity repository, and call `uow.SaveChangesAsync()` to persist changes.

No additional setup required — works for all `DbSet<>` in your DbContext automatically.

---

## ⚙️ EF Core Migrations and `DesignTimeDbContextFactory`

When using Entity Framework Core migrations, EF sometimes needs a way to **create your `DbContext` at design time** (not at runtime).  
In most modern .NET apps, it’s **optional** — EF automatically uses your `Program.cs` configuration.

If you encounter an error like:
```
Unable to create an object of type 'AppDbContext'
```
you can fix it by adding a small factory class:

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MyProject.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
```
---

## 🧪 Example Usage

- Fetch all records of an entity  
- Add, update, or delete entities asynchronously  
- Use Unit of Work to coordinate changes across multiple repositories  
- Call `SaveChangesAsync()` once to commit all operations

---

## 🧩 Summary

| Feature | Description |
|----------|-------------|
| Framework | .NET 9 |
| ORM | Entity Framework Core 9 |
| Pattern | Unit of Work + Generic Repository |
| Setup | Single-line DI Registration |
| License | MIT |

---

**UowRepositoryKit** — Simplifying EF Core data access with clean, maintainable architecture for .NET 9 projects.
