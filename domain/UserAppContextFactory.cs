using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserApp.Domain {

    public class UserAppContextFactory : IDesignTimeDbContextFactory<UserAppContext>
    {
        public UserAppContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var apiPath = ResolveApiProjectPath();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("UserApp");
            var databaseProvider = configuration["Database:Provider"];

            var optionsBuilder = new DbContextOptionsBuilder<UserAppContext>();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("No se encontro la cadena de conexion 'ConnectionStrings:UserApp'.");
            }

            ConfigureDatabaseProvider(optionsBuilder, databaseProvider, connectionString);

            return new UserAppContext(optionsBuilder.Options);
        }

        private static void ConfigureDatabaseProvider(DbContextOptionsBuilder optionsBuilder, string? databaseProvider, string connectionString)
        {
            var provider = string.IsNullOrWhiteSpace(databaseProvider)
                ? "Postgres"
                : databaseProvider.Trim();

            switch (provider.ToUpperInvariant())
            {
                case "POSTGRES":
                case "POSTGRESQL":
                case "NPGSQL":
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                case "SQLSERVER":
                case "SQL_SERVER":
                case "MSSQL":
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                default:
                    throw new InvalidOperationException($"Database provider '{databaseProvider}' no soportado. Use 'Postgres' o 'SqlServer'.");
            }
        }

        private static string ResolveApiProjectPath()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (directory != null)
            {
                var candidate = Path.Combine(directory.FullName, "api");
                if (Directory.Exists(candidate) && File.Exists(Path.Combine(candidate, "appsettings.json")))
                    return candidate;
                directory = directory.Parent;
            }
            throw new InvalidOperationException("No se pudo localizar el proyecto api con appsettings.json.");
        }
    }
}
