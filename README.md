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
- Observability: OpenTelemetry Metrics

## Projektstruktur

```
regatta-buero/
├── src/
│   ├── LRV.Regatta.Buero/              # Hauptanwendung (Web API)
│   │   ├── Attributes/                 # ApiKeyAttribute (API-Key-Authentifizierung)
│   │   ├── Controllers/                # FinishController, LogController, LoginController,
│   │   │                               #   RegistrationController, TeamController
│   │   ├── Interfaces/                 # IFinishService, ILogService, IRegistrationService
│   │   ├── Migrations/                 # EF-Core-Datenbankmigrationen
│   │   ├── Models/                     # Datenmodelle (FinishObject, LogObject, TeamObject, …)
│   │   ├── Services/                   # DatabaseContext, FinishService, LogService,
│   │   │                               #   RegistrationService
│   │   └── Program.cs
│   └── LRV.Regatta.Buero.Tests/        # Unit-Tests (MSTest)
│       ├── Controllers/                # Controller-Tests
│       ├── Helpers/                    # DatabaseContextFactory
│       ├── Mocks/                      # MockLogService, MockRegistrationService
│       └── Service/                    # Service-Tests
└── tools/
    └── docker-compose.yml              # Lokale Infrastruktur (MariaDB, phpMyAdmin, Redis, RedisInsight)
```

## Voraussetzungen

- .NET SDK 8
- Docker + Docker Compose (empfohlen fuer lokale Infrastruktur)

## Lokaler Start

### 1. Infrastruktur starten

```bash
cp tools/.env.example tools/.env
docker compose --env-file tools/.env -f tools/docker-compose.yml up -d
```

Verfuegbare Services:

| Service       | URL / Adresse             |
|---------------|---------------------------|
| MariaDB       | `localhost:3306`          |
| phpMyAdmin    | `http://localhost:8080`   |
| Redis         | `localhost:6379`          |
| RedisInsight  | `http://localhost:5540`   |

### 2. API starten

```bash
dotnet restore "src/LRV Regattabuero.sln"
dotnet run --project "src/LRV.Regatta.Buero/LRV.Regatta.Buero.csproj"
```

Standard-URL (Development): `http://localhost:5015`

Swagger ist in Development unter `http://localhost:5015/swagger` erreichbar.

> **Hinweis:** Beim Start werden ausstehende EF-Core-Migrationen automatisch angewendet.

## Konfiguration

### Umgebungsvariablen

| Variable               | Beschreibung                         | Standard             |
|------------------------|--------------------------------------|----------------------|
| `DB_HOST`              | Datenbankhost                        | `localhost`          |
| `DB_PORT`              | Datenbankport                        | `3306`               |
| `DB_NAME`              | Datenbankname                        | `regatta_database`   |
| `DB_USER`              | Datenbankbenutzer                    | `regatta`            |
| `DB_PASSWORD`          | Datenbankpasswort                    | `regatta`            |
| `API_KEY`              | API-Key fuer gesicherte Endpunkte    | –                    |
| `X_API_KEY`            | API-Key (Legacy-Fallback)            | –                    |
| `JWT_KEY`              | Geheimer Schluessel fuer JWT         | –                    |
| `JWT_ISSUER`           | JWT Issuer                           | –                    |
| `JWT_AUDIENCE`         | JWT Audience                         | –                    |
| `JWT_EXPIRES_MINUTES`  | JWT-Ablaufzeit in Minuten            | –                    |
| `REDIS_HOST`           | Redis-Host                           | –                    |
| `REDIS_PORT`           | Redis-Port                           | –                    |
| `ImageFolder`          | Ablageordner fuer Zieleinlauf-Bilder | –                    |

### appsettings.json

Alle obigen Werte koennen alternativ ueber `appsettings.json` konfiguriert werden.  
Umgebungsvariablen haben dabei Vorrang.

## API-Endpunkte

Basisroute: `api/{Controller}`

### Oeffentlich

| Methode | Route         | Beschreibung                                     |
|---------|---------------|--------------------------------------------------|
| `GET`   | `/api/Login`  | Prueft Login-Status                              |
| `POST`  | `/api/Login`  | Liefert JWT-Token (Demo: `testuser` / `password`)|

### Mit API-Key (`X-API-KEY` Header erforderlich)

#### Registration

| Methode | Route                | Beschreibung                      |
|---------|----------------------|-----------------------------------|
| `GET`   | `/api/Registration`  | Alle Meldungen abrufen            |
| `PUT`   | `/api/Registration`  | Neue Meldung hinzufuegen          |

#### Team

