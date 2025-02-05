using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class ScreenAction: BaseEntity
    {
        public int ScreenId { get; set; }
        public Screen Screen { get; set; }
        public int ActionId { get; set; }
        public ActionSys Action { get; set; }

        public class Map: BaseEntityConfiguration<ScreenAction>
        {
            public override void ConfigureEntity(EntityTypeBuilder<ScreenAction> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("user_application_id");
                builder.Property(x => x.ScreenId).HasColumnName("screen_id");
                builder.Property(x => x.ActionId).HasColumnName("action_id");

                builder
                    .HasOne(e => e.Screen)
                    .WithMany(e => e.ScreenActions)
                    .HasForeignKey(e => e.ScreenId)
                    .HasPrincipalKey(e => e.Id);

                builder
                    .HasOne(e => e.Action)
                    .WithMany(e => e.ScreenActions)
                    .HasForeignKey(e => e.ActionId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("screens_actions", "sec");
            }
        }
    }
}
