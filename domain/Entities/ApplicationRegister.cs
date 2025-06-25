using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class ApplicationRegister: BaseEntity
    {
        public string Description { get; set; }
        public List<Role> Roles { get; set; }
        public List<UserApplication> UsersApplications { get; set; }
        public List<Screen> Screens { get; set; }

        public class Map : BaseEntityConfiguration<ApplicationRegister>
        {
            public override void ConfigureEntity(EntityTypeBuilder<ApplicationRegister> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("application_id");
                builder.Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(255).HasColumnName("description");

                builder.ToTable("applications", "sec");
            }
        }
    }
}
