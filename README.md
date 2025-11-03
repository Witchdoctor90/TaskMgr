# TaskMgr

TaskMgr is a web API built on [translate:ASP.NET Core] designed to manage tasks and routines with a clean layered architecture pattern.

---

## Overview

This project provides full CRUD (Create, Read, Update, Delete) operations for user-specific tasks and routines. It uses the CQRS pattern with commands and queries, ensuring separation of concerns, security via user verification, and exception handling.

---

## Key Features

- Manage tasks and routines belonging to individual users.
- CRUD operations with permission checks.
- Custom exception handling (e.g., TaskEntityNotFoundException).
- Structured with layers: Application, Domain, Infrastructure, WebApi.
- Uses MediatR for CQRS and in-process messaging.
- Object mapping via profiles for data transfer objects (DTOs).

---

## Technologies Used

- [translate:C#], [.NET 8]
- [translate:ASP.NET Core] Web API
- Entity Framework Core (assumed for data access)
- MediatR for CQRS pattern implementation
- Swagger for API documentation and testing

---

## API Endpoints

### Tasks

| Method | URL                  | Description                          |
|--------|----------------------|------------------------------------|
| GET    | /api/tasks           | Get all tasks for the authenticated user |
| GET    | /api/tasks/{id}      | Get a specific task by ID           |
| POST   | /api/tasks           | Create a new task                   |
| PUT    | /api/tasks/{id}      | Update an existing task             |
| DELETE | /api/tasks/{id}      | Delete a task                      |

### Routines

| Method | URL                  | Description                          |
|--------|----------------------|------------------------------------|
| GET    | /api/routines        | Get all routines for the authenticated user |
| GET    | /api/routines/{id}   | Get a specific routine by ID        |
| POST   | /api/routines        | Create a new routine                |
| PUT    | /api/routines/{id}   | Update an existing routine          |
| DELETE | /api/routines/{id}   | Delete a routine                   |

---

## Example Command and Query Structure

- **Commands** such as `AddTaskCommand`, `UpdateTaskCommand`, `DeleteTaskCommand` handle mutations.
- **Queries** such as `GetAllTasksQuery`, `GetTaskByIdQuery` retrieve data.
- Each command/query validates user permissions before acting.

---

## Installation and Running
1. Clone the repository
   
  `git clone https://github.com/Witchdoctor90/TaskMgr.git`  

  `cd TaskMgr`  

2. Configure the database connection (likely via appsettings).
3. Run migrations (if applicable).
4. Build and run the project using:  
 `dotnet run --project TaskMgr.WebApi`

---

## Contribution

Contributions are welcome! Submit issues or pull requests to improve the project.

---

## License

MIT License — see LICENSE file
