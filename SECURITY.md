# Security Policy

## Supported Versions

This project does not follow a fixed versioning scheme yet. Security fixes
are applied to the current state of the `main` branch. We recommend always
using the latest available image or build from `main`.

## Reporting a Vulnerability

If you discover a security vulnerability, please open a
[GitHub Issue](https://github.com/Sanifant/regatta-buero/issues/new?labels=security&template=bug_report.md)
and add the label **security**.

For reports where public disclosure would be harmful before a fix is available,
please create a
[GitHub Security Advisory](https://github.com/Sanifant/regatta-buero/security/advisories/new)
instead.

**Response time:** We will address security reports on a best-effort basis.
There is no guaranteed response window, but we aim to acknowledge and
triage all reports as soon as possible.

**What to include:**

- A clear description of the vulnerability
- Steps to reproduce
- Potential impact assessment
- Suggested fix (optional)

After a report is accepted, we will work on a fix and reference the issue in
the release notes. If a report is declined, we will explain our reasoning.

## Known Security Considerations

This API uses two layers of authentication:

1. **API Key** (`X-API-KEY` header) — required on all data endpoints
   (Registration, Team, Finish, Log).

**Important notes for operators:**

- Change the API key before any deployment. Set it via the `X-API-KEY` environment
  variable or `appsettings.json`.
- Replace the  JWT signing key with a strong, randomly generated secret before deployment.
  Set it via the `JWT_KEY` environment variable or `appsettings.json`.
- Database credentials (`DB_PASSWORD`) and the Redis connection string should
  be provided via environment variables and never committed to the repository.
- Database credentials such as `DB_PASSWORD` and Redis settings such as
  `REDIS_HOST` and `REDIS_PORT` should be provided via environment variables,
  not committed to the repository.
- Do not commit secret-bearing `.env` files. If local examples are needed,
  commit only sanitized templates that contain placeholder values.
- The CORS policy currently allows any origin (`AllowAnyOrigin`). Restrict
  it to trusted origins in production environments.

## Automated Dependency Management

Dependabot is configured to scan NuGet packages and the devcontainer
definition weekly. Pull requests from Dependabot should be reviewed and
merged promptly, especially those tagged as security updates.
