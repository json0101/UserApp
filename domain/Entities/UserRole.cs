using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public class Map : BaseEntityConfiguration<UserRole>
        {
            public override void ConfigureEntity(EntityTypeBuilder<UserRole> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("user_role_id");
                builder.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
                builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();

                builder
                    .HasOne(e => e.User)
                    .WithMany(e => e.UsersRoles)
                    .HasForeignKey(e => e.UserId)
                    .HasPrincipalKey(e => e.Id);

                builder
                    .HasOne(e => e.Role)
                    .WithMany(e => e.UsersRoles)
                    .HasForeignKey(e => e.RoleId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("users_roles", "sec");
            }
        }
    }
}
