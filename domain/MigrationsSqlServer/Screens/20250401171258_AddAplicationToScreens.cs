using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApp.Domain.Migrations.Screens
{
    /// <inheritdoc />
    public partial class AddAplicationToScreens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "application_id",
                schema: "sec",
                table: "screens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_screens_application_id",
                schema: "sec",
                table: "screens",
                column: "application_id");

            migrationBuilder.AddForeignKey(
                name: "FK_screens_applications_application_id",
                schema: "sec",
                table: "screens",
                column: "application_id",
                principalSchema: "sec",
                principalTable: "applications",
                principalColumn: "application_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_screens_applications_application_id",
                schema: "sec",
                table: "screens");

            migrationBuilder.DropIndex(
                name: "IX_screens_application_id",
                schema: "sec",
                table: "screens");

            migrationBuilder.DropColumn(
                name: "application_id",
                schema: "sec",
                table: "screens");
        }
    }
}
