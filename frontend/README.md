# Regatta-Frontend

Das Angular-19-Frontend ist Teil von [Sanifant/regatta-buero](https://github.com/Sanifant/regatta-buero).
Alle Befehle hier werden im Ordner `frontend/` ausgefuehrt.

## Entwicklung

Node.js 22 verwenden. Das Backend mit seinen Datenbank-/Redis-Verbindungen separat starten.

```bash
npm ci
npm start
```

Der Entwicklungsserver laeuft auf Port 4200. Relative `/api`-Anfragen gehen ueber
`src/proxy.conf.json` an das Backend auf Port 5015.

## Build und Tests

```bash
npm run build
npm test -- --watch=false --browsers=ChromeHeadless
```

Build-Ausgabe: `dist/regatta-frontend/browser/`. Chrome oder Chromium ist fuer Karma-Tests erforderlich.
Die Beispielanwendung `projects/test` ist weiterhin enthalten, aber der Standard-Build
und die Tests waehlen explizit `regatta-frontend`.

## Container

```bash
docker build -t regatta-buero-frontend .
```

Der Nginx-Upstream und das gemeinsame Bild-Volume sind in `config/proxyconf` konfiguriert.
Weitere Informationen zu Backend, CI, Deployment und Git-Historie stehen in der
[gemeinsamen README](../README.md) und der [Migrationsdokumentation](../docs/REPOSITORY-MIGRATION.md).
