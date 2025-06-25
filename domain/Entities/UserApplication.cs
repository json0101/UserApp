using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class UserApplication : BaseEntity
    {
        public int ApplicationId { get; set; }
        public ApplicationRegister Application { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public class Map : BaseEntityConfiguration<UserApplication>
        {
            public override void ConfigureEntity(EntityTypeBuilder<UserApplication> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("user_application_id");
                builder.Property(x => x.ApplicationId).HasColumnName("application_id");
                builder.Property(x => x.UserId).HasColumnName("user_id");

                builder
                    .HasOne(e => e.Application)
                    .WithMany(e => e.UsersApplications)
                    .HasForeignKey(e => e.ApplicationId)
                    .HasPrincipalKey(e => e.Id);

                builder
                    .HasOne(e => e.User)
                    .WithMany(e => e.UsersApplications)
                    .HasForeignKey(e => e.UserId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("users_applications", "sec");
            }
        }
    }
}
