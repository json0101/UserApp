using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    // Solicitud de cambio de contrasena via PIN enviado al correo (flujo self-service / "olvide mi clave").
    // Equivalente a SolicitudCambioContrasena del ejemplo en NestJS.
    public class PasswordResetRequest
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Pin { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string EmailSentTo { get; set; }
        public bool Active { get; set; }
        public DateTime? PasswordChangedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        public class Map : IEntityTypeConfiguration<PasswordResetRequest>
        {
            public void Configure(EntityTypeBuilder<PasswordResetRequest> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("password_reset_request_id");
                builder.Property(x => x.UserId).HasColumnName("user_id");
                builder.Property(x => x.Pin).HasColumnName("pin").HasMaxLength(10);
                builder.Property(x => x.RequestedAt).HasColumnName("requested_at");
                builder.Property(x => x.ExpiresAt).HasColumnName("expires_at");
                builder.Property(x => x.EmailSentTo).HasColumnName("email_sent_to").HasMaxLength(255);
                builder.Property(x => x.Active).HasColumnName("active");
                builder.Property(x => x.PasswordChangedAt).HasColumnName("password_changed_at");
                builder.Property(x => x.DeactivatedAt).HasColumnName("deactivated_at");

                builder
                    .HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("password_reset_requests", "sec");
            }
        }
    }
}
