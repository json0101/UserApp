using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApp.Domain.Migrations.Init.Records
{
    /// <inheritdoc />
    public partial class AddInitRecords : Migration
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
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Export System" },
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Logistics System" },
                }
            );

            migrationBuilder.InsertData(
                table: "actions",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "description" },
                values: new object[,]
                {
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "OnlyView" },
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Save" },
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Delete" },
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Modify" },
                }
            );

            migrationBuilder.InsertData(
                table: "roles",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "description", "application_id" },
                values: new object[,]
                {
                    { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Shipping", 1 },
                    { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Planning", 1 },
                    { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Impex" , 1},
                }
            );

            migrationBuilder.InsertData(
                table: "screens",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "name", "route", "screen_father_id", "order" },
                values: new object[,]
                {
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "VSP" , "/vsp", null, 1},
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Planning", "/vsp/planning", 1, 2 },
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Shipping", "/vsp/shipping", 1, 3 },
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "Impex", "/vsp/impex", 1, 4 },
                }
            );

            migrationBuilder.InsertData(
                table: "users",
                schema: "sec",
                columns: new[] { "created_at", "created_by", "active", "username", "password", "email", "country_id", "address_id" },
                values: new object[,]
                {
            { DateTime.Now.ToString("yyyyMMdd hh:mm:ss"), "system", true, "jason.hernandez" , "123", "jason.hernandez@tegraglobal.com", null, null},
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
