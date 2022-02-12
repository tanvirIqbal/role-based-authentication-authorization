# role-based-authentication-authorization
Role Based Authentication Authorization with .Net 5 and Angular 13

## Nuget Pakages for Role Based Authentication and Authorization

1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Design
3. Microsoft.EntityFrameworkCore.SqlServer/Microsoft.EntityFrameworkCore.Sqlite
4. Microsoft.AspNetCore.Identity.EntityFrameworkCore

Install entity framework 'ef' tools globally  ``dotnet tool install --global dotnet-ef``  

## Steps after project creation:  

1. Install 4 Nuget Packages for EF Core and MS Identity
2. Add a connection string in ``appsettings.json``
3. Add ``AppUser`` class inherited from ``IdentityUser`` with necessary properties
4. Add ``AppDBContext`` class inherited from ``IdentityDbContext<AppUser, IdentityRole, string>`` with constractor ``AppDBContext(DbContextOptions options) : base(options)``
5. Add ``AddDbContext`` and ``AddIdentity`` services in ``ConfigureServices`` method in ``Startup.cs`` class.
6. ``dotnet ef migrations add InitialMigration -o "Data/Migrations"``
7. ``dotnet ef database update``
8. Create a ``UserController`` with ``UserManager`` and ``SignInManager`` injected.
9. Add ``RegisterDTO`` class
10. Add ``RegisterUser`` method in ``UserController``.
11. Add ``UserDTO`` class
12. Add ``GetAllUser`` method in ``UserController``.

