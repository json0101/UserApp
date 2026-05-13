using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserApp.Domain {

    public class UserAppContextFactory : IDesignTimeDbContextFactory<UserAppContext>
    {
        public UserAppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserAppContext>();

            optionsBuilder.UseSqlServer(
                "Server=COLOIMPEXDB01; Database=UserApp; User Id=ImpexSecurity;Password=S3curityL0gisticsExp0rt;TrustServerCertificate=True"
            );

            return new UserAppContext(optionsBuilder.Options);
        }
    }
}
