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
13. Add ``LoginDTO`` class
14. Add ``Login`` method in ``UserController``.
15. Install ``Microsoft.AspNetCore.Authentication.JwtBearer`` nuget package.
16. Add ``JWTConfig`` object in ``appsettings.json`` file.
17. Add ``AddAuthentication`` service in ``ConfigureServices`` method in ``Startup.cs`` class.
18. Create ``JWTConfig.cs`` class that matches the name and properties from ``JWTConfig`` object in ``appsettings.json`` file.
19. Map ``JWTConfig.cs`` class and ``JWTConfig`` JSON object from ``appsettings.json`` file in ``Startup.cs`` class.
20. Add ``app.UseAuthentication();`` middleware in ``Configure`` method in ``Startup.cs`` class.
21. Inject ``JWTConfig`` class in ``UserController``
22. Add ``GenerateToken`` method in ``UserController``.
23. Add ``Token`` Property in ``UserDTO`` class.
24. Modify ``Login`` method in ``UserController`` for returning user.
25. Create ``Angular`` project with bootstrap simple navbar.
26. Create a login component ``ng g c login --skip-tests``
27. Add login route to ``app-routing.module.ts`` 
28. Design Login forms
29. Import ``FormsModule``, ``ReactiveFormsModule``, ``HttpClientModule`` in ``app.module.ts``.
30. Make login form semi-functional with ``ReactiveForm``.
31. Create a register component ``ng g c register --skip-tests``
32. Add register route to ``app-routing.module.ts`` 
33. Design register forms
34. Make register form semi-functional with ``ReactiveForm``.
35. Add ``Login`` and ``Register`` button in the nav bar with ``routerLink`` attribute.
36. Create a user service ``ng g s user --skip-tests``
37. Add ``login`` and ``register`` methods in user service.
38. Enable CROS in ``Startup.cs`` by adding ``AddCors`` service in ``ConfigureServices`` method and ``app.UseCors();`` middleware in ``Configure`` method.
39. In register component modify ``onSubmit()`` method with user service.
40. Create a custom response model class ``ResponseModel.cs`` with ``Code``, ``Message``, ``DataSet`` properties.
41. In user controller, return the ``ResponsModel`` in every methods.
42. In login component modify ``onSubmit()`` method with user service.
43. Create a user management component ``ng g c user-management --skip-tests`` with bootstrap table.
44. Add user management route to ``app-routing.module.ts`` 
45. Add user management routing in the nav bar.
46. Create a ``user.ts`` and ``response-model.ts`` class.
47. Add ``ResponseModel`` return type to all methods in user service class.
48. Modify login component's ``onSubmit`` method with ``localStorage`` and ``router``.
49. Add ``getAllUser`` method in user service with header.
50. In user-management component call the ``getAllUser`` method to get the user list and bind the list to the bootstrap table in the html file.
51. Add a ``Logout`` button in the nav bar and clear the ``localStorage`` and go to ``Login`` when the ``Logout`` button is clicked.
52. Hide ``Register``, ``Logout`` and ``User Management`` button from the nav bar when user is logged out.
53. Create a ``constants.ts`` helper class to hold the project's constants value. Right now it only holds ``userInfo``. 
54. Replace the ``userInfo`` strings with ``USER_KEY`` constants from ``constants.ts`` helper class.
55. Create a auth guard service ``ng g s auth-guard --skip-tests`` and implements ``CanActivate``. This service prevents user to access any route that he/she is not authorized.
56. Use the auth guard service in the routing module.
57. Add JWT as default authentication scheme at ``AddAuthentication`` method in the ``Startup.cs`` file 
58. Change ``[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]`` to ``[Authorize()]`` in user controller.
59. Add ``RoleDTO`` class.
60. Inject ``RoleManager`` class in ``UserController``
61. Add ``AddRole`` and ``GetAllRoles`` methods in ``UserController``.
62. Add ``Role`` property in ``RegisterDTO``.
63. Modify ``RegisterUser`` method in user controller class for register user with a role.
64. Add ``Role`` property in UserDTO class.
65. Modify ``GetAllUser`` and ``Login`` methods in the ``usercontroller`` to return user with role. (Single role).
66. Create ``GetUsers`` method in ``usercontroller`` to return only users.