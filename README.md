# ProductAPI

## Introduction
ProductAPI is a sample .NET Core web API project designed to manage product information. The project demonstrates the use of Clean Architecture, CQRS pattern with MediatR, and AutoMapper for mapping. It also includes authentication and authorization using ASP.NET Core Identity and JWT tokens.

## Features
- Create, read, update, and delete (CRUD) operations for products
- Authentication and authorization with JWT
- Clean Architecture implementation
- CQRS pattern using MediatR
- AutoMapper for object mapping
- TDD

## Technologies Used
- .NET Core 8.0
- ASP.NET Core Web API
- Entity Framework Core
- MediatR
- AutoMapper
- xUnit
- Moq

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server

### Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/erfanfarhanian/ProductAPI.git
2. Navigate to the project directory
   ```sh
   cd ProductAPI
  
### Configuration
Update the database connection string in appsettings.json:
  ```json
  {
    "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ProductAPI_Db;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
    },
  "Jwt": {
    "Key": "be9631c1270943e9a1dfe2840ce9ebcc7cc44e7478b6445bae2aeb9f6b20f6ce",
    "Issuer": "https://localhost:7134", // Your localhost!
    "Audience": "productapi"
    }
  }
```
Apply migrations and update the database
   ```sh
   dotnet ef database update
```
### Running the Application
Build the project:
  ```sh
dotnet build
```
Run the project:
```sh
dotnet run
```
### Running Tests
To run the unit and integration tests, use the following command:
```sh
dotnet test
```
### Authentication Usage
```json
{
  "username": "your-username",
  "password": "your-password"
}
```
* The username and password of Authenticated users are in Info folder.
- You will receive a JWT token which you should include in the Authorization header of subsequent requests.

## Endpoints
- GET /api/products: Retrieve all products
- GET /api/products/{id}: Retrieve a product by ID
- POST /api/products: Create a new product (requires authentication)
- PUT /api/products/{id}: Update an existing product (requires authentication)
- DELETE /api/products/{id}: Delete a product (requires authentication)

* For performance testing of applications (like CRUD, Authentication, ...) I used postman.
* Also I used Microsoft Visual Studio 2022 in this project.
