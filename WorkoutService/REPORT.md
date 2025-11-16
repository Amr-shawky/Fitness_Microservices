# Workout Service System Report

This report provides a comprehensive overview of the `WorkoutService`, including its database schema, feature organization, API endpoints, and architectural patterns.

## 1. Database Tables and Properties

The `WorkoutService` uses Entity Framework Core to manage its database schema. The following tables are defined based on the domain entities:

| Table Name              | Property                   | Data Type      | Description                                                 |
| :---------------------- | :------------------------- | :------------- | :---------------------------------------------------------- |
| **Workouts**            | `Id` (PK)                  | `int`          | Unique identifier for the workout.                          |
|                         | `Name`                     | `string`       | The name of the workout routine.                            |
|                         | `Description`              | `string`       | A detailed description of the workout.                      |
|                         | `Category`                 | `string`       | e.g., "full-body", "chest", "legs".                         |
|                         | `Difficulty`               | `string`       | e.g., "Beginner", "Intermediate", "Advanced".               |
|                         | `DurationInMinutes`        | `int`          | The estimated duration of the workout.                      |
|                         | `CaloriesBurn`             | `int`          | Estimated calories burned.                                  |
|                         | `IsPremium`                | `bool`         | Indicates if the workout is for premium users.              |
|                         | `Rating`                   | `double`       | The average user rating for the workout.                    |
|                         | `WorkoutPlanId` (FK)       | `int`          | Foreign key linking to the `WorkoutPlans` table.            |
| **Exercises**           | `Id` (PK)                  | `int`          | Unique identifier for the exercise.                         |
|                         | `Name`                     | `string`       | The name of the exercise.                                   |
|                         | `Description`              | `string`       | A description of how to perform the exercise.               |
|                         | `Difficulty`               | `string`       | The difficulty level of the exercise.                       |
|                         | `TargetMuscles`            | `List<string>` | The primary muscle groups targeted.                         |
|                         | `EquipmentNeeded`          | `List<string>` | The equipment required for the exercise.                    |
| **WorkoutPlans**        | `Id` (PK)                  | `int`          | Unique identifier for the workout plan.                     |
|                         | `Name`                     | `string`       | The name of the plan (e.g., "Weight Loss - Normal").        |
|                         | `Description`              | `string`       | A description of the plan's goal.                           |
|                         | `Goal`                     | `string`       | The primary goal (e.g., "Lose Weight", "Gain Weight").      |
|                         | `Difficulty`               | `string`       | The overall difficulty of the plan.                         |
| **WorkoutSessions**     | `Id` (PK)                  | `Guid`         | Unique identifier for a specific user's workout session.    |
|                         | `UserId`                   | `Guid`         | The ID of the user who started the session.                 |
|                         | `WorkoutId` (FK)           | `int`          | Foreign key linking to the `Workouts` table.                |
|                         | `Status`                   | `string`       | The current status (e.g., "InProgress", "Completed").       |
|                         | `StartedAt`                | `DateTime`     | The timestamp when the session began.                       |
|                         | `EndedAt`                  | `DateTime?`    | The timestamp when the session ended (nullable).            |
|                         | `PlannedDurationInMinutes` | `int`          | The user-specified planned duration.                        |
|                         | `Difficulty`               | `string`       | The difficulty level chosen by the user for the session.    |
| **WorkoutExercises**    | *Composite PK*             |                | (Join table for a many-to-many relationship)                |
|                         | `WorkoutId` (FK)           | `int`          | Links to the `Workouts` table.                              |
|                         | `ExerciseId` (FK)          | `int`          | Links to the `Exercises` table.                             |
|                         | `Order`                    | `int`          | The sequence of the exercise in the workout.                |
|                         | `Sets`                     | `int`          | The number of sets for the exercise.                        |
|                         | `Reps`                     | `string`       | The repetition scheme (e.g., "8-12", "30s").                |
|                         | `RestTimeInSeconds`        | `int`          | The rest time after each set.                               |
| **WorkoutSessionExercises** | `Id` (PK)                | `Guid`         | Unique identifier for an exercise within a session.         |
|                         | `WorkoutSessionId` (FK)    | `Guid`         | Links to the `WorkoutSessions` table.                       |
|                         | `ExerciseId` (FK)          | `int`          | Links to the `Exercises` table.                             |
|                         | `Status`                   | `string`       | The status (e.g., "Pending", "Completed").                  |
|                         | `Order`                    | `int`          | The sequence of the exercise in this specific session.      |

## 2. Feature Folders

The business logic is organized into feature folders within `WorkoutService/Features/`. This follows the **Vertical Slice Architecture** pattern.

