using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class Screen: BaseEntity
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public Screen? ScreenFather { get; set; }
        public int? ScreenFatherId { get; set; }
        public int Order { get; set; }
        public bool IsFather { get; set; }
        public int? ApplicationId { get; set; }
        public ApplicationRegister Application { get; set; }
        public List<Screen> ScreenChildren { get; set; }
        public List<RoleScreen> RolesScreens { get; set; }
        public List<ScreenAction> ScreenActions { get; set; }
        public class Map : BaseEntityConfiguration<Screen>
        {
            public override void ConfigureEntity(EntityTypeBuilder<Screen> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("screen_id");
                builder.Property(x => x.ScreenFatherId).HasColumnName("screen_father_id");
                builder.Property(x => x.Order).HasColumnName("order").IsRequired();
                builder.Property(x => x.IsFather).HasColumnName("is_father").IsRequired().HasDefaultValue(false);
                builder.Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(255).HasColumnName("name");
                builder.Property(x => x.Route).HasColumnType("VARCHAR").HasMaxLength(255).HasColumnName("route");
                builder.Property(x => x.ApplicationId).HasColumnName("application_id");

                builder
                    .HasOne(e => e.ScreenFather)
                    .WithMany(e => e.ScreenChildren)
                    .HasForeignKey(e => e.ScreenFatherId)
                    .HasPrincipalKey(e => e.Id);

                builder
                    .HasOne(e => e.Application)
                    .WithMany(e => e.Screens)
                    .HasForeignKey(e => e.ApplicationId)
                    .HasPrincipalKey(e => e.Id);

                builder.ToTable("screens", "sec");
            }
        }

    }
}
