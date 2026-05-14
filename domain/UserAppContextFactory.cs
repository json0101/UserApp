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

            var optionsBuilder = new DbContextOptionsBuilder<UserAppContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new UserAppContext(optionsBuilder.Options);
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
