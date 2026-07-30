# # Slab Ingestion Service

## Overview

Slab Ingestion Service is a .NET 8 Web API developed to ingest and manage steel slab events. The application maintains a single, consistent record for each slab, supports querying slab information, and demonstrates optimistic concurrency handling when multiple updates are received simultaneously.

---

# Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

---

# Project Setup

## Prerequisites

- Visual Studio 2022
- .NET 8 SDK
- SQL Server

## Clone Repository

```bash
git clone <repository-url>
```

## Configure Database

Update the SQL Server connection string inside **appsettings.json**.

```json
"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server Connection String"
}
```

## Apply Migrations

```powershell
Update-Database
```

## Run the Project

```powershell
dotnet run
```

Swagger UI will open automatically.

---

# API Endpoints

## 1. Ingest Slab

**POST**

```
/api/slabs/ingest
```

### Sample Request

```json
{
  "slabId": "HSM-001",
  "weight": 22000,
  "length": 11000,
  "width": 1200,
  "status": "Rolled"
}
```

---

## 2. Get Slab By Id

**GET**

```
/api/slabs/{slabId}
```

Example

```
GET /api/slabs/HSM-001
```

---

## 3. Get Slabs

**GET**

```
/api/slabs?status=Rolled&from=2026-07-01&to=2026-07-31
```

Supports filtering by:

- Status
- From Date
- To Date

---

# Part 1 – Core API & Data Model

Implemented the following requirements:

- Slab Entity
- Entity Framework Core (Code First)
- SQL Server Database
- Create or Update (Upsert) functionality
- Get Slab by SlabId
- Filter Slabs by Status and Date Range
- DTOs
- Service Layer Architecture
- Swagger Documentation
- EF Core Migrations

---

# Part 2 – Concurrency Handling

## Concurrency Strategy

Implemented **Optimistic Concurrency** using Entity Framework Core's **RowVersion** concurrency token.

```csharp
[Timestamp]
public byte[] RowVersion { get; set; } = Array.Empty<byte>();
```

The `RowVersion` column ensures that concurrent updates are detected before saving changes, preventing silent overwrites and maintaining data consistency.

## Concurrency Demonstration

A separate **Console Application** was created to simulate concurrent updates.

The application sends **20 concurrent POST requests** for the same `SlabId`.

Example:

```
SlabId : HSM-001
```

### Result

- Successfully simulated 20 concurrent ingest requests.
- Only one slab record exists after all requests completed.
- The slab remained in a valid final state.
- No partial updates or data corruption occurred.
- Concurrent updates were handled using EF Core Optimistic Concurrency.

---

# Part 3 – Performance Optimization

## Problems in the Original Implementation

The original implementation had the following performance issues:

- Loaded all slabs into memory before filtering.
- Filtered records in memory instead of SQL Server.
- Suffered from the **N+1 Query Problem**, executing one additional query for each slab.
- Used change tracking for a read-only query.

## Optimized Version

```csharp
public async Task<List<SlabDto>> GetTodaysSlabsAsync()
{
    var today = DateTime.Today;
    var tomorrow = today.AddDays(1);

    var result = await
    (
        from slab in _context.Slabs.AsNoTracking()
        where slab.UpdatedAt >= today &&
              slab.UpdatedAt < tomorrow

        join log in _context.SlabStatusLog
            on slab.SlabId equals log.SlabId into statusLogs

        select new SlabDto
        {
            SlabId = slab.SlabId,
            Status = slab.Status,
            LastEvents = statusLogs.Count()
        }
    ).ToListAsync();

    return result;
}
```

## Improvements Made

- Used **AsNoTracking()** to improve read-only query performance.
- Replaced `UpdatedAt.Date == DateTime.Today` with an **index-friendly date range** (`>= today && < tomorrow`) to allow SQL Server to utilize indexes more effectively.
- Eliminated the **N+1 Query Problem** by using a **Group Join**, avoiding separate database queries for each slab.
- Projected only the required fields into `SlabDto`, reducing memory usage and improving query efficiency.
- Generated a **single SQL query**, reducing database round trips and improving overall performance.

---

# Project Structure

```
SlabIngestionService.API
│
├── Controllers
├── Data
├── DTOs
├── Enums
├── Models
├── Services
├── Migrations
├── Program.cs
└── appsettings.json

SlabIngestionService.ConcurrencyTest
│
└── Program.cs
```

---

# Future Enhancements

- JWT Authentication & Authorization
- Logging with Serilog
- Unit Testing
- Integration Testing
- Docker Support
- CI/CD Pipeline
- Pagination
- Response Caching

---

# Author

**Nagesh Mendhe**

Backend Developer | .NET | ASP.NET Core | SQL Server | Entity Framework Core