*   **/Features/Shared:** Contains shared classes used across multiple features, such as `RequestResponse<T>`, `EndpointResponse<T>`, and `PaginatedResult<T>`.
*   **/Features/Workouts/GetAllWorkouts:** Contains all logic for fetching a paginated list of all workouts.
*   **/Features/Workouts/GetWorkoutDetails:** Contains the logic for fetching the full details of a single workout.
*   **/Features/Workouts/GetWorkoutsByCategory:** Contains the logic for fetching workouts filtered by a specific category.
*   **/Features/Workouts/StartWorkoutSession:** Contains the logic for a user to start a new workout session.

## 3. API Endpoints

The following API endpoints have been implemented in the `WorkoutService`:

| Method | Endpoint                                   | Description                                                        |
| :----- | :----------------------------------------- | :----------------------------------------------------------------- |
| `GET`  | `/api/v1/workouts`                         | Retrieves a paginated list of all workouts with optional filters.  |
| `GET`  | `/api/v1/workouts/{id}`                    | Retrieves the detailed information for a single workout by its ID. |
| `GET`  | `/api/v1/workouts/category/{categoryName}` | Retrieves a paginated list of workouts belonging to a category.    |
| `POST` | `/api/v1/workouts/{id}/start`              | Starts a new workout session for the authenticated user.           |

## 4. Current Folder Structure

The high-level folder structure for the `WorkoutService` is as follows:

```
WorkoutService/
├── Domain/
│   ├── Entities/         # Contains the database entity classes (e.g., Workout, Exercise).
│   └── Interfaces/       # Contains repository interfaces (e.g., IBaseRepository).
├── Features/
│   ├── Shared/           # Shared response and utility classes.
│   └── Workouts/
│       ├── GetAllWorkouts/
│       ├── GetWorkoutDetails/
│       └── ...           # Other feature folders.
├── Infrastructure/
│   ├── Data/             # Contains the DbContext and database seeder.
│   └── Repositories/     # Concrete repository implementations.
├── Migrations/           # Entity Framework Core database migrations.
├── Properties/           # Project configuration (e.g., launchSettings.json).
├── EndpointExtensions.cs # Extension method to automatically register all endpoints.
├── Program.cs            # The main application entry point.
└── appsettings.json      # Application configuration.
```

## 5. Endpoint Implementation Strategy

The implementation of each endpoint follows a consistent and modern architectural pattern to ensure the code is decoupled, maintainable, and easy to test.

### Core Concepts:

1.  **CQRS (Command Query Responsibility Segregation):**
    The pattern separates read and write operations.
    *   **Queries:** Used for read operations. They fetch data and shape it for the client but do not change the state of the system. (e.g., `GetAllWorkoutsQuery`).
    *   **Commands:** Used for write operations. They are responsible for changing the state of the system (e.g., creating, updating, deleting data). (e.g., `StartWorkoutSessionCommand`).

2.  **Vertical Slice Architecture:**
    Instead of organizing code by technical layers (e.g., `Controllers`, `Services`, `Repositories`), we organize it by feature. Each feature folder (or "slice") is self-contained and holds all the code it needs to function. A typical slice includes:
    *   The **Command** or **Query** record.
    *   The **Handler** class that contains the business logic.
    *   Associated **DTOs** (Data Transfer Objects) or **ViewModels**.
    *   The **Endpoint** definition file.

3.  **MediatR Library:**
    MediatR is the glue that makes this pattern work. It is a simple mediator implementation that allows us to send a Command or Query object from our endpoint and have it dispatched to the correct Handler without the endpoint needing to know which handler exists or where it is. This keeps the endpoint thin and focused on HTTP concerns.

### How a Request is Processed (Step-by-Step):

1.  **Endpoint Definition (`Endpoints.cs`):** An incoming HTTP request (e.g., `GET /api/v1/workouts`) is mapped to a specific Minimal API endpoint. This endpoint's only job is to:
    a. Create the appropriate **Query** or **Command** object with data from the request (e.g., route parameters, query strings, request body).
    b. Use MediatR to `mediator.Send()` the object.
    c. Receive the result from the handler.
    d. Map the result to a standardized `EndpointResponse` and return it as an HTTP response (e.g., `Results.Ok()` or `Results.BadRequest()`).

2.  **Handler (`Handlers.cs`):** MediatR routes the Command/Query to its corresponding Handler. The Handler contains the core business logic and is responsible for:
    a. Receiving the request object.
    b. Interacting with the database via injected repositories (`IBaseRepository<T>`).
    c. Performing any necessary business logic, validation, or data mapping.
    d. Returning the result wrapped in a standardized `RequestResponse<T>`, which indicates success or failure and contains the data or an error message.
