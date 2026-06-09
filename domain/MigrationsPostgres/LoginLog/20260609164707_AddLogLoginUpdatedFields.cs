using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UserApp.Domain.MigrationsPostgres.LoginLog
{
    /// <inheritdoc />
    public partial class AddLogLoginUpdatedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                schema: "sec",
                table: "users_login_logs");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "sec",
                table: "users_login_logs");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "sec",
                table: "users_login_logs");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "sec",
                table: "users_login_logs");

            migrationBuilder.RenameColumn(
                name: "created_at",
                schema: "sec",
                table: "users_login_logs",
                newName: "login_at");

            migrationBuilder.AlterColumn<long>(
                name: "user_login_log_id",
                schema: "sec",
                table: "users_login_logs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "user_agent",
                schema: "sec",
                table: "users_login_logs",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "username",
                schema: "sec",
                table: "users_login_logs",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_agent",
                schema: "sec",
                table: "users_login_logs");

            migrationBuilder.DropColumn(
                name: "username",
                schema: "sec",
                table: "users_login_logs");

            migrationBuilder.RenameColumn(
                name: "login_at",
                schema: "sec",
                table: "users_login_logs",
                newName: "created_at");

            migrationBuilder.AlterColumn<int>(
                name: "user_login_log_id",
                schema: "sec",
                table: "users_login_logs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "active",
                schema: "sec",
                table: "users_login_logs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                schema: "sec",
                table: "users_login_logs",
                type: "VARCHAR",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "sec",
                table: "users_login_logs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                schema: "sec",
                table: "users_login_logs",
                type: "VARCHAR",
                maxLength: 250,
                nullable: true);
        }
    }
}
