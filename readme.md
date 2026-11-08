# Equipment API

An equipment management Web API built with ASP.NET Core. A learning project.

## Features

- CRUD operations for equipment
- Dependency Injection (DI): `IEquipmentService` / `EquipmentService`
- DTOs: the API returns `EquipmentDto` instead of exposing entities directly
- Logging and 404 handling

## Tech Stack

- .NET / ASP.NET Core
- C#

## How to Run

```bash
cd EquipmentApi
dotnet run


EquipmentApi/
├── Controllers/      # API controllers
├── Dtos/             # Data transfer objects
├── Models/           # Entities
├── Services/         # Business logic
├── Program.cs        # Entry point
└── EquipmentApi.http # Test requests
## Endpoints

| Method | Path | Description |
| ------ | ---- | ----------- |
| GET | /api/Equipment | Get all equipment |
| GET | /api/Equipment/{id} | Get one equipment item; returns 404 if not found |
| POST | /api/Equipment | Create equipment; returns 201 on success |
| PUT | /api/Equipment/{id} | Update equipment; returns 404 if not found |
| DELETE | /api/Equipment/{id} | Delete equipment; returns 204 on success |
