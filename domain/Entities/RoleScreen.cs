using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class RoleScreen: BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int ScreenId { get; set; }
        public Screen Screen { get; set; }

        public class Map : BaseEntityConfiguration<RoleScreen>
        {
            public override void ConfigureEntity(EntityTypeBuilder<RoleScreen> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("role_screen_id");
                builder.Property(x => x.RoleId).HasColumnName("role_id");
                builder.Property(x => x.ScreenId).HasColumnName("screen_id");

                builder
                    .HasOne(e => e.Role)
                    .WithMany(e => e.RolesScreens)
                    .HasForeignKey(e => e.RoleId)
                    .HasPrincipalKey(e => e.Id);

                builder
                    .HasOne(e => e.Screen)
                    .WithMany(e => e.RolesScreens)
                    .HasForeignKey(e => e.ScreenId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("roles_screens", "sec");
            }
        }
    }
}
