using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

using UserApp.Domain;
using UserApp.Repository;
using Microsoft.EntityFrameworkCore;
using UserApp.Service.Global;
using UserApp.Service.Services.Users;
using UserApp.Service.Services.Users.Mapper;
using UserApp.Service.Services.UsersApplications;
using UserApp.Service.Services.UsersScreens;
using UserApp.Service.Services.Autentication;
using UserApp.Service.Services.Screens.Service;

using UserApp.Service.Services.RolesScreens;
using UserApp.Service.Services.Roles;
using UserApp.Service.Services.Applications;
using UserApp.Service.Services.UserRoles;
using UserApp.Service.Services.Actions;
using UserApp.Service.Services.ActionsScreens;
using UserApp.Service.Services.UserAccessTree;
using UserApp.Service.Services.UsersLoginLogs;

namespace UserApp.Service
{
    public static class Main
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, string? userAppConnectionString, int applicationId, string licenceAutoMapper, string? databaseProvider = null)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserScreenService, UserScreenService>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();
            services.AddScoped<IUserAppAuthService, UserAppAuthService>();
            services.AddScoped<IScreenService, ScreenService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IRoleScreenService, RoleScreenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<IActionScreenService, ActionScreenService>();
            services.AddScoped<IUserAccessTreeService, UserAccessTreeService>();
            services.AddScoped<IUserLoginLogService, UserLoginLogService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            if (!string.IsNullOrWhiteSpace(userAppConnectionString))
                services.AddDbContext<UserAppContext>(options => ConfigureDatabaseProvider(options, databaseProvider, userAppConnectionString));

            
            services.AddAutoMapper(cfg =>
                {
                    if (!string.IsNullOrWhiteSpace(licenceAutoMapper))
                    {
                        cfg.LicenseKey = licenceAutoMapper;
                    }

                },
                typeof(UserProfile)
            );
            
            ApplicationGlobal.ApplicationGlobalID = applicationId;
            return services;
        }

        private static void ConfigureDatabaseProvider(DbContextOptionsBuilder options, string? databaseProvider, string connectionString)
        {
            var provider = string.IsNullOrWhiteSpace(databaseProvider)
                ? "Postgres"
                : databaseProvider.Trim();

            switch (provider.ToUpperInvariant())
            {
                case "POSTGRES":
                case "POSTGRESQL":
                case "NPGSQL":
                    options.UseNpgsql(connectionString);
                    break;
                case "SQLSERVER":
                case "SQL_SERVER":
                case "MSSQL":
                    options.UseSqlServer(connectionString);
                    break;
                default:
                    throw new InvalidOperationException($"Database provider '{databaseProvider}' no soportado. Use 'Postgres' o 'SqlServer'.");
            }
        }
    }
}
