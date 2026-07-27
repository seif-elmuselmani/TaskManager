# Task Manager Project

This is a simple Task Manager web application developed as part of a technical assessment. It consists of a Backend API built with ASP.NET Core and a Frontend built with React (Vite).

## Overview

- **Backend**: ASP.NET Core 10 Web API, EF Core, SQL Server (LocalDB).
- **Frontend**: React, React Router, Axios.

## Prerequisites

- .NET 10 SDK
- Node.js & npm
- SQL Server LocalDB (installed with Visual Studio)

## How to run the project

### 1. Database & Backend
Navigate to the backend directory:
```bash
cd TaskManager
```

The connection string in `TaskManager.API/appsettings.json` points to LocalDB by default:
`Server=(localdb)\mssqllocaldb;Database=TaskManagerDb;Trusted_Connection=True`

Update the database schema:
```bash
dotnet ef database update --project TaskManager.Infrastructure --startup-project TaskManager.API
```

Run the API:
```bash
cd TaskManager.API
dotnet run
```
The API will start on `http://localhost:5264`.

### 2. Frontend
Open a new terminal and navigate to the frontend directory:
```bash
cd taskmanager-ui
```

Install dependencies and run:
```bash
npm install
npm run dev
```
The React app will start on `http://localhost:5173`.

## Architecture & Design Decisions

- **Architecture**: The solution is structured using a simplified Clean Architecture approach, separating the logic into `Core`, `Infrastructure`, and `API` layers. 
- **Modular Structure**: Inside the `Core` layer, I separated the `Projects` and `Tasks` into distinct modules to satisfy the bonus requirement for microservices readiness.
- **Repository Pattern & Service Layer**: Used Repositories for data access to abstract Entity Framework, and a Service Layer to handle business logic and DTO mapping.
- **Result Pattern**: Used a custom `Result<T>` wrapper in the Service Layer. This helps separate business logic from HTTP status codes.
- **Error Handling**: Added a global exception handling middleware to catch unhandled errors and return a proper JSON response.
- **Validation**: Integrated FluentValidation for validating DTOs.
- **Frontend**: Kept the UI clean and simple using standard CSS and flexbox/grid layouts.

## Future Improvements

If I had more time, I would consider adding:
- Authentication and Authorization (Identity/JWT) to link tasks to specific users.
- A Unit of Work pattern to handle compound database transactions more elegantly.
- Server-side pagination, filtering, and sorting for better performance with large datasets.
- AutoMapper for cleaner object-to-object mapping instead of manual DTO mapping.
- Comprehensive unit testing for the service layer and frontend components.
