using UserApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserApp.Domain
{
    public class UserAppContext: DbContext
    {
        public UserAppContext(DbContextOptions<UserAppContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleScreen> RoleScreen { get; set; }
        public DbSet<Screen> Screen { get; set; }
        public DbSet<ScreenAction> ScreenAction { get; set; }
        public DbSet<ApplicationRegister> Application { get; set; }
        public DbSet<UserApplication> UserApplication { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<ActionSys> Action { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Role.Map());
            modelBuilder.ApplyConfiguration(new RoleScreen.Map());
            modelBuilder.ApplyConfiguration(new Screen.Map());
            modelBuilder.ApplyConfiguration(new ScreenAction.Map());
            modelBuilder.ApplyConfiguration(new ApplicationRegister.Map());
            modelBuilder.ApplyConfiguration(new UserApplication.Map());
            modelBuilder.ApplyConfiguration(new UserRole.Map());
            modelBuilder.ApplyConfiguration(new User.Map());
            modelBuilder.ApplyConfiguration(new ActionSys.Map());
        }
    }
}
