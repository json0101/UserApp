using UserApp.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public int? AddressId { get; set; }
        public string EmployeeCode { get; set; }
        public List<UserApplication> UsersApplications { get; set; }
        public List<UserRole> UsersRoles { get; set; }

        public class Map : BaseEntityConfiguration<User>
        {
            public override void ConfigureEntity(EntityTypeBuilder<User> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("user_id");
                builder.Property(x => x.UserName).HasColumnName("username").HasMaxLength(255);
                builder.Property(x => x.Password).HasColumnName("password");
                builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(255);
                builder.Property(x => x.CountryId).HasColumnName("country_id");
                builder.Property(x => x.AddressId).HasColumnName("address_id");
                builder.Property(x => x.EmployeeCode).HasColumnName("employee_code").HasMaxLength(10);

                builder.ToTable("users", "sec");
            }
        }
    }
}
