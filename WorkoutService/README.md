# Workout Service

The Workout Service is a core component of the Elevate Fitness application, responsible for managing all aspects of workouts, exercises, and workout plans. It provides a comprehensive set of APIs for creating, retrieving, and managing fitness-related data.

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)

## Features

-   **Workout Management:** Create, retrieve, and manage workout routines.
-   **Exercise Library:** A comprehensive library of exercises with detailed information.
-   **Workout Plans:** Pre-defined workout plans for various fitness goals.
-   **Session Tracking:** Start and track workout sessions.
-   **Standardized Responses:** A consistent and predictable API response format.

## Architecture

This service is built using a **Vertical Slice Architecture**. Each feature is self-contained within its own "slice," which includes all the necessary components for that feature, such as MediatR queries and commands, handlers, and API endpoints. This approach promotes modularity and maintainability.

-   **.NET 8:** The service is built on the latest version of the .NET platform.
-   **Minimal APIs:** API endpoints are defined using the lightweight and efficient Minimal API framework.
-   **MediatR:** The CQRS (Command Query Responsibility Segregation) pattern is implemented using the MediatR library to decouple business logic from the API controllers.
-   **Entity Framework Core:** The service uses EF Core for data access and to interact with the SQL Server database.
-   **Mapster:** Object-to-object mapping is handled by the Mapster library.

## Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
-   A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    ```
2.  **Navigate to the service directory:**
    ```bash
    cd WorkoutService
    ```
3.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```

### Database Setup

1.  **Update Connection String:** Open the `appsettings.Development.json` file and update the `DefaultConnection` string to point to your local SQL Server instance.
2.  **Apply Migrations:** Run the following command to apply the database migrations and create the database schema:
    ```bash
    dotnet ef database update
    ```

### Running the Application

To run the application locally, use the following command:

```bash
dotnet run
```

The service will be available at `https://localhost:5001` (or a similar port).

*(Note: In the future, the service will be containerized and can be run using Docker.)*

## API Endpoints

Here is a summary of the available API endpoints:

| Method | Endpoint                                 | Description                      |
| ------ | ---------------------------------------- | -------------------------------- |
| `GET`  | `/api/v1/workouts`                       | Get a paginated list of workouts |
| `GET`  | `/api/v1/workouts/{id}`                  | Get the details of a workout     |
| `GET`  | `/api/v1/workouts/category/{categoryName}` | Get workouts by category         |
| `POST` | `/api/v1/workouts/{id}/start`            | Start a new workout session      |

## Authentication

The service uses **JWT Bearer token** and **OAuth** for authentication. To access protected endpoints, you must include a valid JWT in the `Authorization` header of your request:

```
Authorization: Bearer <your-jwt-token>
```