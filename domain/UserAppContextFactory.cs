using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserApp.Domain {

    public class UserAppContextFactory : IDesignTimeDbContextFactory<UserAppContext>
    {
        public UserAppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserAppContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5433;Database=userapp;Username=postgres;Password=123"
            );

            return new UserAppContext(optionsBuilder.Options);
        }
    }
}
