# Equipment API

An equipment management Web API built with ASP.NET Core. A learning project.

## Features

- CRUD operations for equipment
- Dependency Injection (DI): `IEquipmentService` / `EquipmentService`
- DTOs: the API returns `EquipmentDto` instead of exposing entities directly
- Logging and 404 handling
- EF Core + SQLite persistence (Code First migrations); data survives restarts
- One-to-many equipment logs: paginated logs and error alerts per device
- Alert rules in a separate `AlertService`: alerts when Error logs in the last 24 hours exceed a configurable threshold (`AlertThreshold` in appsettings.json)

## Tech Stack

- .NET / ASP.NET Core
- C#
- Entity Framework Core
- SQLite

## How to Run

```bash
cd EquipmentApi
dotnet run


Project Structure

EquipmentApi/
├── Controllers/      # API controllers
├── Data/             # AppDbContext
├── Dtos/             # Data transfer objects
├── Migrations/       # EF Core migrations
├── Models/           # Entities
├── Services/         # Business logic
├── Program.cs        # Entry point
└── EquipmentApi.http # Test requests


Endpoints
Method	Path	Description
GET	/api/Equipment	Get all equipment
GET	/api/Equipment/{id}	Get one equipment item; returns 404 if not found
POST	/api/Equipment	Create equipment; returns 201 on success
PUT	/api/Equipment/{id}	Update equipment; returns 404 if not found
DELETE	/api/Equipment/{id}	Delete equipment; returns 204 on success
GET	/api/Equipment/{id}/logs	Get logs for one device, newest first, paginated
GET	/api/Equipment/{id}/alerts	Error count and alert status for one device in the last 24 hours
POST	/api/Equipment/{id}/logs	Add a log to one device; returns 201 on success


SQL Notes
1. JOIN — which device does each log belong to

SELECT e.Name, l.Message, l.CreatedAt
FROM Equipment e
JOIN LogEntries l ON l.EquipmentId = e.Id
WHERE l.Level = 'Error';


Result: 2 rows — "Motor overheating" and "Temperature high", both on the same device.
Matches GET /api/Equipment/1/logs.

EF equivalent: _context.Equipment.Include(e => e.Logs), or query LogEntries and read l.Equipment.Name — EF generates the JOIN for you.

2. GROUP BY — error count per device

SELECT EquipmentId, COUNT(*) AS ErrorCount
FROM LogEntries
WHERE Level = 'Error'
GROUP BY EquipmentId;
SELECT EquipmentId, COUNT(*) AS ErrorCount
FROM LogEntries
WHERE Level = 'Error'
GROUP BY EquipmentId;

Result: EquipmentId 1 → ErrorCount 2.
Matches GET /api/Equipment/1/alerts (errorCount: 2).

EF equivalent:

_context.LogEntries
    .Where(l => l.Level == "Error")
    .GroupBy(l => l.EquipmentId)
    .Select(g => new { EquipmentId = g.Key, ErrorCount = g.Count() });


3. Index — make the logs query faster

CREATE INDEX IX_LogEntry_Equipment_Timestamp ON LogEntries (EquipmentId, CreatedAt);


EXPLAIN QUERY PLAN before creating the index:

SEARCH LogEntries USING INDEX IX_LogEntries_EquipmentId (EquipmentId=?)
USE TEMP B-TREE FOR ORDER BY


After:

SEARCH LogEntries USING INDEX IX_LogEntry_Equipment_Timestamp (EquipmentId=?)


The foreign key column already had an auto-created index (EF Core creates indexes for foreign keys), so the lookup was already indexed. The composite index also covers ORDER BY CreatedAt DESC, so the temporary sort (TEMP B-TREE) is gone.

Note: this index was created directly in the local equipment.db for learning. To keep it in code, add HasIndex in OnModelCreating and generate a migration.

LINQ to SQL cheat sheet
LINQ	SQL
.Where(...)	WHERE
.OrderByDescending(...)	ORDER BY ... DESC
.Skip(n).Take(m)	LIMIT m OFFSET n
.Include(e => e.Logs)	JOIN
.GroupBy(...) + .Count()	GROUP BY + COUNT(*)

