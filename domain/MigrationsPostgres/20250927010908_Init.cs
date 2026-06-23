using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UserApp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                    action_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
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
                    application_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.application_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "sec",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    address_id = table.Column<int>(type: "integer", nullable: true),
                    employee_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
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
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "screens",
                schema: "sec",
                columns: table => new
                {
                    screen_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    route = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    screen_father_id = table.Column<int>(type: "integer", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    is_father = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    application_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_screens", x => x.screen_id);
                    table.ForeignKey(
                        name: "FK_screens_applications_application_id",
                        column: x => x.application_id,
                        principalSchema: "sec",
                        principalTable: "applications",
                        principalColumn: "application_id");
                    table.ForeignKey(
                        name: "FK_screens_screens_screen_father_id",
                        column: x => x.screen_father_id,
                        principalSchema: "sec",
                        principalTable: "screens",
                        principalColumn: "screen_id");
                });

            migrationBuilder.CreateTable(
                name: "users_applications",
                schema: "sec",
                columns: table => new
                {
                    user_application_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "users_roles",
                schema: "sec",
                columns: table => new
                {
                    user_role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "roles_screens",
                schema: "sec",
                columns: table => new
                {
                    role_screen_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    screen_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "screens_actions",
                schema: "sec",
                columns: table => new
                {
                    user_application_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    screen_id = table.Column<int>(type: "integer", nullable: false),
                    action_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "VARCHAR", maxLength: 250, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "IX_screens_application_id",
                schema: "sec",
                table: "screens",
                column: "application_id");

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

            // -----------------------------------------------------------------------------
            // Carga inicial (seed): solo se ejecuta cuando se arma la BD por primera vez
            // (esta migración Init). En bases ya creadas no se vuelve a correr.
            // Crea la aplicación UserApp, el usuario por defecto, el rol Administrador y
            // todos sus accesos (pantallas, rol-pantalla, usuario-rol, usuario-aplicación).
            // Idempotente (ON CONFLICT DO NOTHING) y reajusta las secuencias de identidad.
            // -----------------------------------------------------------------------------
            migrationBuilder.Sql(@"
-- Aplicación (UserApp / sistema de seguridad) ------------------------------------
INSERT INTO sec.applications (application_id, description, created_at, created_by, updated_at, updated_by, active)
VALUES (1, 'User', '2026-02-01 21:31:57.755169+00', 'system', NULL, NULL, 't')
ON CONFLICT (application_id) DO NOTHING;

-- Usuario por defecto -----------------------------------------------------------
INSERT INTO sec.users (user_id, username, password, email, country_id, address_id, employee_code, created_at, created_by, updated_at, updated_by, active)
VALUES (1, 'jason.hernandez', 'AQAAAAIAAYagAAAAEHcrsJa8s9wl4LD5xsr0vh8Se3Gwr82fm0XntGGqmt3cTfAq0sX+0DOyy7I7M4m4OQ==', 'jasonhernandezaguilar@gmail.com', NULL, NULL, '0000-0001', '2026-02-01 21:31:57.778328+00', 'system', NULL, NULL, 't')
ON CONFLICT (user_id) DO NOTHING;

-- Rol Administrador --------------------------------------------------------------
INSERT INTO sec.roles (role_id, description, application_id, created_at, created_by, updated_at, updated_by, active)
VALUES (1, 'Administrador', 1, '2026-02-01 21:31:57.778323+00', 'system', NULL, NULL, 't')
ON CONFLICT (role_id) DO NOTHING;

-- Pantallas de la aplicación -----------------------------------------------------
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (1, 'Users', '/sec/user', NULL, 1, 't', 1, '2026-02-01 21:31:57.778324+00', 'system', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (2, 'Roles', '/sec/role', NULL, 2, 't', 1, '2026-02-01 21:31:57.778325+00', 'system', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (3, 'Users - Roles', '/sec/user-role', NULL, 3, 't', 1, '2026-02-01 21:31:57.778326+00', 'system', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (4, 'Screens', '/sec/screen', NULL, 4, 't', 1, '2026-02-01 21:31:57.778327+00', 'system', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (5, 'Aplicaciones', '/sec/app', NULL, 1, 't', 1, '2026-02-12 03:09:32.952812+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (6, 'Roles - Screens', '/sec/role-screen', NULL, 5, 't', 1, '2026-02-12 03:10:12.479507+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (34, 'Actions', '/sec/action', NULL, 8, 't', 1, '2026-04-30 14:08:18.892649+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (35, 'Screen-Actions', '/sec/action-screen', NULL, 9, 't', 1, '2026-04-30 14:09:00.165188+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (36, 'Arbol Acceso', '/sec/tree-access', NULL, 10, 't', 1, '2026-04-30 14:24:55.216252+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (37, 'Usuario - Aplicaciones', '/sec/user-application', NULL, 3, 't', 1, '2026-05-05 21:46:07.047286+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;
INSERT INTO sec.screens (screen_id, name, route, screen_father_id, ""order"", is_father, application_id, created_at, created_by, updated_at, updated_by, active) VALUES (38, 'Arbol Pantallas', '/sec/tree-screen', NULL, 10, 't', 1, '2026-05-08 20:29:15.674212+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (screen_id) DO NOTHING;

-- Relación rol Administrador -> pantallas (accesos) ------------------------------
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (1, 1, 1, '2026-01-01 00:00:00+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (2, 1, 2, '2026-01-01 00:00:00+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (3, 1, 3, '2026-01-01 00:00:00+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (4, 1, 4, '2026-01-01 00:00:00+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (5, 1, 6, '2026-02-12 03:10:33.590178+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (6, 1, 5, '2026-02-12 03:10:39.573421+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (33, 1, 34, '2026-04-30 14:09:31.98802+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (34, 1, 35, '2026-04-30 14:09:43.094375+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (35, 1, 36, '2026-04-30 16:18:41.116417+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (36, 1, 37, '2026-05-05 21:46:27.111176+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;
INSERT INTO sec.roles_screens (role_screen_id, role_id, screen_id, created_at, created_by, updated_at, updated_by, active) VALUES (72, 1, 38, '2026-05-08 20:32:21.571128+00', 'jason.hernandez', NULL, NULL, 't') ON CONFLICT (role_screen_id) DO NOTHING;

-- Usuario -> aplicación UserApp (acceso activo) ----------------------------------
INSERT INTO sec.users_applications (user_application_id, application_id, user_id, created_at, created_by, updated_at, updated_by, active)
VALUES (3, 1, 1, '2026-06-23 15:27:23.625718+00', 'system', NULL, NULL, 't')
ON CONFLICT (user_application_id) DO NOTHING;

-- Usuario -> rol Administrador ---------------------------------------------------
INSERT INTO sec.users_roles (user_role_id, user_id, role_id, created_at, created_by, updated_at, updated_by, active)
VALUES (1, 1, 1, '2026-01-01 00:00:00+00', 'jason.hernandez', NULL, NULL, 't')
ON CONFLICT (user_role_id) DO NOTHING;

-- Reajuste de secuencias de identidad (para que los próximos inserts no choquen) -
SELECT setval(pg_get_serial_sequence('sec.applications', 'application_id'), GREATEST((SELECT COALESCE(MAX(application_id), 1) FROM sec.applications), 1));
SELECT setval(pg_get_serial_sequence('sec.users', 'user_id'), GREATEST((SELECT COALESCE(MAX(user_id), 1) FROM sec.users), 1));
SELECT setval(pg_get_serial_sequence('sec.roles', 'role_id'), GREATEST((SELECT COALESCE(MAX(role_id), 1) FROM sec.roles), 1));
SELECT setval(pg_get_serial_sequence('sec.screens', 'screen_id'), GREATEST((SELECT COALESCE(MAX(screen_id), 1) FROM sec.screens), 1));
SELECT setval(pg_get_serial_sequence('sec.roles_screens', 'role_screen_id'), GREATEST((SELECT COALESCE(MAX(role_screen_id), 1) FROM sec.roles_screens), 1));
SELECT setval(pg_get_serial_sequence('sec.users_applications', 'user_application_id'), GREATEST((SELECT COALESCE(MAX(user_application_id), 1) FROM sec.users_applications), 1));
SELECT setval(pg_get_serial_sequence('sec.users_roles', 'user_role_id'), GREATEST((SELECT COALESCE(MAX(user_role_id), 1) FROM sec.users_roles), 1));
");
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
