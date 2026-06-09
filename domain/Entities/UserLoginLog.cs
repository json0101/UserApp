using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class UserLoginLog
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int ApplicationId { get; set; }
        public Application? Application { get; set; }
        public string? UserName { get; set; }
        public bool Successful { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? FailureReason { get; set; }
        public DateTime LoginAt { get; set; }

        public class Map : IEntityTypeConfiguration<UserLoginLog>
        {
            public void Configure(EntityTypeBuilder<UserLoginLog> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("user_login_log_id");
                builder.Property(x => x.UserId).HasColumnName("user_id");
                builder.Property(x => x.ApplicationId).HasColumnName("application_id");
                builder.Property(x => x.UserName).HasColumnName("username").HasMaxLength(255);
                builder.Property(x => x.Successful).HasColumnName("successful");
                builder.Property(x => x.IpAddress).HasColumnName("ip_address").HasMaxLength(45);
                builder.Property(x => x.UserAgent).HasColumnName("user_agent").HasMaxLength(512);
                builder.Property(x => x.FailureReason).HasColumnName("failure_reason").HasMaxLength(255);
                builder.Property(x => x.LoginAt).HasColumnName("login_at");

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
