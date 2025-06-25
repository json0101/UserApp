using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApp.Domain.Migrations.Users
{
    /// <inheritdoc />
    public partial class AddEmployeeCodToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "employee_code",
                schema: "sec",
                table: "users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee_code",
                schema: "sec",
                table: "users");
        }
    }
}
