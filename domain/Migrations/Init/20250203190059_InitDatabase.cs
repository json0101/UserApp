﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApp.Domain.Migrations.Init
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sec");

            migrationBuilder.CreateTable(
                name: "actions",
                schema: "sec",
                columns: table => new
                {
                    action_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actions", x => x.action_id);
                });

            migrationBuilder.CreateTable(
                name: "applications",
                schema: "sec",
                columns: table => new
                {
                    application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.application_id);
                });

            migrationBuilder.CreateTable(
                name: "screens",
                schema: "sec",
                columns: table => new
                {
                    screen_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    route = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    screen_father_id = table.Column<int>(type: "int", nullable: true),
                    order = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_screens", x => x.screen_id);
                    table.ForeignKey(
                        name: "FK_screens_screens_screen_father_id",
                        column: x => x.screen_father_id,
                        principalSchema: "sec",
                        principalTable: "screens",
                        principalColumn: "screen_id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "sec",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: true),
                    address_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "sec",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    application_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                    table.ForeignKey(
                        name: "FK_roles_applications_application_id",
                        column: x => x.application_id,
                        principalSchema: "sec",
                        principalTable: "applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "screens_actions",
                schema: "sec",
                columns: table => new
                {
                    user_application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    screen_id = table.Column<int>(type: "int", nullable: false),
                    action_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_screens_actions", x => x.user_application_id);
                    table.ForeignKey(
                        name: "FK_screens_actions_actions_action_id",
                        column: x => x.action_id,
                        principalSchema: "sec",
                        principalTable: "actions",
                        principalColumn: "action_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_screens_actions_screens_screen_id",
                        column: x => x.screen_id,
                        principalSchema: "sec",
                        principalTable: "screens",
                        principalColumn: "screen_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_applications",
                schema: "sec",
                columns: table => new
                {
                    user_application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    application_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_applications", x => x.user_application_id);
                    table.ForeignKey(
                        name: "FK_users_applications_applications_application_id",
                        column: x => x.application_id,
                        principalSchema: "sec",
                        principalTable: "applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_applications_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "sec",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roles_screens",
                schema: "sec",
                columns: table => new
                {
                    role_screen_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    screen_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_screens", x => x.role_screen_id);
                    table.ForeignKey(
                        name: "FK_roles_screens_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "sec",
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roles_screens_screens_screen_id",
                        column: x => x.screen_id,
                        principalSchema: "sec",
                        principalTable: "screens",
                        principalColumn: "screen_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_roles",
                schema: "sec",
                columns: table => new
                {
                    user_role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_roles", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_users_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "sec",
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "sec",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_roles_application_id",
                schema: "sec",
                table: "roles",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_screens_role_id",
                schema: "sec",
                table: "roles_screens",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_screens_screen_id",
                schema: "sec",
                table: "roles_screens",
                column: "screen_id");

            migrationBuilder.CreateIndex(
                name: "IX_screens_screen_father_id",
                schema: "sec",
                table: "screens",
                column: "screen_father_id");

            migrationBuilder.CreateIndex(
                name: "IX_screens_actions_action_id",
                schema: "sec",
                table: "screens_actions",
                column: "action_id");

            migrationBuilder.CreateIndex(
                name: "IX_screens_actions_screen_id",
                schema: "sec",
                table: "screens_actions",
                column: "screen_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_applications_application_id",
                schema: "sec",
                table: "users_applications",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_applications_user_id",
                schema: "sec",
                table: "users_applications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_roles_role_id",
                schema: "sec",
                table: "users_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_roles_user_id",
                schema: "sec",
                table: "users_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles_screens",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "screens_actions",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "users_applications",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "users_roles",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "actions",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "screens",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "users",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "applications",
                schema: "sec");
        }
    }
}
