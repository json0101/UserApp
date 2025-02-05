using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class Role: BaseEntity
    {
        public string Description { get; set; }
        public Application Application { get; set; }
        public int ApplicationId { get; set; }
        public List<UserRole> UsersRoles { get; set; }
        public List<RoleScreen> RolesScreens { get; set; }
        public class Map : BaseEntityConfiguration<Role>
        {
            public override void ConfigureEntity(EntityTypeBuilder<Role> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("role_id").IsRequired();
                builder.Property(x => x.ApplicationId).HasColumnName("application_id").IsRequired();
                builder.Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(255).HasColumnName("description");

                builder
                    .HasOne(e => e.Application)
                    .WithMany(e => e.Roles)
                    .HasForeignKey(e => e.ApplicationId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("roles", "sec");
            }
        }
    }

    
}
