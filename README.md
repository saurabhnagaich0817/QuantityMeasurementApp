# Quantity Measurement Application

[![.NET Version](https://img.shields.io/badge/.NET-8.0+-blue.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![Test Coverage](https://img.shields.io/badge/coverage-comprehensive-blue.svg)]()

A robust, enterprise-grade REST API application for managing and converting various units of measurement including length, weight, volume, and temperature. Built with a clean layered architecture, comprehensive security, and extensive unit testing.

---

## 📋 Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Configuration](#configuration)
- [API Documentation](#api-documentation)
- [Usage Examples](#usage-examples)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [Security](#security)
- [Contributing](#contributing)
- [Troubleshooting](#troubleshooting)
- [License](#license)
- [Support](#support)

---

## ✨ Features

### Core Functionality
- **Multi-Unit Measurement Support**
  - Length conversions (meters, kilometers, feet, miles, etc.)
  - Weight conversions (kilograms, grams, pounds, ounces, etc.)
  - Volume conversions (liters, milliliters, gallons, etc.)
  - Temperature conversions (Celsius, Fahrenheit, Kelvin)

- **Advanced Operations**
  - Compare quantities with automatic unit conversion
  - Perform arithmetic operations (addition, subtraction, division)
  - History tracking of user operations
  - Memory-cached query results for performance optimization

### Security Features
- **JWT-based Authentication & Authorization**
- **Role-based Access Control (RBAC)**
- **Secure token generation and validation**
- **HTTPS support**
- **CORS configuration**

### Developer Experience
- **Swagger/OpenAPI integration** for interactive API documentation
- **Comprehensive XML documentation**
- **Structured error handling**
- **Request/Response logging**
- **Unit test coverage**

---

## 🏗️ Architecture

The application follows a **clean layered architecture** pattern:

```
┌─────────────────────────────────┐
│   API Layer (REST Controllers)  │
├─────────────────────────────────┤
│   Business Logic Layer          │
│   (Services & Converters)       │
├─────────────────────────────────┤
│   Model Layer                   │
│   (DTOs, Entities, Enums)       │
├─────────────────────────────────┤
│   Repository Layer              │
│   (Data Access & EF Core)       │
├─────────────────────────────────┤
│   SQL Server Database           │
└─────────────────────────────────┘
```

### Layer Responsibilities

| Layer | Purpose | Components |
|-------|---------|-----------|
| **API Layer** | HTTP request handling, routing, authentication | Controllers, Middleware, Swagger Config |
| **Business Layer** | Core business logic, unit conversions, operations | Services, Converters, Interfaces |
| **Model Layer** | Data transfer objects, entities, enums, exceptions | DTOs, Entities, Enums, Custom Exceptions |
| **Repository Layer** | Data persistence, Entity Framework Core mappings | Repositories, DbContext, Migrations |

---

## 🛠️ Technology Stack

### Core
- **.NET 8.0+** - Cross-platform framework
- **C# 12** - Modern programming language
- **ASP.NET Core 8** - Web framework for REST APIs

### Data Access
- **Entity Framework Core** - ORM for data persistence
- **SQL Server** - Primary database
- **SQL Server Management Studio** - Database management

### Authentication & Security
- **JWT (JSON Web Tokens)** - Token-based authentication
- **Microsoft.IdentityModel.Tokens** - Token validation
- **CORS** - Cross-Origin Resource Sharing

### API & Documentation
- **Swagger/OpenAPI** - Interactive API documentation
- **Swashbuckle.AspNetCore** - Swagger generation

### Testing
- **MSTest** - Unit testing framework
- **xUnit-compatible assertions**
- **TestHelper utilities** - Test support

### Development Tools
- **Visual Studio 2022+** - IDE
- **Git** - Version control
- **NuGet** - Package management

---

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- **[.NET SDK 8.0 or higher](https://dotnet.microsoft.com/download)**
  ```bash
  dotnet --version  # Verify installation
  ```

- **SQL Server 2019 or higher** (or SQL Server Express)
  ```bash
  sqlcmd -S localhost -U sa -P YourPassword -Q "SELECT @@VERSION"
  ```

- **Visual Studio 2022** (Community Edition or higher) OR **VS Code** with C# extension

- **Git** for version control
  ```bash
  git --version
  ```

- **Postman or similar API testing tool** (optional but recommended)

---

## 🚀 Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/QuantityMeasurementApp.git
cd QuantityMeasurementApp
```

### 2. Restore Dependencies

```bash
dotnet restore
```

This will restore all NuGet packages specified in the project files.

### 3. Database Setup

#### Option A: Using SQL Server Management Studio

1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Create a new database named `QuantityMeasurementDBB` (or your preferred name)
4. Run the provided SQL migration scripts:
   - `SQLQuery1.sql` - Initial schema
   - `SQLQuery2.sql` - Additional migrations or seed data

#### Option B: Using Entity Framework Core Migrations

```bash
# Navigate to the API project
cd QuantityMeasurementApp.API

# Update database to latest migration
dotnet ef database update --project ../RepoLayer

# Or create a new migration
dotnet ef migrations add InitialCreate --project ../RepoLayer
```

### 4. Configure Connection String

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=QuantityMeasurementDBB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;"
  }
}
```

**Connection String Parameters:**
- `Server` - SQL Server instance name or IP address
- `Database` - Database name
- `Trusted_Connection=True` - Use Windows authentication
- `TrustServerCertificate=True` - Trust self-signed certificates (development only)

### 5. Configure JWT Settings

Update JWT configuration in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyForJWTTokenGenerationThatIsAtLeast32CharsLong123456!",
    "Issuer": "QuantityMeasurementApp",
    "Audience": "QuantityMeasurementAppClients"
  }
}
```

**Important:** Change the `Key` value to a strong, unique secret in production environments.

### 6. Build the Solution

```bash
# Build the entire solution
dotnet build

# Build with Release configuration
dotnet build --configuration Release
```

### 7. Run the Application

#### Development Environment

```bash
cd QuantityMeasurementApp.API
dotnet run
```

The API will be available at:
- **API Base URL**: `https://localhost:5001` or `http://localhost:5000`
- **Swagger UI**: `https://localhost:5001/swagger/index.html`

#### Production Environment

```bash
dotnet run --configuration Release --no-build
```

---

## ⚙️ Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=QuantityMeasurementDBB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyForJWTTokenGenerationThatIsAtLeast32CharsLong123456!",
    "Issuer": "QuantityMeasurementApp",
    "Audience": "QuantityMeasurementAppClients"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### appsettings.Development.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information"
    }
  }
}
```

### Environment Variables

You can override settings using environment variables:

```bash
set ConnectionStrings__DefaultConnection="your_connection_string"
set Jwt__Key="your_jwt_key"
set Jwt__Issuer="your_issuer"
set Jwt__Audience="your_audience"
```

---

## 📚 API Documentation

### Interactive API Documentation

The application exposes **Swagger/OpenAPI** documentation:

- **Swagger UI**: Navigate to `https://localhost:5001/swagger/index.html`
- **OpenAPI JSON**: Available at `https://localhost:5001/swagger/v1/swagger.json`

### Authentication

All protected endpoints require a JWT token in the Authorization header:

```bash
Authorization: Bearer <your_jwt_token>
```

### Base URL

```
https://localhost:5001/api/v1
```

### Main Endpoints

#### Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/auth/register` | Register a new user |
| POST | `/auth/login` | User login (returns JWT token) |

#### Quantity Measurement

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/quantitymeasurement/compare` | Compare two quantities |
| POST | `/quantitymeasurement/add` | Add two quantities |
| POST | `/quantitymeasurement/subtract` | Subtract two quantities |
| POST | `/quantitymeasurement/divide` | Divide two quantities |
| GET | `/quantitymeasurement/history` | Get user's operation history |

---

## 💡 Usage Examples

### 1. User Registration

**Request:**
```http
POST /api/v1/auth/register HTTP/1.1
Content-Type: application/json

{
  "username": "john_doe",
  "email": "john@example.com",
  "password": "SecurePassword123!",
  "role": "User"
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "username": "john_doe",
  "email": "john@example.com",
  "role": "User",
  "registeredDate": "2024-03-30T10:30:00Z"
}
```

### 2. User Login

**Request:**
```http
POST /api/v1/auth/login HTTP/1.1
Content-Type: application/json

{
  "username": "john_doe",
  "password": "SecurePassword123!"
}
```

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "john_doe",
  "expiresIn": 3600
}
```

### 3. Compare Quantities

**Request:**
```http
POST /api/v1/quantitymeasurement/compare HTTP/1.1
Authorization: Bearer <jwt_token>
Content-Type: application/json

{
  "quantity1": {
    "value": 100,
    "unit": "Meter"
  },
  "quantity2": {
    "value": 0.1,
    "unit": "Kilometer"
  }
}
```

**Response (200 OK):**
```json
{
  "comparison": "Equal",
  "quantity1Converted": 100,
  "quantity2Converted": 100,
  "baseUnit": "Meter"
}
```

### 4. Add Quantities

**Request:**
```http
POST /api/v1/quantitymeasurement/add HTTP/1.1
Authorization: Bearer <jwt_token>
Content-Type: application/json

{
  "quantity1": {
    "value": 50,
    "unit": "Kilogram"
  },
  "quantity2": {
    "value": 2000,
    "unit": "Gram"
  }
}
```

**Response (200 OK):**
```json
{
  "result": 52,
  "baseUnit": "Kilogram",
  "operationType": "Addition"
}
```

### 5. Get Operation History

**Request:**
```http
GET /api/v1/quantitymeasurement/history HTTP/1.1
Authorization: Bearer <jwt_token>
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "userId": 1,
    "operationType": "Comparison",
    "quantity1": "100 Meter",
    "quantity2": "0.1 Kilometer",
    "result": "Equal",
    "executedAt": "2024-03-30T10:35:00Z"
  },
  {
    "id": 2,
    "userId": 1,
    "operationType": "Addition",
    "quantity1": "50 Kilogram",
    "quantity2": "2000 Gram",
    "result": "52 Kilogram",
    "executedAt": "2024-03-30T10:36:00Z"
  }
]
```

### Using cURL

```bash
# Register user
curl -X POST "https://localhost:5001/api/v1/auth/register" \
  -H "Content-Type: application/json" \
  -d '{"username":"john_doe","email":"john@example.com","password":"SecurePassword123!","role":"User"}'

# Login and get token
TOKEN=$(curl -s -X POST "https://localhost:5001/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"username":"john_doe","password":"SecurePassword123!"}' | jq -r '.token')

# Compare quantities using token
curl -X POST "https://localhost:5001/api/v1/quantitymeasurement/compare" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"quantity1":{"value":100,"unit":"Meter"},"quantity2":{"value":0.1,"unit":"Kilometer"}}'
```

### Using Postman

1. Import the API endpoints from Swagger: `https://localhost:5001/swagger/v1/swagger.json`
2. Create a Bearer Token in the Authorization tab
3. Use the token for authenticated requests

---

## 📂 Project Structure

```
QuantityMeasurementApp/
├── BusinessLayer/                      # Business logic layer
│   ├── Services/
│   │   ├── AuthService.cs
│   │   ├── JwtService.cs
│   │   ├── QuantityMeasurementService.cs
│   │   ├── LengthUnitConverter.cs
│   │   ├── WeightUnitConverter.cs
│   │   ├── VolumeUnitConverter.cs
│   │   └── TemperatureUnitConverter.cs
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── IJwtService.cs
│   │   └── IQuantityMeasurementService.cs
│   ├── Extensions/
│   │   └── BusinessServiceExtensions.cs
│   └── BusinessLayer.csproj
│
├── ModelLayer/                         # Data models and DTOs
│   ├── DTOs/
│   │   ├── QuantityInputDTO.cs
│   │   ├── QuantityMeasurementDTO.cs
│   │   ├── QuantityResultDto.cs
│   │   ├── ComparisionResultDto.cs
│   │   ├── DivisionResultDto.cs
│   │   ├── Auth/
│   │   └── Quantity/
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Quantity.cs
│   │   └── QuantityMeasurementEntity.cs
│   ├── Enums/
│   │   ├── LengthUnit.cs
│   │   ├── WeightUnit.cs
│   │   ├── VolumeUnit.cs
│   │   ├── TemperatureUnit.cs
│   │   ├── OperationType.cs
│   │   └── Role.cs
│   ├── Exceptions/
│   │   ├── QuantityMeasurementException.cs
│   │   └── DatabaseException.cs
│   ├── Interfaces/
│   │   ├── IMeasurable.cs
│   │   └── IUnitConverter.cs
│   └── ModelLayer.csproj
│
├── RepoLayer/                          # Data access layer
│   ├── Context/
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/
│   │   ├── UserRepository.cs
│   │   ├── QuantityRepository.cs
│   │   └── BaseRepository.cs
│   ├── Interfaces/
│   │   ├── IUserRepository.cs
│   │   ├── IQuantityRepository.cs
│   │   └── IRepository.cs
│   ├── Migrations/
│   │   └── [Migration files]
│   ├── Persistence/
│   ├── Auth/
│   ├── Extensions/
│   │   └── RepositoryExtensions.cs
│   └── RepoLayer.csproj
│
├── QuantityMeasurementApp.API/         # REST API layer
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── QuantityMeasurementController.cs
│   ├── Middleware/
│   │   └── [Custom middleware]
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── QuantityMeasurementApp.API.csproj
│
├── QuantityMeasurementApp/             # Console application (optional)
│   ├── Program.cs
│   └── QuantityMeasurementApp.csproj
│
├── QuantityMeasurementApp.Tests/       # Unit tests
│   ├── ArithmeticOperationTest.cs
│   ├── CentralizedArithmeticOperationTest.cs
│   ├── GenericQuantityTest.cs
│   ├── TemperatureTest.cs
│   ├── VolumeMeasurementTest.cs
│   ├── ControllerTests/
│   ├── TestHelper.cs
│   ├── MSTestSettings.cs
│   └── QuantityMeasurementApp.Tests.csproj
│
├── QuantityMeasurementApp.sln          # Solution file
├── README.md                           # This file
├── LICENSE                             # License file
├── .gitignore                          # Git ignore file
├── SQLQuery1.sql                       # Database schema
└── SQLQuery2.sql                       # Database migrations
```

### Key Directory Descriptions

| Directory | Purpose |
|-----------|---------|
| `BusinessLayer/` | Core business logic, services, and unit converters |
| `ModelLayer/` | Data models, DTOs, enums, and custom exceptions |
| `RepoLayer/` | Entity Framework Core mappings and repository pattern implementation |
| `QuantityMeasurementApp.API/` | REST API controllers and endpoint definitions |
| `QuantityMeasurementApp.Tests/` | Comprehensive unit tests for all layers |

---

## 🧪 Testing

### Running Tests

#### Run All Tests

```bash
dotnet test
```

#### Run Tests with Verbose Output

```bash
dotnet test --verbosity normal
```

#### Run Specific Test File

```bash
dotnet test --filter "ClassName=GenericQuantityTest"
```

#### Run Tests with Coverage Report

```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

### Test Files

| Test File | Coverage |
|-----------|----------|
| `GenericQuantityTest.cs` | Generic quantity operations |
| `ArithmeticOperationTest.cs` | Arithmetic operations (add, subtract, divide) |
| `TemperatureTest.cs` | Temperature unit conversions |
| `VolumeMeasurementTest.cs` | Volume unit conversions |
| `CentralizedArithmeticOperationTest.cs` | Centralized operation tests |
| `ControllerTests/` | API controller endpoint tests |

### Test Framework

- **Framework**: MSTest (Microsoft.VisualStudio.TestTools.UnitTesting)
- **Assertions**: Standard MSTest assertions and custom helpers
- **Coverage**: Comprehensive unit test coverage across all layers

### Example Test

```csharp
[TestClass]
public class GenericQuantityTest
{
    [TestMethod]
    public void TestMeterToFeetConversion()
    {
        // Arrange
        double meters = 1;
        double expectedFeet = 3.28084;
        
        // Act
        var converter = new LengthUnitConverter();
        var result = converter.Convert(meters, LengthUnit.Meter, LengthUnit.Foot);
        
        // Assert
        Assert.AreEqual(expectedFeet, result, 0.00001);
    }
}
```

---

## 🔒 Security

### Authentication & Authorization

- **JWT Tokens**: All protected endpoints require valid JWT tokens
- **Token Validation**: Tokens are validated against issuer, audience, and expiration
- **Role-Based Access Control**: Different user roles have different permissions

### Security Best Practices

1. **Always use HTTPS in production** - Set `RequireHttpsMetadata = true` in production
2. **Secure your JWT Key** - Use a strong, random secret key (minimum 32 characters)
3. **Update dependencies regularly** - Keep NuGet packages up to date
4. **Validate all inputs** - Server-side validation prevents injection attacks
5. **Use environment variables** - Never commit sensitive data to version control

### CORS Configuration

CORS is enabled with the following default policy:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```

**For production**, restrict to specific origins:

```json
{
  "Cors": {
    "AllowedOrigins": ["https://yourdomain.com", "https://app.yourdomain.com"]
  }
}
```

### Token Expiration

Configure token expiration time in the JWT service:

```csharp
var tokenHandler = new JwtSecurityTokenHandler();
var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(claims),
    Expires = DateTime.UtcNow.AddHours(1),  // Token expires in 1 hour
    SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(key), 
        SecurityAlgorithms.HmacSha256Signature)
};
```

---

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

### Process

1. **Fork the repository**
   ```bash
   git clone https://github.com/yourusername/QuantityMeasurementApp.git
   ```

2. **Create a feature branch**
   ```bash
   git checkout -b feature/UC##-feature-description
   ```

3. **Make your changes**
   - Follow the existing code style and patterns
   - Add unit tests for new functionality
   - Update documentation as needed

4. **Commit your changes**
   ```bash
   git commit -m "feat: Add feature description"
   ```

5. **Push to your fork**
   ```bash
   git push origin feature/UC##-feature-description
   ```

6. **Submit a Pull Request**
   - Describe the changes made
   - Reference any related issues
   - Ensure all tests pass

### Code Standards

- Follow **C# naming conventions** (PascalCase for classes, camelCase for variables)
- Use **meaningful variable names** and add XML comments for public members
- Keep **methods small and focused** on single responsibility
- Write **unit tests** for all new functionality
- Use **async/await** for I/O operations

### Commit Message Format

```
<type>: <subject>

<body>

<footer>
```

**Types**: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

**Example**:
```
feat: Add temperature unit conversion support

- Implement Celsius, Fahrenheit, and Kelvin conversions
- Add comprehensive temperature conversion tests
- Update API documentation

Closes #42
```

---

## 🐛 Troubleshooting

### Common Issues

#### 1. Database Connection Failed

**Error**: `cannot open database "QuantityMeasurementDBB"`

**Solution**:
```bash
# Verify SQL Server is running
sqlcmd -S localhost

# Check connection string in appsettings.json
# Ensure database name matches your created database
# Create database if it doesn't exist:
sqlcmd -S localhost -Q "CREATE DATABASE QuantityMeasurementDBB"
```

#### 2. JWT Token Invalid or Expired

**Error**: `401 Unauthorized - Invalid token`

**Solution**:
```bash
# Ensure token is passed correctly in Authorization header
# Header format: Authorization: Bearer <token>
# Verify token hasn't expired
# Check JWT configuration matches in all projects
```

#### 3. CORS Error: Origin Not Allowed

**Error**: `No 'Access-Control-Allow-Origin' header`

**Solution**:
```csharp
// Update CORS policy in Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecific", builder =>
    {
        builder.WithOrigins("https://yourdomain.com")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```

#### 4. Migrations Not Applied

**Error**: `The migration ... has not been applied`

**Solution**:
```bash
# Update database to latest migration
dotnet ef database update --project RepoLayer

# Or clear all migrations and start fresh
dotnet ef database update 0 --project RepoLayer  # Reverts all
dotnet ef database update --project RepoLayer     # Applies all
```

#### 5. Port Already in Use

**Error**: `Address already in use: http://localhost:5000`

**Solution**:
```bash
# Find process using the port
netstat -ano | findstr :5000

# Kill the process (Windows)
taskkill /PID <PID> /F

# Or use a different port
dotnet run --urls "https://localhost:5002"
```

#### 6. NuGet Package Restore Failed

**Error**: `Package restore failed`

**Solution**:
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore --force
```

### Debug Mode

Run with detailed logging:

```bash
# Set logging level to Debug
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --environment Development
```

Check logs in:
- Console output
- `appsettings.Development.json` logging configuration
- Event Viewer (Windows)

### Getting Help

If you encounter issues:

1. Check the **Troubleshooting** section above
2. Review **API documentation** at `/swagger`
3. Check **existing issues** on GitHub
4. Create a **new issue** with detailed error information
5. Contact the development team

---

## 📄 License

This project is licensed under the **MIT License** - see [LICENSE](LICENSE) file for details.

### MIT License Summary

- ✅ Commercial use
- ✅ Modification
- ✅ Distribution
- ✅ Private use
- ❌ Liability
- ❌ Warranty

---

## 📞 Support

### Contact Information

- **Email**: support@quantitymeasurement.dev
- **Issues**: [GitHub Issues](https://github.com/yourusername/QuantityMeasurementApp/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/QuantityMeasurementApp/discussions)

### Documentation Resources

- [API Documentation](https://localhost:5001/swagger) - Interactive Swagger UI
- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [ASP.NET Core Guide](https://docs.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [JWT.io](https://jwt.io/) - JWT decoder and validator

### Useful Commands Reference

```bash
# Build
dotnet build

# Test
dotnet test

# Run
dotnet run

# Publish
dotnet publish -c Release

# Entity Framework
dotnet ef migrations add MigrationName
dotnet ef database update
dotnet ef database update 0  # Revert all migrations

# NuGet
dotnet add package PackageName
dotnet remove package PackageName
dotnet list package --outdated
```

---

## 🎯 Roadmap

### Planned Features

- [ ] GraphQL API support
- [ ] Advanced caching strategies (Redis)
- [ ] Batch operations API
- [ ] Custom unit definitions
- [ ] Real-time notifications (SignalR)
- [ ] Mobile application (Xamarin/MAUI)
- [ ] Web UI dashboard
- [ ] Advanced reporting and analytics
- [ ] API rate limiting
- [ ] Webhooks support

---

## 📊 Project Statistics

- **Lines of Code**: ~2,500+
- **Test Coverage**: Comprehensive
- **Architecture Layers**: 4 (API, Business, Model, Repository)
- **Supported Measurement Types**: 4 (Length, Weight, Volume, Temperature)
- **Database**: SQL Server
- **Framework**: .NET 8.0+

---

## 🙏 Acknowledgments

- Built with **.NET 8.0** - Modern and efficient
- API documentation with **Swagger/OpenAPI**
- Database management with **Entity Framework Core**
- Security with **JWT Authentication**
- Testing with **MSTest**

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0.0 | Mar 30, 2024 | Initial release with core features |
| 1.1.0 | [Future] | Add GraphQL support |
| 1.2.0 | [Future] | Redis caching integration |

---

**Last Updated**: March 30, 2024
**Maintainer**: Your Name / Team
**Repository**: https://github.com/yourusername/QuantityMeasurementApp

---

## Quick Start Checklist

- [ ] Clone the repository
- [ ] Install .NET SDK 8.0+
- [ ] Restore dependencies: `dotnet restore`
- [ ] Create and configure database
- [ ] Update `appsettings.json` with your configuration
- [ ] Run migrations: `dotnet ef database update`
- [ ] Run tests: `dotnet test`
- [ ] Start the application: `dotnet run`
- [ ] Visit Swagger: `https://localhost:5001/swagger`
- [ ] Register a user and test the API

---

**Happy Coding! 🚀**
