using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UserApp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                schema: "sec",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "sec",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "sec",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "sec",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "sec",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "password_reset_requests",
                schema: "sec",
                columns: table => new
                {
                    password_reset_request_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    pin = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    requested_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email_sent_to = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    password_changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deactivated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_reset_requests", x => x.password_reset_request_id);
                    table.ForeignKey(
                        name: "FK_password_reset_requests_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "sec",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_password_reset_requests_user_id",
                schema: "sec",
                table: "password_reset_requests",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "password_reset_requests",
                schema: "sec");

            migrationBuilder.DropColumn(
                name: "birth_date",
                schema: "sec",
                table: "users");

            migrationBuilder.DropColumn(
                name: "city",
                schema: "sec",
                table: "users");

            migrationBuilder.DropColumn(
                name: "country",
                schema: "sec",
                table: "users");

            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "sec",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "sec",
                table: "users");
        }
    }
}
