using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UserApp.Domain.MigrationsPostgres.Login.Log
{
    /// <inheritdoc />
    public partial class AddLogLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users_login_logs",
                schema: "sec",
                columns: table => new
                {
                    user_login_log_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    successful = table.Column<bool>(type: "boolean", nullable: false),
                    ip_address = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    failure_reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_login_logs", x => x.user_login_log_id);
                    table.ForeignKey(
                        name: "FK_users_login_logs_applications_application_id",
                        column: x => x.application_id,
                        principalSchema: "sec",
                        principalTable: "applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_login_logs_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "sec",
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_login_logs_application_id",
                schema: "sec",
                table: "users_login_logs",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_login_logs_user_id",
                schema: "sec",
                table: "users_login_logs",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users_login_logs",
                schema: "sec");
        }
    }
}
