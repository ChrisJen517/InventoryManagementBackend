# ASP.NET Core 10 Web API

A modern ASP.NET Core 10 Web API built with Controllers, Entity Framework Core, ASP.NET Core Identity, Cookie Authentication, and Role-Based Authorization.

## Features

- ASP.NET Core 10 Web API
- Controller-based API architecture
- Entity Framework Core
- ASP.NET Core Identity
- Cookie Authentication
- Role-Based Authorization
- Dependency Injection
- Configuration using `appsettings.json`
- Swagger/OpenAPI support
- Database migrations with Entity Framework Core

---

## Technologies

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- MySQL Server
- ASP.NET Core Identity
- Cookie Authentication
- Role-Based Authorization
- Swagger / OpenAPI

---

## Project Structure

```
├── Controllers/
│   ├── CategoriesController.cs
│   ├── LocationsController.cs
│   ├── ProductsController.cs
│   ├── UsersController.cs
│   └── VendorsController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── CustomUserClaimsPrincipalFactory.cs
│   └── Migrations/
├── Models/
│   ├── Category.cs
│   ├── Location.cs
│   ├── Product.cs
│   ├── Shipment.cs
│   └── Vendor.cs
├── wwwroot/
├── Program.cs
├── appsettings.json
└── README.md
```

---

## Prerequisites

Before running the project, ensure you have:

- .NET 10 SDK
- MySQL Server
- Visual Studio 2025 or Visual Studio Code

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/ChrisJen517/InventoryManagementBackend .
```

### 2. Configure the Database

Update the connection string in **appsettings.json**.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(Server);Database=(Database);User=(User);Password=(Password);"
  }
}
```

---

### 3. Apply Entity Framework Migrations

```bash
dotnet ef database update
```

If migrations do not exist:

```bash
dotnet ef migrations add InitialCreate

dotnet ef database update
```

---

### 4. Run the Application

```bash
dotnet run
```

The API will typically be available at:

```
https://localhost:5001
http://localhost:5000
```

Another option to run the Application

```bash
dotnet watch --launch-profile https
```

This option allows for hot reload and will typically be available at:

```
https://localhost:7213
```

---

## Authentication

The application uses **ASP.NET Core Identity** with **Cookie Authentication**.

Users authenticate using Identity, and the authentication cookie is automatically issued upon successful login.

Example login flow:

1. Register a user
2. Login
3. Receive authentication cookie
4. Include the cookie on future requests

---

## Authorization

Role-based authorization is implemented using ASP.NET Core Identity Roles.

Example:

```csharp
[Authorize(Roles = "Administrator")]
public IActionResult AdminOnly()
{
    return Ok();
}
```

Multiple roles:

```csharp
[Authorize(Roles = "Administrator,Vendor")]
```

Authenticated users:

```csharp
[Authorize]
```

Anonymous access:

```csharp
[AllowAnonymous]
```

---

## Identity

Identity provides:

- User Registration
- User Login
- Password Hashing
- Password Validation
- User Roles
- User Claims
- Cookie Authentication
- Account Lockout
- Email Confirmation (optional)
- Password Reset (optional)

---

## Entity Framework Core

Entity Framework Core is used as the ORM.

Typical workflow:

Create a migration:

```bash
dotnet ef migrations add MigrationName
```

Update the database:

```bash
dotnet ef database update
```

Remove the last migration:

```bash
dotnet ef migrations remove
```

---

## API Documentation

Swagger is enabled during development.

After starting the application, browse to:

```
https://localhost:<port>/swagger
```

---

## Configuration

Common configuration options are located in:

```
appsettings.json
```

Typical settings include:

- Connection Strings
- Logging
- Identity Options
- Cookie Settings
- CORS
- Authentication

---

## Cookie Authentication

Cookie Authentication is configured using ASP.NET Core Authentication Middleware.

Typical cookie settings include:

- Cookie Name
- Expiration
- Sliding Expiration
- Secure Policy
- SameSite Policy
- Login Path
- Logout Path
- Access Denied Path

---

## Dependency Injection

Services are registered using the built-in dependency injection container.

Example:

```csharp
builder.Services.AddScoped<IUserService, UserService>();
```

---

## Common Commands

Restore packages:

```bash
dotnet restore
```

Build:

```bash
dotnet build
```

Run:

```bash
dotnet run
```

Watch:

```bash
dotnet watch
```

Publish:

```bash
dotnet publish -c Release
```

---

## Security

This project follows common ASP.NET Core security practices:

- Password hashing with ASP.NET Core Identity
- Secure authentication cookies
- Role-based authorization
- HTTPS support
- Authorization policies
- Secure password requirements

---

## License

This project is licensed under the MIT License.

---

## Author

Created using ASP.NET Core 10 and Entity Framework Core.