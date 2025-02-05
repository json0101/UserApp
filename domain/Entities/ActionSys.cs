using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class ActionSys: BaseEntity
    {
        public string Description { get; set; }
        public List<ScreenAction> ScreenActions { get; set; }
        public class Map: BaseEntityConfiguration<ActionSys> {

            public override void ConfigureEntity(EntityTypeBuilder<ActionSys> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("action_id");
                builder.Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(255).HasColumnName("description");

                builder.ToTable("actions", "sec");
            }
        }
    }
}
