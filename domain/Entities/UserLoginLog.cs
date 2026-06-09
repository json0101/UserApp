using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class UserLoginLog : BaseEntity
    {
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int ApplicationId { get; set; }
        public Application? Application { get; set; }
        public bool Successful { get; set; }
        public string? IpAddress { get; set; }
        public string? FailureReason { get; set; }
        public class Map : BaseEntityConfiguration<UserLoginLog>
        {
            public override void ConfigureEntity(EntityTypeBuilder<UserLoginLog> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("user_login_log_id");
                builder.Property(x => x.UserId).HasColumnName("user_id");
                builder.Property(x => x.ApplicationId).HasColumnName("application_id");
                builder.Property(x => x.Successful).HasColumnName("successful");
                builder.Property(x => x.IpAddress).HasColumnName("ip_address").HasMaxLength(45);
                builder.Property(x => x.FailureReason).HasColumnName("failure_reason").HasMaxLength(255);

                builder
                    .HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .HasPrincipalKey(e => e.Id);

                builder
                    .HasOne(e => e.Application)
                    .WithMany()
                    .HasForeignKey(e => e.ApplicationId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("users_login_logs", "sec");
            }
        }
    }
}
