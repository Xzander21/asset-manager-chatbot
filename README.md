# AV Equipment Manager

A complete AV Equipment Management System built with **Blazor WebAssembly**, **ASP.NET Core Web API**, and **MudBlazor** UI components. Designed to be convertible to a mobile app via MAUI Blazor Hybrid.

## Project Structure

```
AVEquipmentManager.sln
├── src/
│   ├── AVEquipmentManager.Shared/   # Models, DTOs, Enums (Class Library)
│   ├── AVEquipmentManager.API/      # ASP.NET Core Web API + EF Core + SQLite
│   └── AVEquipmentManager.Web/      # Blazor WebAssembly (MudBlazor UI)
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Running the Application

### 1. Run the API

```bash
cd src/AVEquipmentManager.API
dotnet run
```

The API will start at `http://localhost:5033` (or `https://localhost:7127`).  
Swagger UI is available at `http://localhost:5033/swagger`.

> On first run, the SQLite database (`avequipment.db`) is created automatically and seeded with sample data.

### 2. Run the Web (Blazor WASM)

In a separate terminal:

```bash
cd src/AVEquipmentManager.Web
dotnet run
```

The web app will start at `http://localhost:5173` (or `https://localhost:7173`).

> Make sure the API is running before using the web app. The API URL is configured in `src/AVEquipmentManager.Web/wwwroot/appsettings.json`.

### Default URLs

| Project | HTTP | HTTPS |
|---------|------|-------|
| API     | http://localhost:5033 | https://localhost:7127 |
| Web     | http://localhost:5072 | https://localhost:7022 |

## Features

### Dashboard
- Summary cards showing total, active, under maintenance, and retired/decommissioned equipment counts
- Filterable and searchable equipment table with sortable columns
- Add, Edit, and Delete equipment via dialogs
- Colored status chips (Green=Active, Orange=UnderMaintenance, Red=Retired, Grey=Decommissioned)
- Remaining life calculation

### Chatbot
- Rule-based keyword chatbot (no AI/ML)
- Ask about equipment by room ("Room 1", "Room 2", "Room 3")
- Look up by serial number ("AV-R1-001")
- Filter by status ("active", "maintenance", "retired")
- Get summary/overview
- Quick action chips for common queries

## Modifying Seed Data

The seed data is located in:

```
src/AVEquipmentManager.API/Data/SeedData.cs
```

Edit the `Initialize` method to change the default equipment entries. The seed data is only applied when the database is empty (fresh start). To re-seed, delete the `avequipment.db` file and restart the API.

## Configuration

**API** (`src/AVEquipmentManager.API/appsettings.json`):
- `ConnectionStrings:DefaultConnection` — SQLite database file path
- `CorsOrigins:BlazorClient` — Blazor WASM client URL

**Web** (`src/AVEquipmentManager.Web/wwwroot/appsettings.json`):
- `ApiBaseUrl` — API base URL

## Future Roadmap

- **MAUI Blazor Hybrid**: Convert the Blazor WASM frontend to a MAUI Blazor Hybrid app for cross-platform mobile support (iOS, Android, Windows, macOS)
- **Authentication**: Add JWT-based authentication
- **Notifications**: Email/push notifications for equipment nearing end-of-life
- **Reports**: Export equipment reports to PDF/Excel
- **Audit Log**: Track changes to equipment records
