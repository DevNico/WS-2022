# WS-2022

## Backend

The backend application is based on: [Clean Architecture Template](https://github.com/ardalis/CleanArchitecture).

For local development, start the postgres database with the `docker/start.sh` script.

To generate a migration, run the following command in the `backend/src` directory:

```bash
dotnet ef migrations add -s ServiceReleaseManager.Api -c AppDbContext -p ServiceReleaseManager.Infrastructure <MigrationName>
```

## Frontend

The frontend application is automatically deployed to Firebase Hosting.

| Branch  | Url                        |
| ------- | -------------------------- |
| main    | https://srm.devnico.me     |
| develop | https://dev.srm.devnico.me |

## Terraform

Terraform is used to manage the Google Cloud Platform environment as well as a self hosted K3S Cluster where the Postgres instance, Keycloak and the backend will be deployed.

| Service  | Url                                |
| -------- | ---------------------------------- |
| Keycloak | https://idp.srm.k3s.devnico.cloud/ |