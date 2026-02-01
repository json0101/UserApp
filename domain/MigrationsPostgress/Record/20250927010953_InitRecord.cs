using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApp.Domain.Migrations.Record
{
    /// <inheritdoc />
    public partial class InitRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "applications",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "description" },
                values: new object[,]
                {
                    { DateTime.Now.ToUniversalTime(), "system", true, "User" },
                }
            );

            migrationBuilder.InsertData(
                table: "actions",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "description" },
                values: new object[,]
                {
                    { DateTime.Now.ToUniversalTime(), "system", true, "OnlyView" },
                    { DateTime.Now.ToUniversalTime(), "system", true, "Save" },
                    { DateTime.Now.ToUniversalTime(), "system", true, "Delete" },
                    { DateTime.Now.ToUniversalTime(), "system", true, "Modify" },
                }
            );

            migrationBuilder.InsertData(
                table: "roles",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "description", "application_id" },
                values: new object[,]
                {
                    { DateTime.Now.ToUniversalTime(), "system", true, "Administrador", 1 },                    
                }
            );

            migrationBuilder.InsertData(
                table: "screens",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "name", "route", "screen_father_id", "order" },
                values: new object[,]
                {
                    { DateTime.Now.ToUniversalTime(), "system", true, "Users" , "/users", null, 1},
                    { DateTime.Now.ToUniversalTime(), "system", true, "Roles", "/roles", 1, 2 },
                    { DateTime.Now.ToUniversalTime(), "system", true, "Users - Roles", "/user/rol", 1, 3 },
                    { DateTime.Now.ToUniversalTime(), "system", true, "Screens", "/screen", 1, 4 },
                }
            );

            migrationBuilder.InsertData(
                table: "users",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "username", "password", "email", "country_id", "address_id", "employee_code" },
                values: new object[,]
                {
                    { DateTime.Now.ToUniversalTime(), "system", true, "jason.hernandez" , "123", "jasonhernandezaguilar@gmail.com", null, null, "0000-0001"},
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("truncate table sec.users_applications;");

            migrationBuilder.Sql("truncate table sec.roles_screens;");
            migrationBuilder.Sql("truncate table sec.screens_actions;");
            migrationBuilder.Sql("truncate table sec.users_roles;");
            migrationBuilder.Sql("delete from sec.screens;");

            migrationBuilder.Sql("delete from sec.actions;");

            migrationBuilder.Sql("delete from sec.roles;");
            migrationBuilder.Sql("delete from sec.applications;");
            migrationBuilder.Sql("delete from sec.users;");
        }
    }
}
