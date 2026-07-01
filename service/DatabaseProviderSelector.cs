using Microsoft.EntityFrameworkCore;

namespace UserApp.Service
{
    /// <summary>
    /// Unica fuente de verdad para elegir el motor de base de datos y su assembly de migraciones.
    /// La usan tanto el runtime (Main.ConfigureService) como el factory de diseno del proyecto api,
    /// para que "add migration" y la ejecucion real nunca queden desalineados.
    /// </summary>
    public static class DatabaseProviderSelector
    {
        // Cada motor tiene su propio assembly de migraciones (EF Core solo admite un snapshot por assembly/context).
        public const string SqlServerMigrationsAssembly = "UserApp.Migrations.SqlServer";
        public const string PostgresMigrationsAssembly = "UserApp.Migrations.Postgres";

        public enum DbProvider
        {
            SqlServer,
            Postgres
        }

        public static DbProvider Resolve(string? provider)
        {
            // Postgres por defecto para no romper clones historicos que no setean Database:Provider.
            var value = string.IsNullOrWhiteSpace(provider) ? "Postgres" : provider.Trim();

            switch (value.ToUpperInvariant())
            {
                case "POSTGRES":
                case "POSTGRESQL":
                case "NPGSQL":
                    return DbProvider.Postgres;
                case "SQLSERVER":
                case "SQL_SERVER":
                case "MSSQL":
                    return DbProvider.SqlServer;
                default:
                    throw new InvalidOperationException(
                        $"Database provider '{provider}' no soportado. Use 'Postgres' o 'SqlServer'.");
            }
        }

        public static void Configure(DbContextOptionsBuilder options, string? provider, string connectionString)
        {
            switch (Resolve(provider))
            {
                case DbProvider.SqlServer:
                    options.UseSqlServer(connectionString, x => x.MigrationsAssembly(SqlServerMigrationsAssembly));
                    break;
                case DbProvider.Postgres:
                    options.UseNpgsql(connectionString, x => x.MigrationsAssembly(PostgresMigrationsAssembly));
                    break;
            }
        }
    }
}
