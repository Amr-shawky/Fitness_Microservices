# Elevate Fitness Microservices

Welcome to the Elevate Fitness application! This project is a comprehensive fitness platform built on a microservices architecture. Each service is designed to handle a specific domain of the application, ensuring scalability, maintainability, and flexibility.

## Table of Contents

- [Overview](#overview)
- [Services](#services)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Running the System](#running-the-system)
- [API Gateway](#api-gateway)
- [Development Guidelines](#development-guidelines)

## Overview

The Elevate Fitness application is designed to be a one-stop-shop for all your fitness needs. It provides a wide range of features, including workout tracking, nutrition planning, progress monitoring, and personalized coaching. The system is built using a set of independent microservices that communicate with each other through a central API Gateway.

## Services

This project is composed of the following microservices:

-   **Authentication Service:** Handles user authentication, registration, and JWT token management.
-   **User Profile Service:** Manages user profiles, including personal information, fitness goals, and preferences.
-   **Workout Service:** Responsible for managing workouts, exercises, and workout plans.
-   **Nutrition Service:** Manages meal plans, food items, and nutritional information.
-   **Progress Tracking Service:** Tracks user progress, including workout history, body measurements, and performance metrics.
-   **Fitness Calculation Service:** Provides fitness-related calculations, such as calorie burn estimates and macronutrient recommendations.
-   **Smart Coach Service:** A smart coaching system that provides personalized recommendations and guidance.
-   **Fitness API Gateway:** The single entry point for all client requests. It routes requests to the appropriate microservice and handles cross-cutting concerns like authentication and logging.

## Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Docker](https://www.docker.com/products/docker-desktop) (recommended for running the entire system)
-   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (for individual service development)

### Running the System

While each service can be run individually, the recommended way to run the entire system is through Docker Compose. *(Note: A `docker-compose.yml` file will be added in the future to orchestrate the services.)*

To run an individual service (e.g., `WorkoutService`):

1.  **Navigate to the service directory:**
    ```bash
    cd WorkoutService
    ```
2.  **Update the connection string** in `appsettings.Development.json` to point to your local database.
3.  **Apply migrations:**
    ```bash
    dotnet ef database update
    ```
4.  **Run the service:**
    ```bash
    dotnet run
    ```

## API Gateway

The **Fitness API Gateway** is the public-facing entry point for the entire system. All client applications (web, mobile) should interact with the API Gateway, which will then route requests to the appropriate downstream service. This provides a unified and secure interface to the microservices.

## Development Guidelines

-   **Service Boundaries:** Each service should be responsible for a single, well-defined domain. Avoid creating dependencies between services where possible.
-   **Communication:** Services should communicate asynchronously where possible (e.g., using a message broker). For synchronous communication, use lightweight protocols like REST.
-   **Code Style:** Follow the established coding conventions and patterns within each service.
-   **Testing:** Each service should have its own set of unit and integration tests.
