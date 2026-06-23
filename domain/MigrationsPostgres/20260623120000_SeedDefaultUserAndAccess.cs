using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApp.Domain.Migrations
{
    /// <summary>
    /// Carga inicial (seed) para arrancar un proyecto nuevo con la aplicación UserApp
    /// (application_id = 1), el usuario por defecto y todos sus accesos ya registrados:
    /// pantallas, rol Administrador y las relaciones rol-pantalla / usuario-rol / usuario-aplicación.
    ///
    /// Es idempotente (ON CONFLICT DO NOTHING) y al final reajusta las secuencias de identidad
    /// para que los siguientes inserts automáticos no choquen con los ids sembrados.
    /// NOTA: la contraseña del usuario es el hash actual; el login con la clave vigente funciona tal cual.
    /// </summary>
    public partial class SeedDefaultUserAndAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.Sql(@"
DELETE FROM sec.users_roles WHERE user_role_id = 1;
DELETE FROM sec.users_applications WHERE user_application_id = 3;
DELETE FROM sec.roles_screens WHERE role_screen_id IN (1, 2, 3, 4, 5, 6, 33, 34, 35, 36, 72);
DELETE FROM sec.screens WHERE screen_id IN (1, 2, 3, 4, 5, 6, 34, 35, 36, 37, 38);
DELETE FROM sec.roles WHERE role_id = 1;
DELETE FROM sec.users WHERE user_id = 1;
DELETE FROM sec.applications WHERE application_id = 1;
");
        }
    }
}
