# Repository-Migration

`Sanifant/regatta-buero` ist das gemeinsame Repository fuer Backend (`src/`) und Frontend (`frontend/`).

## Erhaltene Historie

- Backend-Ausgangsstand: `9ab0021284d24de5eb2f39df7d5ef7ef50c93337`.
- Frontend-Ausgangsstand: `08176c9a9ba5fc2c913603bf5174e412bbf2b369`.
- Importcommit: `30a360e` hat beide Historien als Eltern und uebernimmt den vollstaendigen Frontend-Baum unter `frontend/`.
- Kein Squash, kein Submodule; Frontend und Backend sind in einem Checkout verfuegbar.
- Die urspruengliche Frontend-Lizenz bleibt in `frontend/LICENSE` erhalten.

Zum Nachvollziehen einzelner Frontend-Dateien vor dem Import weiterhin die Originalhistorie nutzen:

```bash
git log 08176c9a9ba5fc2c913603bf5174e412bbf2b369 -- src/app
```

## Uebernommene Entwicklung und CI

Frontend-Build und Tests laufen im Hauptrepository mit `frontend/` als Arbeitsverzeichnis.
Dependabot und der gemeinsame Devcontainer beruecksichtigen Node.js und die npm-Abhaengigkeiten.
Der importierte separate Sonar-Workflow war ein .NET-Scannerablauf ohne .NET-Projekt im Frontend;
er wurde nicht als Angular-Check aktiviert. Sein Original bleibt in der Git-Historie erhalten.

Die API-URLs sind relativ, damit der lokale Angular-Proxy und der bestehende Nginx-Upstream
verwendet werden. Anwendungsauthentifizierung und Backend-Logik bleiben unveraendert.
Frontend-Releases verwenden das separate Image `ghcr.io/sanifant/regatta-buero-frontend`.
Ein optionaler Deployment-Webhook benoetigt das Secret `FRONTEND_PORTAINER_WEBHOOK_URL`
im gemeinsamen Repository; Backend- und Frontend-Webhooks bleiben getrennt.

## Archivierung des alten Frontend-Repositories

Nach verifizierter Uebertragung auf den Hauptzweig von `Sanifant/regatta-buero` wird
`Sanifant/regatta-frontend` als Archiv gekennzeichnet. Das alte Repository bleibt fuer
Historie, bestehende Links und alte Releases erhalten. Neue Issues und Pull Requests
gehoeren ins gemeinsame Repository. Die GitHub-Archivierung ist ein separater
Repository-Einstellungsschritt und darf erst nach der gesicherten Uebertragung erfolgen.
