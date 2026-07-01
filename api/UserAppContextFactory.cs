using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UserApp.Domain;
using UserApp.Service;

namespace api
{
    /// <summary>
    /// Factory usado SOLO en tiempo de diseno por las herramientas de EF Core
    /// (dotnet ef migrations add / script / database update).
    ///
    /// El motor se elige asi (en orden de prioridad):
    ///   1. argumento de linea de comandos:  ... -- --provider SqlServer | Postgres
    ///   2. appsettings.json -> Database:Provider
    ///
    /// Al generar migraciones hay que apuntar al proyecto del motor correspondiente, p.ej.:
    ///   dotnet ef migrations add Nombre --project migrations/UserApp.Migrations.SqlServer --startup-project api -- --provider SqlServer
    ///   dotnet ef migrations add Nombre --project migrations/UserApp.Migrations.Postgres --startup-project api -- --provider Postgres
    /// </summary>
    public class UserAppContextFactory : IDesignTimeDbContextFactory<UserAppContext>
    {
        public UserAppContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var provider = GetProviderArg(args) ?? configuration["Database:Provider"];

            // En diseno no se conecta a la BD (add/script), pero UseXxx exige una cadena.
            var connectionString = configuration.GetConnectionString("UserApp")
                ?? "Host=localhost;Database=UserApp;Username=postgres;Password=postgres";

            var optionsBuilder = new DbContextOptionsBuilder<UserAppContext>();
            DatabaseProviderSelector.Configure(optionsBuilder, provider, connectionString);

            return new UserAppContext(optionsBuilder.Options);
        }

        private static string? GetProviderArg(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "--provider", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
                    return args[i + 1];

                if (args[i].StartsWith("--provider=", StringComparison.OrdinalIgnoreCase))
                    return args[i].Substring("--provider=".Length);
            }

            return null;
        }
    }
}
