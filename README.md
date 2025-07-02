# Car Management API - .NET 8 Backend Application

This is a robust REST API for managing car brands, models, and countries of origin. Built with .NET 8 and PostgreSQL, the application follows modern architectural patterns and provides comprehensive API documentation through Swagger UI.

## Features

- RESTful API for CRUD operations on cars, brands, and countries
- CQRS pattern with MediatR implementation
- Entity Framework Core for database operations
- PostgreSQL database support
- Docker containerization with Docker Compose
- Swagger UI for API documentation and testing
- Repository Pattern and Unit of Work implementation
- HTTPS support with Kestrel web server

## Technology Stack

|Component |	Technology|
|---|---|
| Backend | Framework	.NET 8|
| Database	| PostgreSQL 16|
| ORM	| Entity Framework Core 8|
| API Documentation	| Swagger|
| Containerization	| Docker + Docker Compose|
| Web Server	| Kestrel|

## API Endpoints
The application provides endpoints for managing three main resources:

### Cars
- GET /api/Car - Get list of cars
- GET /api/Car/{id} - Get car details
- POST /api/Car/powerQuery - Get a list of cars filtered by engine power
- POST /api/Car/priceQuery - Get a list of cars filtered by price
- POST /api/Car/detailsQuery - Get a list of cars filtered by engine power and price
- POST /api/Car - Create new car
- PUT /api/Car - Update existing car
- DELETE /api/Car/{id} - Delete car

### Brands
- GET /api/Brand - Get list of brands
- GET /api/Brand/{id} - Get brand details
- POST /api/Brand - Create new brand
- PUT /api/Brand - Update existing brand
- DELETE /api/Brand/{id} - Delete brand

### Countries
- GET /api/Country - Get list of countries
- GET /api/Country/{id} - Get country details
- POST /api/Country - Create new country
- PUT /api/Country - Update existing country
- DELETE /api/Country/{id} - Delete country

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL (optional for local development)](https://www.postgresql.org/download/)

## Getting Started

1. Clone the repository  
   git clone https://github.com/AL512/CarWebApi.git  
   cd CarWebApi
2. Configure the application  
   Edit appsettings.json to configure your PostgreSQL connection:  

```json
{
   "Kestrel": {
      "Endpoints": {
         "HTTP": {
            "Url": "http://127.0.0.1:5050"
         },
         "HTTPS": {
            "Url": "https://127.0.0.1:7050"
         }
      }
   },

   "DatabaseConfig":  {
      "PostgreSQLConnection": "Host=localhost;Port=5432;Database=CarWebApi;Username=postgres;Password=123"
   },

   "Logging": {
      "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
      }
   },
   "AllowedHosts": "*"
}
```

3. Run with Docker Compose (Recommended)  
   docker-compose up --build  
   
This will:

- Build the backend Docker image
- Start PostgreSQL container
- Start the backend API container

Apply database migrations automatically

4. Access the API  
   The API will be available at:

   HTTP: http://localhost:5050  
   HTTPS: https://localhost:7050 
 

5. Access Swagger UI  
   For API documentation and testing:
   
   HTTP Swagger: http://localhost:5050/swagger  
   HTTPS Swagger: https://localhost:7050/swagger

To trust the HTTPS development certificate on your local machine:

```bash
dotnet dev-certs https --trust
```

## Database Configuration
The application uses PostgreSQL as the primary database. The connection string is configured in appsettings.json:

```json
"DatabaseConfig": {
"PostgreSQLConnection": "Host=localhost;Port=5432;Database=CarWebApi;Username=postgres;Password=123"
}
```

In the Docker Compose setup, the database is automatically initialized and migrated on startup.

The overall structure of the project
```text
car-management-api/
├── Controllers/          # API controllers
│   ├── CarController.cs
│   ├── BrandController.cs
│   └── CountriesController.cs
│
├── CQRS/                 # Commands and queries
│   ├── Commands/
│   ├── Queries/
│   └── QueriesParam/
│
├── Database/             # Database config and setting
│
├── Migrations/           # Database migrations
│
├── Models/               # Data models
│   ├── Cars/
│   ├── Brands/
│   └── Countries/
│
├── Repositories/         # Repository implementations
│   ├── Interfaces/
│   └── Implementations/
│
├── appsettings.json      # Application configuration
├── Dockerfile            # Docker build configuration
├── docker-compose.yml    # Docker Compose setup
└── Program.cs            # Startup configuration
```

## License
This project is licensed under the MIT License.

## Contact
For questions or support, please contact AL512@mail.ru