| Methode | Route                          | Beschreibung                        |
|---------|--------------------------------|-------------------------------------|
| `POST`  | `/api/Team`                    | Vereine per XML-Import hochladen    |
| `GET`   | `/api/Team`                    | Alle Vereine abrufen                |
| `GET`   | `/api/Team/select?teamName=…`  | Verein nach Name suchen             |
| `DELETE`| `/api/Team`                    | Alle Vereinsdaten loeschen          |

**XML-Format fuer Team-Import:**

```xml
<RegattaMeldungen>
  <Vereine>
    <TeamObject>
      <Name>Ludwigsburger Ruderverein</Name>
      <Kurzform>LRV</Kurzform>
      <Lettern>L</Lettern>
    </TeamObject>
  </Vereine>
</RegattaMeldungen>
```

#### Finish (Zieleinlauf)

| Methode | Route               | Beschreibung                                  |
|---------|---------------------|-----------------------------------------------|
| `GET`   | `/api/Finish`       | Alle Zieleinlauf-Objekte abrufen              |
| `POST`  | `/api/Finish`       | Neuen Zieleinlauf hochladen (`multipart/form-data`, inkl. Bild) |
| `DELETE`| `/api/Finish`       | Alle Zieleinlauf-Objekte loeschen             |
| `DELETE`| `/api/Finish/{id}`  | Einzelnen Zieleinlauf loeschen                |

#### Log

| Methode | Route                               | Beschreibung                                          |
|---------|-------------------------------------|-------------------------------------------------------|
| `POST`  | `/api/Log`                          | Log-Eintraege senden (Liste von `LogObject`)          |
| `GET`   | `/api/Log`                          | Alle Log-Eintraege abrufen                            |
| `GET`   | `/api/Log/search?page=1&pageSize=10`| Paginierte Log-Eintraege abrufen                      |

**Log-Request-Header (werden serverseitig ausgewertet):**

| Header              | Beschreibung                  |
|---------------------|-------------------------------|
| `X-Client-Name`     | Name des sendenden Clients    |
| `X-Client-Version`  | Version des sendenden Clients |
| `X-Forwarded-For`   | Client-IP (Proxy-Weiterleitung)|

## Health Check

```
GET /health
```

Prueft den Status folgender Abhaengigkeiten:

- Redis
- MySQL / MariaDB

## Entwicklung

**Build:**

```bash
dotnet build "src/LRV Regattabuero.sln"
```

**Publish:**

```bash
dotnet publish "src/LRV Regattabuero.sln"
```

## Tests

```bash
dotnet test "src/LRV.Regatta.Buero.Tests/LRV.Regatta.Buero.Tests.csproj"
```

### Vorhandene Tests

**Controller-Tests:**

- `FinishControllerTests`
- `LogControllerTests`
- `RegistrationControllerTests`
- `TeamControllerTests`

**Service-Tests:**

- `FinishServiceTests`
- `LogServiceTests`
- `RegistrationServiceTests`

### Code Coverage

Coverage als Cobertura-Report erzeugen:

```bash
rm -rf artifacts/test-results artifacts/coverage-report
dotnet test "src/LRV.Regatta.Buero.Tests/LRV.Regatta.Buero.Tests.csproj" --settings "coverage.runsettings" --collect:"XPlat Code Coverage" --results-directory "artifacts/test-results"
```

Der Coverage-Report wird unter `artifacts/test-results/<run-id>/coverage.cobertura.xml` abgelegt.  
Die Datei `coverage.runsettings` schliesst den autogenerierten Namespace `LRV.Regatta.Buero.Migrations` aus.

**HTML-Report lokal erzeugen:**

```bash
dotnet tool restore
rm -rf artifacts/test-results artifacts/coverage-report
dotnet test "src/LRV.Regatta.Buero.Tests/LRV.Regatta.Buero.Tests.csproj" --settings "coverage.runsettings" --collect:"XPlat Code Coverage" --results-directory "artifacts/test-results"
dotnet tool run reportgenerator -reports:"artifacts/test-results/**/coverage.cobertura.xml" -targetdir:"artifacts/coverage-report" -reporttypes:"Html;TextSummary"
```

Der lesbare Bericht liegt danach unter `artifacts/coverage-report/index.html`.

In VS Code kann der Ablauf auch ueber Tasks gestartet werden:

```
Tasks: Run Task -> coverage report and serve
```

Danach den Report im Simple Browser mit `http://localhost:8000/` oeffnen.  
Zum Beenden des lokalen Servers steht der Task `stop coverage report` zur Verfuegung.

In GitHub Actions wird derselbe Report erzeugt und als Artefakt `coverage-report` hochgeladen.
