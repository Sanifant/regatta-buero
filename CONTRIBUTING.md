# Contributing to Regatta-Buero

Thank you for taking the time to contribute! The following guidelines help
keep the codebase consistent and the review process smooth.

---

## Table of Contents

1. [Code of Conduct](#code-of-conduct)
2. [Getting Started](#getting-started)
3. [Reporting Bugs](#reporting-bugs)
4. [Requesting Features](#requesting-features)
5. [Development Setup](#development-setup)
6. [Branch Strategy](#branch-strategy)
7. [Commit Messages](#commit-messages)
8. [Pull Requests](#pull-requests)
9. [Coding Standards](#coding-standards)
10. [Running Tests](#running-tests)

---

## Code of Conduct

This project follows our [Code of Conduct](CODE_OF_CONDUCT.md).
All contributors are expected to uphold it.

---

## Getting Started

1. Fork the repository and clone your fork.
2. Open the project in the provided Dev Container (VS Code + Docker required).
   The container starts MariaDB, Redis and the .NET 8 toolchain automatically.
3. Run `dotnet restore` inside `src/` to install all dependencies.

---

## Reporting Bugs

Use the **Bug Report** issue template:

> [Create a bug report](https://github.com/Sanifant/regatta-buero/issues/new?template=bug_report.md)

Please include:

- A clear description and steps to reproduce
- Expected vs. actual behavior
- Environment details (OS, .NET version, Docker version)

---

## Requesting Features

Use the **Feature Request** issue template:

> [Create a feature request](https://github.com/Sanifant/regatta-buero/issues/new?template=feature_request.md)

---

## Development Setup

Start the local infrastructure (MariaDB, phpMyAdmin, Redis, RedisInsight):

```bash
docker compose -f tools/docker-compose.yml up -d
```

Run the API locally:

```bash
dotnet run --project src/LRV.Regatta.Buero/LRV.Regatta.Buero.csproj
```

Swagger UI is available at `http://localhost:5015/swagger` in Development mode.

The default development API key is `37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12`
(set via the `X-API-KEY` header or environment variable).

---

## Branch Strategy

- `main` — stable, production-ready code. Direct commits are not allowed.
- Feature / fix work is done on short-lived branches:

```text
feat/<short-description>
fix/<short-description>
chore/<short-description>
docs/<short-description>
```

Create a branch from `main`, do your work, then open a pull request back
into `main`.

---

## Commit Messages

This project follows [Conventional Commits](https://www.conventionalcommits.org/).

Format:

```text
<type>(<optional scope>): <short summary>

[optional body]

[optional footer(s)]
```

Valid types:

| Type       | When to use                                |
|------------|--------------------------------------------|
| `feat`     | A new feature                              |
| `fix`      | A bug fix                                  |
| `docs`     | Documentation changes only                 |
| `test`     | Adding or updating tests                   |
| `refactor` | Code change that is neither fix nor feat   |
| `chore`    | Build process, dependency updates, tooling |
| `ci`       | Changes to CI/CD workflows                 |

Examples:

```text
feat(log): add paginated log endpoint
fix(auth): correct ClientIp assignment logic in LogController
chore(deps): bump Pomelo.EntityFrameworkCore.MySql to 8.0.4
```

---

## Pull Requests

1. Open a PR against `main` from your feature branch.
2. Fill in the PR description — what does it change and why?
3. **At least one reviewer must approve** before merging.
4. All CI checks (build + tests) must pass.
5. Keep PRs focused: one feature or fix per PR.
6. Rebase or merge the latest `main` into your branch before requesting review.

---

## Coding Standards

- Target framework: **.NET 8** (`net8.0`)
- Language version: **C# latest** with nullable reference types enabled.
- Follow the existing code style (4-space indentation, XML doc comments on
  public members).
- Do not commit secrets, API keys, or passwords.
  Use environment variables for all sensitive values (see `appsettings.json`
  for the expected key names).
- Keep controllers thin — business logic belongs in services implementing the
  `IFinishService`, `IRegistrationService`, or `ILogService` interfaces.

---

## Running Tests

```bash
dotnet test "src/LRV.Regatta.Buero.Tests/LRV.Regatta.Buero.Tests.csproj"
```

Please add or update tests for every code change. The test project uses
**MSTest** and references the main project directly — no external test
infrastructure is required.
