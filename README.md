# Regatta-Buero

[![.NET](https://github.com/Sanifant/regatta-buero/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Sanifant/regatta-buero/actions/workflows/dotnet.yml)
[![Regattabureo Backend](https://github.com/Sanifant/regatta-buero/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/Sanifant/regatta-buero/actions/workflows/docker-publish.yml)

Backend-Service fuer das Regatta-Buero auf Basis von ASP.NET Core 8, MariaDB und Redis.

## Ueberblick

Das Projekt stellt REST-Endpunkte fuer Meldungen, Vereine, Zieleinlaeufe und Logs bereit.

- Framework: ASP.NET Core 8 (`net8.0`)
- Datenbank: MariaDB via Entity Framework Core + Pomelo
- Caching/Health: Redis Health Check
- API-Dokumentation: Swagger (im Development-Modus)
- Authentifizierung:
  - `X-API-KEY` fuer gesicherte Controller
  - JWT-Token ueber Login-Endpunkt

## Projektstruktur

- `src/LRV.Regatta.Buero`: Hauptanwendung (Web API)
- `src/LRV.Regatta.Buero.Tests`: Unit-Tests (MSTest)
- `tools/docker-compose.yml`: Lokale Infrastruktur (MariaDB, phpMyAdmin, Redis, RedisInsight)

## Voraussetzungen

- .NET SDK 8
- Docker + Docker Compose (empfohlen fuer lokale Infrastruktur)

## Lokaler Start

### 1. Infrastruktur starten

```bash
docker compose -f tools/docker-compose.yml up -d
```

Verfuegbare Services:

- MariaDB: `localhost:3306`
- phpMyAdmin: `http://localhost:8080`
- Redis: `localhost:6379`
- RedisInsight: `http://localhost:5540`

### 2. API starten

```bash
dotnet restore "src/LRV Regattabuero.sln"
dotnet run --project "src/LRV.Regatta.Buero/LRV.Regatta.Buero.csproj"
```

Standard-URL (Development): `http://localhost:5015`

Swagger ist in Development unter `http://localhost:5015/swagger` erreichbar.

## Konfiguration

Wichtige Einstellungen per Umgebungsvariablen:

- Datenbank:
  - `DB_HOST` (Standard: `localhost`)
  - `DB_PORT` (Standard: `3306`)
  - `DB_NAME` (Standard: `regatta_database`)
  - `DB_USER` (Standard: `regatta`)
  - `DB_PASSWORD` (Standard: `regatta`)
- API-Key:
  - `X-API-KEY`
- JWT:
  - `JWT_KEY`
  - `JWT_ISSUER`
  - `JWT_AUDIENCE`
  - `JWT_EXPIRES_MINUTES`
- Redis:
  - `REDIS_HOST`
  - `REDIS_PORT`

Wichtige Einstellungen per `appsettings.json`.

- Bildablage:
  - `ImageFolder` (z. B. `/wwwdata/images`)

Hinweis: Beim Start werden ausstehende EF-Core-Migrationen automatisch angewendet.

## API-Endpunkte (Auszug)

Basisroute: `api/{Controller}`

### Oeffentlich

- `GET /api/Login`
- `POST /api/Login`
  - Demo-Login: `testuser` / `password`
  - Liefert JWT Token

### Mit API-Key (`X-API-KEY`)

- Registration
  - `GET /api/Registration`
  - `PUT /api/Registration`
- Team
  - `POST /api/Team` (XML-Import)
  - `GET /api/Team`
  - `GET /api/Team/select?teamName=...`
  - `DELETE /api/Team`
- Finish
  - `GET /api/Finish`
  - `POST /api/Finish` (`multipart/form-data`)
  - `DELETE /api/Finish`
  - `DELETE /api/Finish/{id}`
- Log
  - `POST /api/Log`
  - `GET /api/Log`
  - `GET /api/Log/search?page=1&pageSize=10`

## Health Check

- `GET /health`

Der Endpunkt prueft aktuell:

- Redis
- MySQL/MariaDB

## Entwicklung

Build:

```bash
dotnet build "src/LRV Regattabuero.sln"
```

Publish:

```bash
dotnet publish "src/LRV Regattabuero.sln"
```

## Tests

```bash
dotnet test "src/LRV.Regatta.Buero.Tests/LRV.Regatta.Buero.Tests.csproj"
```

Coverage als Cobertura-Report erzeugen:

```bash
rm -rf artifacts/test-results artifacts/coverage-report
dotnet test "src/LRV.Regatta.Buero.Tests/LRV.Regatta.Buero.Tests.csproj" --settings "coverage.runsettings" --collect:"XPlat Code Coverage" --results-directory "artifacts/test-results"
```

Der Coverage-Report wird anschliessend unter `artifacts/test-results/<run-id>/coverage.cobertura.xml` abgelegt.
Die Datei `coverage.runsettings` schliesst aktuell den autogenerierten Namespace `LRV.Regatta.Buero.Migrations` aus.

HTML-Report lokal erzeugen:

```bash
dotnet tool restore
rm -rf artifacts/test-results artifacts/coverage-report
dotnet test "src/LRV.Regatta.Buero.Tests/LRV.Regatta.Buero.Tests.csproj" --settings "coverage.runsettings" --collect:"XPlat Code Coverage" --results-directory "artifacts/test-results"
dotnet tool run reportgenerator -reports:"artifacts/test-results/**/coverage.cobertura.xml" -targetdir:"artifacts/coverage-report" -reporttypes:"Html;TextSummary"
```

Der lesbare Bericht liegt danach unter `artifacts/coverage-report/index.html`.

In VS Code kannst du den Ablauf auch ueber Tasks starten:

```text
Tasks: Run Task -> coverage report and serve
```

Danach den Report im Simple Browser mit `http://localhost:8000/` oeffnen.
Zum Beenden des lokalen Servers steht der Task `stop coverage report` zur Verfuegung.

In GitHub Actions wird derselbe Report ebenfalls erzeugt und als Artefakt `coverage-report` hochgeladen.

Vorhandene Controller-Tests:

- FinishController
- RegistrationController
- TeamController
