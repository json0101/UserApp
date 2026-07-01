# Migraciones (multi-motor)

Este sistema soporta **SQL Server** y **PostgreSQL** con el mismo modelo de dominio.
La regla de oro de EF Core es: **un solo snapshot por assembly y por `DbContext`**.
Por eso cada motor tiene su **propio proyecto de migraciones** (su propio assembly y su
propio snapshot). Mezclarlos en una sola carpeta fue lo que rompio el proyecto antes.

```
domain/                          -> entidades + UserAppContext (AGNOSTICO del motor, sin provider)
migrations/
  UserApp.Migrations.SqlServer/  -> migraciones + snapshot SOLO de SQL Server
  UserApp.Migrations.Postgres/   -> migraciones + snapshot SOLO de Postgres
  reconcile/                     -> scripts SQL puntuales (one-time) de reconciliacion de BDs vivas
service/DatabaseProviderSelector.cs -> unica fuente de verdad: provider -> UseXxx + MigrationsAssembly
api/UserAppContextFactory.cs        -> factory de diseno; elige el motor por  -- --provider  o appsettings
```

## Como se elige el motor

**En ejecucion (runtime):** por configuracion `Database:Provider` en `api/appsettings.json`
(o variable de entorno `Database__Provider`). Valores: `SqlServer` o `Postgres`.
`DatabaseProviderSelector` mapea ese valor al `UseSqlServer`/`UseNpgsql` **y** al
`MigrationsAssembly` correcto. No hay que tocar `.csproj` para cambiar de motor.

**En tiempo de diseno (dotnet ef):** se pasa `-- --provider <motor>` al final del comando,
y se apunta con `--project` al proyecto del motor correspondiente. Ambos deben coincidir.

## Agregar una migracion

SQL Server:

    dotnet ef migrations add NombreDeLaMigracion \
      --project migrations/UserApp.Migrations.SqlServer \
      --startup-project api \
      --context UserAppContext \
      -- --provider SqlServer

Postgres:

    dotnet ef migrations add NombreDeLaMigracion \
      --project migrations/UserApp.Migrations.Postgres \
      --startup-project api \
      --context UserAppContext \
      -- --provider Postgres

> Al cambiar el modelo, agrega la migracion **en los DOS** proyectos (una por motor).

## Aplicar migraciones a una base

    dotnet ef database update \
      --project migrations/UserApp.Migrations.SqlServer \
      --startup-project api \
      --context UserAppContext \
      -- --provider SqlServer

(La cadena de conexion y el motor salen de `api/appsettings.json`; `-- --provider`
solo fuerza cual assembly de migraciones usar.)

## Nota historica: baseline 2026-07-01

Las migraciones de SQL Server se re-crearon desde cero (`InitialCreate` + `SeedInitialData`)
porque el historial anterior estaba corrupto. Las bases SQL Server **ya existentes** se ponen
al dia una sola vez con `migrations/reconcile/2026-07-01_sqlserver_baseline.sql`
(agrega lo que faltaba y reescribe `__EFMigrationsHistory` al nuevo baseline, sin perder datos).
Las migraciones de Postgres se conservaron intactas para no romper despliegues Postgres existentes.
